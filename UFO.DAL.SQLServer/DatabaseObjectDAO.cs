namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using UFO.DAL.Common;
    using UFO.Domain;

    public static class DatabaseObjectDAO
    {
        public static readonly Dictionary<Type, DbType> DbTypeDictionary = new Dictionary<Type, DbType>
            {
                { typeof(int), DbType.Int32 },
                { typeof(int?), DbType.Int32 },
                { typeof(string), DbType.String },
                { typeof(bool), DbType.Boolean },
                { typeof(decimal), DbType.Decimal },
                { typeof(decimal?), DbType.Decimal },
                { typeof(DateTime), DbType.DateTime },
                { typeof(DateTime?), DbType.DateTime }
            };
    }

    public class DatabaseObjectDAO<T>
        where T : DatabaseObject, new()
    {
        #region Fields

        private readonly IDatabase database;

        #endregion

        public DatabaseObjectDAO(IDatabase database)
        {
            // check parameter
            if (database == null)
            {
                throw new ArgumentNullException(nameof(database));
            }

            this.database = database;
        }

        #region Properties

        public static string TableName => ((TableAttribute)typeof(T).GetCustomAttribute(typeof(TableAttribute))).Name;

        #endregion

        public ICollection<Tuple<string, string, string>> CreateColumnList(out List<string> joins)
        {
            // joins
            joins = new List<string>();
            var list = new List<Tuple<string, string, string>>();

            var propertyInfos = GetPropertyInfos();
            foreach (var propertyInfo in propertyInfos)
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);

                // "normal" column
                if (DatabaseObjectDAO.DbTypeDictionary.ContainsKey(propertyInfo.PropertyType))
                {
                    list.Add(new Tuple<string, string, string>(TableName, columnName, $"{TableName}_{columnName}"));
                }

                // column with reference
                if (propertyInfo.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
                {
                    // Id for reference
                    list.Add(new Tuple<string, string, string>(TableName, $"{columnName}", $"{TableName}_{columnName}"));

                    // Create foreign DAO
                    var genericType = typeof(DatabaseObjectDAO<>).MakeGenericType(propertyInfo.PropertyType);
                    var o = Activator.CreateInstance(genericType, database);

                    // Column list
                    var methodInfo = genericType.GetMethod(nameof(CreateColumnList));
                    var parameters = new object[] { null };
                    var listOther = (ICollection<Tuple<string, string, string>>)methodInfo.Invoke(o, parameters);
                    list.AddRange(listOther);

                    // add joins
                    var foreignTableName = (string)genericType.GetProperty(nameof(TableName)).GetValue(o);
                    joins.Add(CreateJoinStatement(TableName, columnName, foreignTableName));
                    var foreignJoins = (List<string>)parameters[0];
                    joins.AddRange(foreignJoins);
                }
            }

            return list;
        }

        public T CreateObjectFromDataReader(IDataRecord reader)
        {
            var o = new T();

            var propertyInfos = GetPropertyInfos(true);
            foreach (var propertyInfo in propertyInfos)
            {
                // types
                var propertyType = propertyInfo.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // remove nullable
                    propertyType = propertyType.GetGenericArguments()[0];
                }

                // columnName
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);
                if (columnName == null)
                {
                    continue;
                }
                var alias = TableName + "_" + columnName;

                // data
                object data = null;

                // dbtype
                DbType dbType;
                if (DatabaseObjectDAO.DbTypeDictionary.TryGetValue(propertyType, out dbType))
                {
                    var readerObject = reader[alias];
                    data = readerObject == DBNull.Value ? null : Convert.ChangeType(readerObject, propertyType);
                }
                if (propertyInfo.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
                {
                    // Create new DAO
                    var genericType = typeof(DatabaseObjectDAO<>).MakeGenericType(propertyType);
                    var foreignDAO = Activator.CreateInstance(genericType, database);

                    var methodInfo = genericType.GetMethod(nameof(CreateObjectFromDataReader));

                    data = methodInfo.Invoke(foreignDAO, new object[] { reader });
                }

                // set
                propertyInfo.SetValue(o, data);
            }

            // set id
            o.Id = (int)reader[TableName + "_Id"];

            return o;
        }

        public bool Delete(T o)
        {
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if (!o.HasId)
            {
                return false;
            }

            // delete
            using (var command = CreateDeleteByIdCommand(o.Id))
            {
                if (database.ExecuteNonQuery(command) == 1)
                {
                    o.DeleteId();
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
            if (id <= 0)
            {
                return null;
            }

            using (var command = CreateGetByIdCommand(id))
            {
                using (var reader = database.ExecuteReader(command))
                {
                    return reader.Read() ? CreateObjectFromDataReader(reader) : null;
                }
            }
        }

        public bool Insert(T o)
        {
            // check parameter
            if (o == null)
            {
                throw new ArgumentNullException(nameof(o));
            }
            if (o.HasId)
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
                    o.Id = id.Value;
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
            if (!o.HasId)
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

        public Tuple<DbType, object> GetTypeValueFromProperty(T o, PropertyInfo propertyInfo)
        {
            DbType dbType;
            object value;

            if (DatabaseObjectDAO.DbTypeDictionary.ContainsKey(propertyInfo.PropertyType))
            {
                dbType = DatabaseObjectDAO.DbTypeDictionary[propertyInfo.PropertyType];
                value = propertyInfo.GetValue(o);
            }
            else if (propertyInfo.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
            {
                dbType = DbType.Int32;
                value = ((DatabaseObject)propertyInfo.GetValue(o)).Id;
            }
            else
            {
                return null;
            }

            return new Tuple<DbType, object>(dbType, value);
        }

        private DbCommand CreateDeleteByIdCommand(object id)
        {
            // command text
            var commandText = CreateDeleteStatement(TableName, new List<string> { "[Id] = @Id" });

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, id);
            return command;
        }

        private static string CreateDeleteStatement(string table, IEnumerable<string> where)
        {
            var whereSQL = string.Join(" AND ", where);
            return $"DELETE FROM [{table}] WHERE {whereSQL};";
        }

        private DbCommand CreateGetAllCommand()
        {
            List<string> joins;
            var columnList = CreateColumnList(out joins);

            // command text
            var select = columnList.Select(column => $"[{column.Item1}].[{column.Item2}] as [{column.Item3}]").ToList();
            var commandText = CreateSelectStatement(select, TableName, joins);

            // command
            return database.CreateCommand(commandText);
        }

        private DbCommand CreateGetByIdCommand(int id)
        {
            List<string> joins;
            var columnList = CreateColumnList(out joins);

            // command text
            var select = columnList.Select(column => $"[{column.Item1}].[{column.Item2}] as [{column.Item3}]").ToList();
            var where = new[] { $"[{TableName}].[Id] = @Id" };
            var commandText = CreateSelectStatement(select, TableName, joins, where);

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, id);
            return command;
        }

        private DbCommand CreateInsertCommand(T o)
        {
            ICollection<PropertyInfo> propertyInfos;
            var columnNames = GetColumnNames(out propertyInfos, true);

            // command text
            var columns = columnNames.Select(column => $"[{column}]").ToList();
            var values = columnNames.Select(column => $"@{column}").ToList();

            var commandText = CreateInsertStatement(TableName, columns, values);

            // command
            var command = database.CreateCommand(commandText);
            foreach (var propertyInfo  in GetPropertyInfos(true))
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);
                var typeAndValue = GetTypeValueFromProperty(o, propertyInfo);

                if (typeAndValue == null)
                {
                    continue;
                }

                database.DefineParameter(command, columnName, typeAndValue.Item1, typeAndValue.Item2);
            }
            return command;
        }

        private static string CreateInsertStatement(
            string table,
            IEnumerable<string> columns,
            IEnumerable<string> values)
        {
            var columnSQL = string.Join(", ", columns);
            var valuesSQL = string.Join(", ", values);

            return $"INSERT INTO [{table}] ({columnSQL}) OUTPUT [Inserted].[Id] VALUES ({valuesSQL});";
        }

        private static string CreateJoinStatement(string baseTableName, string baseColumnName, string foreignTableName)
            => $"JOIN [{foreignTableName}] ON ([{baseTableName}].[{baseColumnName}] = [{foreignTableName}].[Id])";

        private string CreateSelectStatement(
            IEnumerable<string> select,
            string from,
            IEnumerable<string> join,
            IEnumerable<string> where)
        {
            var selectSQL = string.Join(", ", select);
            var joinSQL = string.Join(" ", join);
            var whereSQL = string.Join(" AND ", where);

            return $"SELECT {selectSQL} FROM [{from}] {joinSQL} WHERE {whereSQL}";
        }

        private string CreateSelectStatement(IEnumerable<string> select, string from, IEnumerable<string> join)
        {
            return CreateSelectStatement(select, from, join, new[] { "1 = 1" });
        }

        private DbCommand CreateUpdateByIdCommand(T o)
        {
            var properties = GetPropertyInfos(true);
            var columnNames = GetColumnNames(true);

            // command text
            var set = columnNames.Select(columnName => string.Format("[{0}] = @{0}", columnName)).ToList();
            var where = new List<string> { "[Id] = @Id" };

            var commandText = CreateUpdateStatement(TableName, set, where);

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, o.Id);
            foreach (var propertyInfo in properties)
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);
                var typeAndValue = GetTypeValueFromProperty(o, propertyInfo);

                if (typeAndValue == null)
                {
                    continue;
                }

                database.DefineParameter(command, columnName, typeAndValue.Item1, typeAndValue.Item2);
            }
            return command;
        }

        private static string CreateUpdateStatement(string table, IEnumerable<string> set, IEnumerable<string> where)
        {
            var setSQL = string.Join(", ", set);
            var whereSQL = string.Join(" AND ", where);
            return $"UPDATE [{table}] SET {setSQL} WHERE {whereSQL}";
        }

        private string CreateUpdateStatement(string table, IEnumerable<string> set)
        {
            return CreateUpdateStatement(table, set, new[] { "1 = 1" });
        }

        private static string GetColumnNameFromPropertyInfo(PropertyInfo propertyInfo)
        {
            if (!propertyInfo.IsDefined(typeof(ColumnAttribute)))
            {
                return null;
            }

            var columnName = ((ColumnAttribute)propertyInfo.GetCustomAttribute(typeof(ColumnAttribute))).Name;
            if (propertyInfo.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
            {
                columnName += "_Id";
            }

            return columnName;
        }

        private static ICollection<string> GetColumnNames(
            out ICollection<PropertyInfo> propertyInfos,
            bool excludeKey = false)
        {
            propertyInfos = GetPropertyInfos(excludeKey);
            return propertyInfos.Select(GetColumnNameFromPropertyInfo).ToList();
        }

        private static ICollection<string> GetColumnNames(bool excludeKey = false)
        {
            return GetPropertyInfos(excludeKey).Select(GetColumnNameFromPropertyInfo).ToList();
        }

        private static ICollection<PropertyInfo> GetPropertyInfos(bool excludeKey = false)
        {
            var propertyInfos = new List<PropertyInfo>();
            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                if (!propertyInfo.IsDefined(typeof(ColumnAttribute)))
                {
                    continue;
                }

                if (!excludeKey || !propertyInfo.IsDefined(typeof(KeyAttribute)))
                {
                    propertyInfos.Add(propertyInfo);
                }
            }

            return propertyInfos;
        }
    }
}
