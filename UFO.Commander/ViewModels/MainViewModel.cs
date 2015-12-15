namespace UFO.Commander.ViewModels
{
    using System;
    using System.Collections.Generic;
    using UFO.Commander.Collections;
    using UFO.Domain;
    using UFO.Server;

    public class MainViewModel : ViewModelBase
    {
        #region Fields

        private readonly Server server = new Server();

        #endregion

        public MainViewModel()
        {
            Artists = new DatabaseSyncObservableCollection<Artist>(server.ArtistServer);
            Categories = new DatabaseSyncObservableCollection<Category>(server.CategoryServer);
            Countries = new DatabaseSyncObservableCollection<Country>(server.CountryServer);
            Performances = new DatabaseSyncObservableCollection<Performance>(server.PerformanceServer);
            Venues = new DatabaseSyncObservableCollection<Venue>(server.VenueServer);

            TimeList = new List<DateTime>();
            var day = new DateTime(2015, 7, 25);
            for (var i = 14; i <= 23; ++i)
            {
                TimeList.Add(day.AddHours(i));
            }
        }

        #region Properties

        public DatabaseSyncObservableCollection<Artist> Artists { get; }
        public DatabaseSyncObservableCollection<Category> Categories { get; }
        public DatabaseSyncObservableCollection<Country> Countries { get; }
        public DatabaseSyncObservableCollection<Performance> Performances { get; set; }
        public List<DateTime> TimeList { get; }
        public DatabaseSyncObservableCollection<Venue> Venues { get; }

        #endregion
    }
}
