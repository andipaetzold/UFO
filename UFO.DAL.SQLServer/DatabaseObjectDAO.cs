namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using UFO.DAL.Common;
    using UFO.Domain;
    using Prop = System.Tuple<string, object, System.Data.DbType>;

    public class DatabaseObjectDAO<T>
        where T : DatabaseObject, new()
    {
        #region Fields

        protected readonly IDatabase database;

        private readonly Dictionary<Type, DbType> dbTypeDictionary = new Dictionary<Type, DbType>
            {
                { typeof(int), DbType.Int32 },
                { typeof(string), DbType.String },
                { typeof(bool), DbType.Boolean }
            };

        #endregion

        public DatabaseObjectDAO(IDatabase database)
        {
            this.database = database;
        }

        #region Properties

        private string TableName
        {
            get
            {
                var tableAttribute = (TableAttribute)typeof(T).GetCustomAttribute(typeof(TableAttribute));
                return tableAttribute.Name;
            }
        }

        #endregion

        private DbCommand CreateDeleteByIdCommand(object id)
        {
            // command text
            string commandText = $"DELETE FROM [{TableName}] WHERE [Id] = @Id;";

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, id);
            return command;
        }

        private DbCommand CreateGetAllCommand()
        {
            // command text
            string commandText = $"SELECT * FROM [{TableName}];";

            // command
            return database.CreateCommand(commandText);
        }

        private DbCommand CreateGetByIdCommand(int id)
        {
            // command text
            string commandText = $"SELECT * FROM [{TableName}] WHERE [Id] = @Id;";

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, id);
            return command;
        }

        private DbCommand CreateInsertCommand(T o)
        {
            // column name list
            var columnNames = GetColumnNameProperties();

            // command text
            var commandTextBuilder = new StringBuilder();
            commandTextBuilder.AppendFormat("INSERT INTO [{0}] (", TableName);
            foreach (var columnName in columnNames)
            {
                commandTextBuilder.AppendFormat("[{0}]", columnName);
                if (columnNames.Last() != columnName)
                {
                    commandTextBuilder.Append(", ");
                }
            }
            commandTextBuilder.Append(") ");
            commandTextBuilder.Append("OUTPUT[Inserted].[Id] ");
            commandTextBuilder.Append("VALUES (");
            foreach (var columnName in columnNames)
            {
                commandTextBuilder.AppendFormat("@{0}", columnName);
                if (columnNames.Last() != columnName)
                {
                    commandTextBuilder.Append(", ");
                }
            }
            commandTextBuilder.Append(")");
            var commandText = commandTextBuilder.ToString();

            // command
            var command = database.CreateCommand(commandText);
            foreach (var propertyInfo in GetColumnProperties())
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);
                var dbType = dbTypeDictionary[propertyInfo.PropertyType];
                database.DefineParameter(command, columnName, dbType, propertyInfo.GetValue(o));
            }
            return command;
        }

        private T CreateObjectFromDataReader(IDataRecord reader)
        {
            var o = new T();

            var properties = GetColumnProperties();
            foreach (var property in properties)
            {
                // columnName
                var columnName = GetColumnNameFromPropertyInfo(property);
                if (columnName == null)
                {
                    continue;
                }

                // dbtype
                var dbType = dbTypeDictionary?[property.PropertyType];
                if (dbType == null)
                {
                    continue;
                }

                // data
                var readerObject = reader[columnName];
                var data = readerObject == DBNull.Value ? null : Convert.ChangeType(readerObject, property.PropertyType);

                // set
                property.SetValue(o, data);
            }

            // set id
            o.InsertedInDatabase((int)reader["Id"]);

            return o;
        }

        private DbCommand CreateUpdateByIdCommand(T o)
        {
            // column name list
            var columnNames = GetColumnNameProperties();

            // command text
            var commandTextBuilder = new StringBuilder();
            commandTextBuilder.AppendFormat("UPDATE [{0}] SET ", TableName);
            foreach (var columnName in columnNames)
            {
                commandTextBuilder.AppendFormat("[{0}] = @{0}", columnName);

                if (columnNames.Last() != columnName)
                {
                    commandTextBuilder.Append(", ");
                }
            }
            commandTextBuilder.Append(" WHERE [Id] = @Id");

            var commandText = commandTextBuilder.ToString();

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, o.Id);
            foreach (var propertyInfo in GetColumnProperties())
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);
                var dbType = dbTypeDictionary[propertyInfo.PropertyType];
                database.DefineParameter(command, columnName, dbType, propertyInfo.GetValue(o));
            }
            return command;
        }

        public bool Delete(T o)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if (!o.HasId())
            {
                return false;
            }

            // delete
            using (var command = CreateDeleteByIdCommand(o.Id))
            {
                if (database.ExecuteNonQuery(command) == 1)
                {
                    o.DeletedFromDatabase();
                    return true;
                }
                return false;
            }
        }

        public ICollection<T> GetAll()
        {
            using (var command = CreateGetAllCommand())
            {
                using (var reader = database.ExecuteReader(command))
                {
                    IList<T> result = new List<T>();
                    while (reader.Read())
                    {
                        result.Add(CreateObjectFromDataReader(reader));
                    }
                    return result;
                }
            }
        }

        public T GetById(int id)
        {
            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    return reader.Read() ? CreateObjectFromDataReader(reader) : null;
                }
            }
        }

        private string GetColumnNameFromPropertyInfo(PropertyInfo propertyInfo)
        {
            var columnAttribute = propertyInfo.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
            return columnAttribute?.Name;
        }

        private ICollection<string> GetColumnNameProperties()
        {
            var list = new List<string>();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var columnAttribute = propertyInfo.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
                if (columnAttribute != null && columnAttribute.Name != "Id")
                {
                    list.Add(columnAttribute.Name);
                }
            }

            return list;
        }

        private ICollection<PropertyInfo> GetColumnProperties()
        {
            var list = new List<PropertyInfo>();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var columnAttribute = propertyInfo.GetCustomAttribute(typeof(ColumnAttribute)) as ColumnAttribute;
                if (columnAttribute != null && columnAttribute.Name != "Id")
                {
                    list.Add(propertyInfo);
                }
            }

            return list;
        }

        public bool Insert(T o)
        {
            // check parameter
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if (o.HasId())
            {
                return false;
            }
            if (!o.TryValidate())
            {
                return false;
            }

            // insert
            using (var command = CreateInsertCommand(o))
            {
                var id = database.ExecuteScalar(command) as int?;

                if (id.HasValue)
                {
                    o.InsertedInDatabase(id.Value);
                    return true;
                }
                return false;
            }
        }

        public bool Update(T o)
        {
            // check parameter
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if (!o.HasId())
            {
                return false;
            }
            if (!o.TryValidate())
            {
                return false;
            }

            // update
            using (var command = CreateUpdateByIdCommand(o))
            {
                return database.ExecuteNonQuery(command) == 1;
            }
        }
    }
}
