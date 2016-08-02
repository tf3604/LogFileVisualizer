using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    internal abstract class DalBase : IDisposable
    {
        protected SqlConnection _connection;

        public DalBase(string instanceName, string databaseName)
        {
            if (string.IsNullOrEmpty(instanceName))
            {
                throw new ArgumentNullException(nameof(instanceName));
            }

            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }

            SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder();
            sb.DataSource = instanceName;
            sb.InitialCatalog = databaseName;
            sb.IntegratedSecurity = true;

            _connection = new SqlConnection(sb.ToString());
            _connection.Open();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected SqlCommand GetCommand(string sql, params SqlParameter[] parameters)
        {
            SqlCommand command = null;
            try
            {
                command = new SqlCommand(sql, _connection);
                if (parameters != null &&
                    parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.CommandTimeout = 300;
                return command;
            }
            catch
            {
                if (command != null)
                {
                    command.Dispose();
                }
                throw;
            }
        }

        protected DataTable ExecuteSqlOneResultset(string sql, params SqlParameter[] parameters)
        {
            using (SqlCommand command = GetCommand(sql, parameters))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    try
                    {
                        adapter.Fill(table);
                        return table;
                    }
                    catch
                    {
                        if (table != null)
                        {
                            table.Dispose();
                        }
                        throw;
                    }
                }
            }
        }

        protected void ExecuteSqlNoResultsets(string sql, params SqlParameter[] parameters)
        {
            using (SqlCommand command = GetCommand(sql, parameters))
            {
                command.ExecuteNonQuery();
            }
        }

        protected object ExecuteSqlScalar(string sql, params SqlParameter[] parameters)
        {
            using (SqlCommand command = GetCommand(sql, parameters))
            {
                return command.ExecuteScalar();
            }
        }

        protected void BulkInsert(string tableName, DataTable table)
        {
            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(_connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.UseInternalTransaction, null))
            {
                foreach (DataColumn column in table.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.BulkCopyTimeout = 600;
                bulkCopy.DestinationTableName = tableName;
                bulkCopy.WriteToServer(table);
            }
        }

        protected T GetObjectValue<T>(DataRow row, string columnName)
        {
            object value = row[columnName];
            return GetObjectValue<T>(value);
        }

        protected T GetObjectValue<T>(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return default(T);
            }

            if (value is T)
            {
                return (T)value;
            }

            string message = string.Format("Cannot convert type {0} to type {1}", value.GetType().FullName, typeof(T).FullName);
            throw new ApplicationException(message);
        }

        protected object GetParameterValue(object value)
        {
            if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_connection != null)
                {
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
    }
}
