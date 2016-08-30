﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    [DataContract]
    public class ApplicationSqlConnection : IDisposable
    {
        private SqlConnection _connection;        
        public SqlInfoMessageEventHandler InfoMessage;

        public ApplicationSqlConnection(SqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _connection = connection;
            _connection.InfoMessage += InfoMessageInternal;
        }

        public ApplicationSqlConnection(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            _connection = new SqlConnection(connectionString);
            _connection.Open();
            _connection.InfoMessage += InfoMessageInternal;
        }

        public ApplicationSqlConnection(string instanceName, string databaseName)
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
            _connection.InfoMessage += InfoMessageInternal;
        }

        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
        }

        [DataMember]
        private string ConnectionString
        {
            get
            {
                return _connection?.ConnectionString;
            }
        }

        [DataMember]
        public string InstanceName
        {
            get
            {
                if (_connection == null)
                {
                    return string.Empty;
                }
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(_connection.ConnectionString);
                return sb.DataSource;
            }
        }

        [DataMember]
        public string DatabaseName
        {
            get
            {
                if (_connection == null)
                {
                    return string.Empty;
                }
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(_connection.ConnectionString);
                return sb.InitialCatalog;
            }
        }

        [DataMember]
        public bool IsSqlAuthentication
        {
            get
            {
                if (_connection == null)
                {
                    return false;
                }
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(_connection.ConnectionString);
                return sb.IntegratedSecurity == false;
            }
        }

        [DataMember]
        public string UserName
        {
            get
            {
                if (_connection == null)
                {
                    return string.Empty;
                }
                SqlConnectionStringBuilder sb = new SqlConnectionStringBuilder(_connection.ConnectionString);
                return sb.UserID;
            }
        }

        public bool IsAvailable
        {
            get
            {
                return _connection != null &&
                    _connection.State == ConnectionState.Open;
            }
        }

        public bool FireInfoMessageEventOnUserErrors
        {
            get
            {
                return _connection.FireInfoMessageEventOnUserErrors;
            }
            set
            {
                _connection.FireInfoMessageEventOnUserErrors = value;
            }
        }

        public void Close()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void InfoMessageInternal(object sender, SqlInfoMessageEventArgs e)
        {
            SqlInfoMessageEventHandler handler = InfoMessage;
            if (handler != null)
            {
                handler(sender, e);
            }
        }

        private void Dispose(bool disposing)
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
