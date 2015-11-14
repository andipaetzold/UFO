namespace Program
{
    using System;
    using System.Configuration;
    using System.Linq;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("------------");

            var database = new Database(ConfigurationManager.ConnectionStrings["UnitTest"].ConnectionString);

            var categoryDAO = new CategoryDAO(database);
            categoryDAO.Insert(new Category("name"));

            var artist = new Artist("nameaa", categoryDAO.GetAll().First(), null, "andi.paetzold@gmail.com", null);

            var artistDAO = new ArtistDAO(database);

            var a = artistDAO.GetAll();
            artistDAO.Insert(artist);
            a = artistDAO.GetAll();

            Console.ReadLine();
        }
    }
}
