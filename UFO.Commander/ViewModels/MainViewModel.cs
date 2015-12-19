namespace UFO.Commander.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
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
            SelectedDateChanged = new LambdaCommand(o => LoadPerformances((DateTime)o));
            UpdateAllCommand = new LambdaCommand(UpdateAll);

            UpdateAll();
        }

        #region Properties

        public ObservableCollection<Artist> Artists { get; private set; }
        public IList<Artist> ArtistsWithNull { get; set; }
        public ObservableCollection<Category> Categories { get; private set; }
        public ObservableCollection<Country> Countries { get; private set; }
        public List<VenueProgram> DayProgram { get; private set; }

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

        public ICommand SendNotifactionCommand { get; } =
            new LambdaCommand(o => Server.ArtistServer.SendNotificationEmail(((IList)o).Cast<Artist>()));

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
            var performances = Server.PerformanceServer.GetByDate(date).ToList();
            DayProgram = VenueProgram.Create(date, performances, Venues);
        }
    }
}
