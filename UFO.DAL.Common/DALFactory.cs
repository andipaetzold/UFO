namespace UFO.DAL.Common
{
    using System;
    using System.Configuration;
    using System.Reflection;

    public class DALFactory
    {
        private static readonly Assembly Assembly;
        private static readonly string AssemblyName;

        static DALFactory()
        {
            AssemblyName = ConfigurationManager.AppSettings["DalAssembly"];
            Assembly = Assembly.Load(AssemblyName);
        }

        public static IDatabase CreateDatabase(string connectionString)
        {
            var databaseClassName = AssemblyName + ".Database";
            var databaseClass = Assembly.GetType(databaseClassName);
            return (IDatabase)Activator.CreateInstance(databaseClass, connectionString);
        }

        public static IArtistDAO CreateArtistDAO(IDatabase database) => CreateDao<IArtistDAO>(database);

        public static ICategoryDAO CreateCategoryDAO(IDatabase database) => CreateDao<ICategoryDAO>(database);

        public static ICountryDAO CreateCountryDAO(IDatabase database) => CreateDao<ICountryDAO>(database);

        public static IPerformanceDAO CreatePerformanceDAO(IDatabase database) => CreateDao<IPerformanceDAO>(database);

        public static IUserDAO CreateUserDAO(IDatabase database) => CreateDao<IUserDAO>(database);

        public static IVenueDAO CreateVenueDAO(IDatabase database) => CreateDao<IVenueDAO>(database);

        private static TInterface CreateDao<TInterface>(IDatabase database)
        {
            var typeName = typeof(TInterface).Name.Substring(1);
            var daoType = Assembly.GetType($"{AssemblyName}.{typeName}");
            return (TInterface)Activator.CreateInstance(daoType, database);
        }
    }
}
