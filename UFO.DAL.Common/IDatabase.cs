namespace UFO.DAL.Common
{
    using System.Data;
    using System.Data.Common;

    public interface IDatabase
    {
        DbCommand CreateCommand(string commandText);

        int DeclareParameter(DbCommand command, string name, DbType type);

        void DefineParameter(DbCommand command, string name, DbType type, object value);

        int ExecuteNonQuery(DbCommand command);
        object ExecuteScalar(DbCommand command);

        IDataReader ExecuteReader(DbCommand command);

        void SetParameter(DbCommand command, string name, object value);
    }
}
