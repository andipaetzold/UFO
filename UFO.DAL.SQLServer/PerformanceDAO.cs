namespace UFO.DAL.SQLServer
{
    using UFO.DAL.Common;
    using UFO.Domain;

    public class PerformanceDAO : DatabaseObjectDAO<Performance>,
                                  IPerformanceDAO
    {
        public PerformanceDAO(IDatabase database)
            : base(database)
        {
        }
    }
}
