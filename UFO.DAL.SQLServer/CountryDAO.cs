namespace UFO.DAL.SQLServer
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class CountryDAO : DatabaseObjectDAO<Country>,
                              ICountryDAO
    {
        public CountryDAO(IDatabase database)
            : base(database)
        {
        }
    }
}
