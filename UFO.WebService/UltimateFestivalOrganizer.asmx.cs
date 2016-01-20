using Implementation_Server = UFO.Server.Implementation.Server;

namespace UFO.WebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Web.Services;
    using UFO.Domain;

    [WebService(Namespace = "http://andipaetzold.de/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class UltimateFestivalOrganizer : WebService
    {
        [WebMethod]
        public Country GetCountryById(int id)
        {
            return Implementation_Server.CountryServer.GetById(id);
        }

        [WebMethod]
        public Category GetCategoryById(int id)
        {
            return Implementation_Server.CategoryServer.GetById(id);
        }

        [WebMethod]
        public Artist GetArtistById(int id)
        {
            return Implementation_Server.ArtistServer.GetById(id);
        }

        [WebMethod]
        public Venue GetVenueById(int id)
        {
            return Implementation_Server.VenueServer.GetById(id);
        }

        [WebMethod]
        public Performance GetPerformanceById(int id)
        {
            return Implementation_Server.PerformanceServer.GetById(id);
        }

        [WebMethod]
        public List<DateTime> GetDatesWithPerformances()
        {
            return Implementation_Server.PerformanceServer.GetDatesWithPerformances().ToList();
        }

        [WebMethod]
        public List<Performance> GetAllPerformances()
        {
            return Implementation_Server.PerformanceServer.GetAll().ToList();
        }

        [WebMethod]
        public List<Artist> GetAllArtists()
        {
            return Implementation_Server.ArtistServer.GetAll().ToList();
        }

        [WebMethod]
        public List<Category> GetAllCategories()
        {
            return Implementation_Server.CategoryServer.GetAll().ToList();
        }

        [WebMethod]
        public List<Country> GetAllCountries()
        {
            return Implementation_Server.CountryServer.GetAll().ToList();
        }

        [WebMethod]
        public List<Venue> GetAllVenues()
        {
            return Implementation_Server.VenueServer.GetAll().ToList();
        }

        [WebMethod]
        public List<Artist> GetAllButDeletedArtists()
        {
            return Implementation_Server.ArtistServer.GetAllButDeleted().ToList();
        }

        [WebMethod]
        public List<Performance> GetPerformancesByDate(DateTime dateTime)
        {
            return Implementation_Server.PerformanceServer.GetByDate(dateTime).ToList();
        }

        [WebMethod]
        public List<Performance> GetUpcomingPerformancesByArtist(Artist artist)
        {
            return Implementation_Server.PerformanceServer.GetUpcomingByArtist(artist).ToList();
        }

        [WebMethod]
        public List<Performance> GetPerformancesByArtist(Artist artist)
        {
            return Implementation_Server.PerformanceServer.GetByArtist(artist).ToList();
        }

        [WebMethod]
        public bool CheckLogin(string username, string password)
        {
            return Implementation_Server.UserServer.CheckLoginData(username, password);
        }

        [WebMethod]
        public void DeletePerformance(Performance performance)
        {
            Implementation_Server.PerformanceServer.Remove(performance);
        }

        [WebMethod]
        public bool UpdatePerformance(Performance performance)
        {
            return Implementation_Server.PerformanceServer.Update(performance);
        }

        [WebMethod]
        public bool AddPerformance(Performance performance)
        {
            return Implementation_Server.PerformanceServer.Add(performance);
        }
    }
}
