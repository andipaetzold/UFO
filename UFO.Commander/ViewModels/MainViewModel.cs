namespace UFO.Commander.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using PropertyChanged;
    using UFO.Commander.Collections;
    using UFO.Domain;
    using UFO.Server;

    [ImplementPropertyChanged]
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private DateTime selectedDate = DateTime.Today;

        #endregion

        public MainViewModel()
        {
            Artists = new DatabaseSyncObservableCollection<Artist>(Server.ArtistServer);
            Categories = new DatabaseSyncObservableCollection<Category>(Server.CategoryServer);
            Countries = new DatabaseSyncObservableCollection<Country>(Server.CountryServer);
            Venues = new DatabaseSyncObservableCollection<Venue>(Server.VenueServer);

            SelectedDateChanged = new LambdaCommand(o => LoadPerformances((DateTime)o));
        }

        #region Properties

        public DatabaseSyncObservableCollection<Artist> Artists { get; }
        public DatabaseSyncObservableCollection<Category> Categories { get; }
        public DatabaseSyncObservableCollection<Country> Countries { get; }
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

        public ICommand SendNotifactionCommand { get; } =
            new LambdaCommand(o => Server.ArtistServer.SendNotificationEmail(((IList)o).Cast<Artist>()));

        public DatabaseSyncObservableCollection<Venue> Venues { get; }

        #endregion

        private void LoadPerformances(DateTime dateTime)
        {
            var performances = Server.PerformanceServer.GetAll().ToList();
            DayProgram = VenueProgram.Create(performances, Venues);
        }
    }
}
