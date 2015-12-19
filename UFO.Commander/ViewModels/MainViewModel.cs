namespace UFO.Commander.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using PropertyChanged;
    using UFO.Commander.Collections;
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
            ChangedArtists = new ObservableHashSet<Artist>();

            SelectedDateChanged = new LambdaCommand(o => LoadPerformances((DateTime)o));
            UpdateAllCommand = new LambdaCommand(UpdateAll);
            SendNotifactionCommand = new LambdaCommand(
                () =>
                    {
                        Server.ArtistServer.SendNotificationEmail(ChangedArtists);
                        ChangedArtists.Clear();
                    });

            UpdateAll();
        }

        #region Properties

        public ObservableCollection<Artist> Artists { get; private set; }
        public IList<Artist> ArtistsWithNull { get; set; }
        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableHashSet<Artist> ChangedArtists { get; }
        public ObservableCollection<Country> Countries { get; private set; }
        public List<VenueProgram> DayProgram { get; } = new List<VenueProgram>();

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
        public ObservableCollection<Venue> Venues { get; private set; }

        #endregion

        public void UpdateAll()
        {
            Artists = new DatabaseSyncObservableCollection<Artist>(Server.ArtistServer);
            ArtistsWithNull = Artists.ToList();
            ArtistsWithNull.Insert(0, VenueProgram.NullArtist);

            Categories = new DatabaseSyncObservableCollection<Category>(Server.CategoryServer);
            Countries = new DatabaseSyncObservableCollection<Country>(Server.CountryServer);
            Venues = new DatabaseSyncObservableCollection<Venue>(Server.VenueServer);

            LoadPerformances(SelectedDate);
        }

        private void LoadPerformances(DateTime date)
        {
            DayProgram.Clear();

            var performances = Server.PerformanceServer.GetByDate(date).ToList();
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

        private void PerformancePropertyChanged(object sender, PropertyChangedEventArgs e)
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
                var prevPerformance = Server.PerformanceServer.GetById(performance.Id);
                if (Server.PerformanceServer.Update(performance))
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
                var prevPerformance = Server.PerformanceServer.GetById(performance.Id);
                ChangedArtists.Add(prevPerformance.Artist);

                Server.PerformanceServer.Remove(performance);
            }
            else if (!performance.Artist.Equals(VenueProgram.NullArtist))
            {
                if (Server.PerformanceServer.Add(performance))
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
