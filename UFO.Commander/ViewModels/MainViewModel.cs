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
        public MainViewModel()
        {
            SendNotifactionCommand =
                new LambdaCommand(o => Server.ArtistServer.SendNotificationEmail((o as IList)?.Cast<Artist>()));

            Artists = new DatabaseSyncObservableCollection<Artist>(Server.ArtistServer);
            Categories = new DatabaseSyncObservableCollection<Category>(Server.CategoryServer);
            Countries = new DatabaseSyncObservableCollection<Country>(Server.CountryServer);
            Venues = new DatabaseSyncObservableCollection<Venue>(Server.VenueServer);

            var performances = Server.PerformanceServer.GetAll().ToList();
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
