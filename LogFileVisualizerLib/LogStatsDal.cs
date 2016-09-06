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
    public class LogStatsDal : DalBase
    {
        public LogStatsDal(string instanceName, string databaseName)
            : base (instanceName, databaseName)
        {
        }

        public LogStatsDal(ApplicationSqlConnection connection)
            : base(connection)
        {
        }

        public List<DbccLogInfoItem> ReadDbccLogInfo(LogSequenceNumber lastKnownLsn, bool useLiteVersion = false)
        {
            string sql;
            SqlParameter[] parameters;

            if (useLiteVersion)
            {
                sql = LogFileVisualizerResources.DbccLoginfoLite;
                parameters = null;
            }
            else
            {
                sql = LogFileVisualizerResources.DbccLoginfo;
                SqlParameter lsnParameter = new SqlParameter("lastKnownLsn", GetParameterValue(lastKnownLsn?.ToString(LsnStringType.DecimalSeparated)));
                parameters = new SqlParameter[1] { lsnParameter };
            }

            using (DataTable table = ExecuteSqlOneResultset(sql, parameters))
            {
                List<DbccLogInfoItem> list = new List<DbccLogInfoItem>();

                foreach (DataRow row in table.Rows)
                {
                    DbccLogInfoItem item = new DbccLogInfoItem();

                    item.FileId = GetObjectValue<int>(row, "FileId");
                    item.FileSize = GetObjectValue<long>(row, "FileSize");
                    item.StartOffset = GetObjectValue<long>(row, "StartOffset");
                    item.VirtualLogFileNumber = GetObjectValue<int>(row, "FSeqNo");
                    item.Status = GetObjectValue<int>(row, "Status");
                    item.Parity = GetObjectValue<byte>(row, "Parity");
                    item.CreateLsn = GetObjectValue<decimal>(row, "CreateLSN");

                    string recoveryUnitIdColumnName = "RecoveryUnitId";
                    if (table.Columns.Contains(recoveryUnitIdColumnName))
                    {
                        item.RecoveryUnitId = GetObjectValue<int>(row, recoveryUnitIdColumnName);
                    }

                    item.ServerName = GetObjectValue<string>(row, "ServerName");
                    item.DatabaseName = GetObjectValue<string>(row, "DatabaseName");
                    item.CaptureTime = GetObjectValue<DateTime>(row, "CaptureTime");

                    if (useLiteVersion == false)
                    {
                        item.LastKnownLsn = new LogSequenceNumber(GetObjectValue<string>(row, "CurrentLsnHex"), LsnStringType.HexidecimalSeparated);
                    }

                    list.Add(item);
                }

                return list;
            }
        }

        public DatabaseInfo GetCurrentDatabaseInfo()
        {
            string sql = @"select db.recovery_model_desc, db.log_reuse_wait_desc from sys.databases db where db.database_id = db_id();";
            using (DataTable table = ExecuteSqlOneResultset(sql))
            {
                if (table.Rows.Count < 1)
                {
                    return null;
                }

                DatabaseInfo dbInfo = new DatabaseInfo();
                dbInfo.RecoveryModelDescription = table.Rows[0]["recovery_model_desc"] as string;
                dbInfo.LogReuseWaitDescription = table.Rows[0]["log_reuse_wait_desc"] as string;

                return dbInfo;
            }
        }
    }
}
