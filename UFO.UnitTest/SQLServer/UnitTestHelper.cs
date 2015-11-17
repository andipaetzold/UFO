namespace UFO.UnitTest.SQLServer
{
    using System;
    using System.Configuration;
    using System.Linq;
    using UFO.DAL.Common;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

    public static class UnitTestHelper
    {
        private static readonly Random Random = new Random();

        public static Artist GetRandomArtist()
        {
            return new Artist(
                GetRandomString(),
                GetRandomInsertedCategory(),
                GetRandomInsertedCountry(),
                GetRandomString(),
                GetRandomEmail(),
                GetRandomString());
        }

        public static Category GetRandomCategory()
        {
            return new Category(GetRandomString());
        }

        public static Country GetRandomCountry()
        {
            return new Country(GetRandomString());
        }

        public static DateTime GetRandomDateTime()
        {
            return DateTime.UtcNow.AddDays(Random.Next(90));
        }

        public static Artist GetRandomInsertedArtist()
        {
            var artist = GetRandomArtist();
            new ArtistDAO(GetUnitTestDatabase()).Insert(artist);

            return artist;
        }

        public static Category GetRandomInsertedCategory()
        {
            var category = GetRandomCategory();
            new CategoryDAO(GetUnitTestDatabase()).Insert(category);

            return category;
        }

        public static Country GetRandomInsertedCountry()
        {
            var country = GetRandomCountry();
            new CountryDAO(GetUnitTestDatabase()).Insert(country);

            return country;
        }

        public static Performance GetRandomInsertedPerformance()
        {
            var performance = GetRandomPerformance();
            new PerformanceDAO(GetUnitTestDatabase()).Insert(performance);

            return performance;
        }

        public static User GetRandomInsertedUser()
        {
            var user = GetRandomUser();
            new UserDAO(GetUnitTestDatabase()).Insert(user);

            return user;
        }

        public static Venue GetRandomInsertedVenue()
        {
            var venue = GetRandomVenue();
            new VenueDAO(GetUnitTestDatabase()).Insert(venue);

            return venue;
        }

        public static Performance GetRandomPerformance()
        {
            return new Performance(GetRandomDateTime(), GetRandomInsertedArtist(), GetRandomInsertedVenue());
        }

        public static string GetRandomString()
        {
            const int Length = 8;

            const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(Chars, Length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static User GetRandomUser()
        {
            return new User(GetRandomString(), GetRandomString(), GetRandomEmail());
        }

        public static Venue GetRandomVenue()
        {
            return new Venue(GetRandomString(), GetRandomString(), Random.Next(-90, 90), Random.Next(-180, 180));
        }

        public static IDatabase GetUnitTestDatabase()
        {
            return new Database(ConfigurationManager.ConnectionStrings["UnitTest"].ConnectionString);
        }

        private static string GetRandomEmail()
        {
            return GetRandomString() + "@" + GetRandomString() + ".de";
        }
    }
}
