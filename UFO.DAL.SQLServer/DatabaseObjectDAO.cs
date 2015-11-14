namespace UFO.DAL.SQLServer
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using UFO.DAL.Common;
    using UFO.Domain;

    public class DatabaseObjectDAO<T>
        where T : DatabaseObject, new()
    {
        private static readonly Dictionary<Type, DbType> DbTypeDictionary = new Dictionary<Type, DbType>
            {
                { typeof(int), DbType.Int32 },
                { typeof(int?), DbType.Int32 },
                { typeof(string), DbType.String },
                { typeof(bool), DbType.Boolean },
                { typeof(decimal), DbType.Decimal },
                { typeof(decimal?), DbType.Decimal },
                { typeof(DateTime), DbType.DateTime }
            };

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

        public static string TableName => typeof(T).Name;

        #endregion

        public ICollection<Tuple<string, string, string>> CreateColumnList(out List<string> joins)
        {
            // joins
            joins = new List<string>();

            // table, column, alias
            var list = new List<Tuple<string, string, string>>();

            var properties = GetProperties();
            foreach (var propertyInfo in properties)
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);

                // "normal" column
                if (DbTypeDictionary.ContainsKey(propertyInfo.PropertyType))
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
            var commandTextBuilder = new StringBuilder();

            // command - select
            commandTextBuilder.Append("SELECT ");
            foreach (var column in columnList)
            {
                commandTextBuilder.AppendFormat("[{0}].[{1}] as [{2}]", column.Item1, column.Item2, column.Item3);

                if (columnList.Last() != column)
                {
                    commandTextBuilder.Append(", ");
                }
            }

            // command - from
            commandTextBuilder.AppendFormat(" FROM [{0}] ", TableName);

            // command - join
            foreach (var @join in joins)
            {
                commandTextBuilder.Append(@join);
            }

            var commandText = commandTextBuilder.ToString();

            // command
            return database.CreateCommand(commandText);
        }

        private DbCommand CreateGetByIdCommand(int id)
        {
            List<string> joins;
            var columnList = CreateColumnList(out joins);

            // command text
            var commandTextBuilder = new StringBuilder();

            // command - select
            commandTextBuilder.Append("SELECT ");
            foreach (var column in columnList)
            {
                commandTextBuilder.AppendFormat("[{0}].[{1}] as [{2}]", column.Item1, column.Item2, column.Item3);

                if (columnList.Last() != column)
                {
                    commandTextBuilder.Append(", ");
                }
            }

            // command - from
            commandTextBuilder.AppendFormat(" FROM [{0}] ", TableName);

            // command - join
            foreach (var @join in joins)
            {
                commandTextBuilder.Append(@join);
            }

            // command - where
            commandTextBuilder.AppendFormat(" WHERE [{0}].[Id] = @Id", TableName);

            // finish
            var commandText = commandTextBuilder.ToString();

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, id);
            return command;
        }

        private DbCommand CreateInsertCommand(T o)
        {
            // column name list
            var columnNames = GetColumnNameProperties("Id");

            // command text
            var columns = new List<string>();
            foreach (var column in columnNames)
            {
                columns.Add($"[{column}]");
            }

            var values = new List<string>();
            foreach (var column in columnNames)
            {
                values.Add($"@{column}");
            }

            var commandText = CreateInsertStatement(TableName, columns, values);

            // command
            var command = database.CreateCommand(commandText);
            foreach (var propertyInfo  in GetProperties("Id"))
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);
                object value;

                DbType dbType;
                if (propertyInfo.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
                {
                    dbType = DbType.Int32;
                    value = ((DatabaseObject)propertyInfo.GetValue(o)).Id;
                }
                else
                {
                    dbType = DbTypeDictionary[propertyInfo.PropertyType];
                    value = propertyInfo.GetValue(o);
                }
                database.DefineParameter(command, columnName, dbType, value);
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

        public T CreateObjectFromDataReader(IDataRecord reader)
        {
            var o = new T();

            var properties = GetProperties("Id");
            foreach (var property in properties)
            {
                // types
                var propertyType = property.PropertyType;
                var propertyTypeWithoutNullable = propertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    propertyTypeWithoutNullable = propertyType.GetGenericArguments()[0];
                }

                // columnName
                var columnName = GetColumnNameFromPropertyInfo(property);
                if (columnName == null)
                {
                    continue;
                }
                var alias = TableName + "_" + columnName;

                // data
                object data = null;

                // dbtype
                DbType dbType;
                if (DbTypeDictionary.TryGetValue(propertyTypeWithoutNullable, out dbType))
                {
                    var readerObject = reader[alias];
                    data = readerObject == DBNull.Value
                               ? null
                               : Convert.ChangeType(readerObject, propertyTypeWithoutNullable);
                }
                if (property.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
                {
                    // Create foreign DAO
                    var genericType = typeof(DatabaseObjectDAO<>).MakeGenericType(propertyType);
                    var foreignDAO = Activator.CreateInstance(genericType, database);

                    var methodInfo = genericType.GetMethod(nameof(CreateObjectFromDataReader));

                    data = methodInfo.Invoke(foreignDAO, new object[] { reader });
                }

                // set
                property.SetValue(o, data);
            }

            // set id
            o.InsertedInDatabase((int)reader[TableName + "_Id"]);

            return o;
        }

        private string CreateSelectStatement(
            IEnumerable<string> select,
            string from,
            string join,
            IEnumerable<string> where)
        {
            var selectSQL = string.Join(", ", select);
            var joinSQL = string.Join(" ", join);
            var whereSQL = string.Join(" AND ", where);

            return $"SELECT [{selectSQL}] FROM {from} {joinSQL} WHERE {whereSQL}";
        }

        private DbCommand CreateUpdateByIdCommand(T o)
        {
            // column name list
            var columnNames = GetColumnNameProperties("Id");

            // command text
            var set = columnNames.Select(columnName => string.Format("[{0}] = @{0}", columnName)).ToList();
            var where = new List<string> { "[Id] = @Id" };

            var commandText = CreateUpdateStatement(TableName, set, where);

            // command
            var command = database.CreateCommand(commandText);
            database.DefineParameter(command, "Id", DbType.Int32, o.Id);
            foreach (var propertyInfo in GetProperties("Id"))
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);
                object value;
                DbType dbType;
                if (propertyInfo.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
                {
                    dbType = DbType.Int32;
                    value = ((DatabaseObject)propertyInfo.GetValue(o)).Id;
                }
                else
                {
                    dbType = DbTypeDictionary[propertyInfo.PropertyType];
                    value = propertyInfo.GetValue(o);
                }
                database.DefineParameter(command, columnName, dbType, value);
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
            return CreateUpdateStatement(table, set, new List<string> { "1" });
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

        private static string GetColumnNameFromPropertyInfo(PropertyInfo propertyInfo)
        {
            var columnName = propertyInfo.Name;
            if (propertyInfo.PropertyType.IsSubclassOf(typeof(DatabaseObject)))
            {
                columnName += "_Id";
            }

            return columnName;
        }

        private static ICollection<string> GetColumnNameProperties(params string[] columnsExclude)
        {
            var list = new List<string>();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);

                if (columnsExclude == null || !columnsExclude.Contains(columnName))
                {
                    list.Add(columnName);
                }
            }

            return list;
        }

        private static ICollection<PropertyInfo> GetProperties(params string[] columnsExclude)
        {
            var list = new List<PropertyInfo>();

            foreach (var propertyInfo in typeof(T).GetProperties())
            {
                var columnName = GetColumnNameFromPropertyInfo(propertyInfo);

                if (columnsExclude == null || !columnsExclude.Contains(columnName))
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
