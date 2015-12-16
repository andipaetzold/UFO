namespace UFO.Commander.ViewModels
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
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
            SendNotifactionCommand =
                new LambdaCommand(o => server.ArtistServer.SendNotificationEmail((o as IList)?.Cast<Artist>()));

            Artists = new DatabaseSyncObservableCollection<Artist>(server.ArtistServer);
            Categories = new DatabaseSyncObservableCollection<Category>(server.CategoryServer);
            Countries = new DatabaseSyncObservableCollection<Country>(server.CountryServer);
            Venues = new DatabaseSyncObservableCollection<Venue>(server.VenueServer);

            var performances = server.PerformanceServer.GetAll().ToList();
            DayProgram =
                VenueProgram.Create(
                    performances.Where(p => p.DateTime.Date == new DateTime(2015, 7, 25)).ToList(),
                    Venues);
        }

        #region Properties

        public DatabaseSyncObservableCollection<Artist> Artists { get; }
        public DatabaseSyncObservableCollection<Category> Categories { get; }
        public DatabaseSyncObservableCollection<Country> Countries { get; }
        public List<VenueProgram> DayProgram { get; }
        public ICommand SendNotifactionCommand { get; }
        public DatabaseSyncObservableCollection<Venue> Venues { get; }

        #endregion
    }
}
