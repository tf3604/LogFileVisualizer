//  Copyright(c) 2016 Brian Hansen.

//  Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
//  documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
//  the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, 
//  and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

//  The above copyright notice and this permission notice shall be included in all copies or substantial portions 
//  of the Software.

//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
//  TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
//  THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
//  CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
//  DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    // TODO: There is a lot common functionality between Dal.cs and DalBase.cs that should be consolidated into a single class.
    public class Dal : IDisposable
    {
        private ApplicationSqlConnection _connection;
        private SqlCommand _currentCommand;
        private object _commandLocker = new object();

        public Dal(ApplicationSqlConnection connection)
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _connection = connection;
        }

        public void ExecuteQueryNoResultSets(string sql, params SqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            try {
                using (SqlCommand command = GetCommand(sql, parameters))
                {
                    command.ExecuteNonQuery();
                }
            }
            catch (SqlException ex) when (ex.Class == 11 && ex.Number == 0)
            {
                // User cancelled query.
                return;
            }
        }

        public DataTable ExecuteQueryOneResultSet(string sql, params SqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            using (SqlCommand command = GetCommand(sql, parameters))
            {
                lock(_commandLocker)
                {
                    _currentCommand = command;
                }
                try
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
                catch (SqlException ex) when (ex.Class == 11 && ex.Number == 0)
                {
                    // User cancelled query.
                    return new DataTable();
                }
                finally
                {
                    lock(_commandLocker)
                    {
                        _currentCommand = null;
                    }
                }
            }
        }

        public DataSet ExecuteQueryMultipleResultSets(string sql, params SqlParameter[] parameters)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            try
            {
                using (SqlCommand command = GetCommand(sql, parameters))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataSet set = new DataSet();
                        try
                        {
                            adapter.Fill(set);
                            return set;
                        }
                        catch
                        {
                            if (set != null)
                            {
                                set.Dispose();
                            }
                            throw;
                        }
                    }
                }
            }
            catch (SqlException ex) when (ex.Class == 11 && ex.Number == 0)
            {
                // User cancelled query.
                return new DataSet();
            }
        }

        public void Cancel()
        {
            lock(_commandLocker)
            {
                if (_currentCommand != null)
                {
                    _currentCommand.Cancel();
                    _currentCommand = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                //if (_connection != null)
                //{
                //    _connection.Dispose();
                //    _connection = null;
                //}
            }
        }

        private SqlCommand GetCommand(string sql, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(sql, _connection.Connection);
            try
            {
                if (parameters != null &&
                    parameters.Length > 0)
                {
                    command.Parameters.AddRange(parameters);
                }

                command.CommandTimeout = 0;
                _currentCommand = command;
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
    }
}
