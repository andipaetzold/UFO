namespace UFO.Commander.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using PropertyChanged;
    using UFO.Commander.Util;
    using UFO.Domain;
    using UFO.Server.Implementation;

    [ImplementPropertyChanged]
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private DateTime selectedDate = DateTime.Today;
        private int selectedTabIndex;

        #endregion

        public MainViewModel()
        {
            Artists = new DatabaseSyncObservableCollection<Artist>(
                Server.ArtistServer,
                Server.ArtistServer.GetAllButDeletedAsync);

            SelectedDateChanged = new RelayCommand<DateTime>(LoadPerformances);
            UpdateAllCommand = new RelayCommand(UpdateAll);
            SendNotifactionCommand =
                new RelayCommand(
                    () =>
                        {
                            Server.ArtistServer.SendNotificationEmailAsync(ChangedArtists)
                                  .ContinueWith(t => Application.Current.Dispatcher.Invoke(ChangedArtists.Clear));
                        });

            UpdateAll();
        }

        #region Properties

        public DatabaseSyncObservableCollection<Artist> Artists { get; }
        public ObservableCollection<Artist> ArtistsWithNull { get; } = new ObservableHashSet<Artist>();

        public DatabaseSyncObservableCollection<Category> Categories { get; } =
            new DatabaseSyncObservableCollection<Category>(Server.CategoryServer);

        public ObservableHashSet<Artist> ChangedArtists { get; } = new ObservableHashSet<Artist>();

        public DatabaseSyncObservableCollection<Country> Countries { get; } =
            new DatabaseSyncObservableCollection<Country>(Server.CountryServer);

        public ObservableCollection<VenueProgram> DayProgram { get; } = new ObservableCollection<VenueProgram>();

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                LoadPerformances(value);
                selectedDate = value;
            }
        }

        public ICommand SelectedDateChanged { get; }

        public int SelectedTabIndex
        {
            get { return selectedTabIndex; }
            set
            {
                selectedTabIndex = value;

                UpdateAll();
            }
        }

        public ICommand SendNotifactionCommand { get; }
        public ICommand UpdateAllCommand { get; }

        public DatabaseSyncObservableCollection<Venue> Venues { get; } =
            new DatabaseSyncObservableCollection<Venue>(Server.VenueServer);

        #endregion

        public void UpdateAll()
        {
            Artists.PullUpdates();

            Server.ArtistServer.GetAllButDeletedAsync().ContinueWith(
                o =>
                    {
                        var list = o.Result.ToList();
                        list.Insert(0, VenueProgram.NullArtist);

                        Application.Current.Dispatcher.Invoke(ChangedArtists.Clear);
                        list.ForEach(ArtistsWithNull.Add);
                    });

            Categories.PullUpdates();
            Countries.PullUpdates();
            LoadPerformances(SelectedDate);
            Venues.PullUpdates();
        }

        private async void LoadPerformances(DateTime date)
        {
            DayProgram.Clear();

            var performances = (await Server.PerformanceServer.GetByDateAsync(date)).ToList();
            foreach (var venue in Venues)
            {
                var venueProgram = new VenueProgram { Venue = venue };

                for (var hour = 0; hour <= 23; ++hour)
                {
                    var performance =
                        performances.FirstOrDefault(p => (p.DateTime.Hour == hour) && (p.Venue.Equals(venue)));

                    if (performance == null)
                    {
                        performance = new Performance
                            {
                                Artist = VenueProgram.NullArtist,
                                DateTime = SelectedDate.AddHours(hour),
                                Venue = venue
                            };
                    }

                    performance.PropertyChanged += PerformancePropertyChanged;

                    venueProgram.Times[hour] = performance;
                }

                DayProgram.Add(venueProgram);
            }
        }

        private async void PerformancePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var performance = (Performance)sender;

            // only artist
            if (e.PropertyName != nameof(performance.Artist))
            {
                return;
            }

            // push to server
            if (performance.HasId && !performance.Artist.Equals(VenueProgram.NullArtist))
            {
                var prevPerformance = await Server.PerformanceServer.GetByIdAsync(performance.Id);
                if (await Server.PerformanceServer.UpdateAsync(performance))
                {
                    ChangedArtists.Add(prevPerformance.Artist);
                    ChangedArtists.Add(performance.Artist);
                }
                else
                {
                    performance.PropertyChanged -= PerformancePropertyChanged;
                    performance.Artist = prevPerformance.Artist;
                    performance.PropertyChanged += PerformancePropertyChanged;

                    MessageBox.Show(
                        "Invalid data. The item will be reset",
                        "Invalid data.",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
            else if (performance.HasId && performance.Artist.Equals(VenueProgram.NullArtist))
            {
                var prevPerformance = await Server.PerformanceServer.GetByIdAsync(performance.Id);
                ChangedArtists.Add(prevPerformance.Artist);

                await Server.PerformanceServer.RemoveAsync(performance);
            }
            else if (!performance.Artist.Equals(VenueProgram.NullArtist))
            {
                if (await Server.PerformanceServer.AddAsync(performance))
                {
                    ChangedArtists.Add(performance.Artist);
                }
                else
                {
                    performance.PropertyChanged -= PerformancePropertyChanged;
                    performance.Artist = VenueProgram.NullArtist;
                    performance.PropertyChanged += PerformancePropertyChanged;

                    MessageBox.Show(
                        "Invalid data. The item will be reset",
                        "Invalid data.",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }
    }
}
