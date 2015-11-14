namespace Program
{
    using System;
    using System.Configuration;
    using UFO.DAL.SQLServer;
    using UFO.Domain;

    class Program
    {
        static void Main(string[] args)
        {
            var userDAO = new UserDAO(new Database(ConfigurationManager.ConnectionStrings["UnitTest"].ConnectionString));

            var user = new User("username", "password", "andi.paetzold@gmail.com", true);
            Console.WriteLine(userDAO.Insert(user));

            var id = user.Id;
            var user2 = userDAO.GetById(id);

            var user3 = new User("username2", "password2", "andi.paetzold@gmail.com", true);
            Console.WriteLine(userDAO.Insert(user3));
            var list = userDAO.GetAll();

            Console.WriteLine(userDAO.Delete(user));

            Console.ReadLine();
        }
    }
}
