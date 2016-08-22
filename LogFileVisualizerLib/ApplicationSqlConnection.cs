﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
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

        public SqlConnection Connection
        {
            get
            {
                return _connection;
            }
        }

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
