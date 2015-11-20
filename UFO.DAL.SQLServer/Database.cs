namespace UFO.DAL.SQLServer
{
    using System;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Transactions;
    using UFO.DAL.Common;

    public class Database : IDatabase
    {
        [ThreadStatic]
        private static DbConnection sharedConnection;

        #region Fields

        private readonly string connectionString;

        #endregion

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #region IDatabase Members

        public DbCommand CreateCommand(string commandText)
        {
            return new SqlCommand(commandText);
        }

        public void SetParameter(DbCommand command, string name, object value)
        {
            // check method parameters
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // set parameter
            if (command.Parameters.Contains(name))
            {
                command.Parameters[name].Value = value;
            }
            else
            {
                throw new ArgumentException($"Parameter {name} is not declared.");
            }
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            // check  method parameters
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            DbConnection connection = null;
            try
            {
                connection = GetOpenConnection();
                command.Connection = connection;

                var beahvior = Transaction.Current == null ? CommandBehavior.CloseConnection : CommandBehavior.Default;

                return command.ExecuteReader();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                ReleaseConnection(connection);
                return null;
            }
        }

        public int ExecuteNonQuery(DbCommand command)
        {
            // check method parameters
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // execute non query
            DbConnection connection = null;
            try
            {
                connection = GetOpenConnection();
                command.Connection = connection;
                return command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return 0;
            }
            finally
            {
                ReleaseConnection(connection);
            }
        }

        public void DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            // check method parameters
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // define parameter
            var paramIndex = DeclareParameter(command, name, type);

            if (type != DbType.Binary)
            {
                command.Parameters[paramIndex].Value = value ?? DBNull.Value;
            }
            else
            {
                command.Parameters[paramIndex].Value = value;
            }
        }

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            // check method parameters
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // declare parameter
            if (!command.Parameters.Contains(name))
            {
                return command.Parameters.Add(new SqlParameter(name, type));
            }
            throw new ArgumentException($"Parameter {name} already exists.");
        }

        public object ExecuteScalar(DbCommand command)
        {
            // check method parameters
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            // execute non query
            DbConnection connection = null;
            try
            {
                connection = GetOpenConnection();
                command.Connection = connection;
                return command.ExecuteScalar();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return null;
            }
            finally
            {
                ReleaseConnection(connection);
            }
        }

        #endregion

        private DbConnection GetOpenConnection()
        {
            var currentTransaction = Transaction.Current;

            if (currentTransaction == null)
            {
                return CreateOpenConnection();
            }
            if (sharedConnection != null)
            {
                return sharedConnection;
            }
            sharedConnection = CreateOpenConnection();
            currentTransaction.TransactionCompleted += (s, e) =>
                {
                    sharedConnection.Close();
                    sharedConnection = null;
                };

            return sharedConnection;
        }

        private DbConnection CreateOpenConnection()
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        private void ReleaseConnection(DbConnection connection)
        {
            if (Transaction.Current == null)
            {
                connection?.Close();
            }
        }
    }
}
