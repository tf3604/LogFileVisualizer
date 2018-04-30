//  Copyright(c) 2016-2017 Brian Hansen.

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
        private Version _sqlVersion;
        private static Version _minDmvLogInfoVersion = new Version("13.0.5026.0");

        public LogStatsDal(string instanceName, string databaseName)
            : base(instanceName, databaseName)
        {
            GetVersion();
        }

        public LogStatsDal(ApplicationSqlConnection connection)
            : base(connection)
        {
            GetVersion();
        }

        public List<DbccLogInfoItem> ReadDbccLogInfo(LogSequenceNumber lastKnownLsn, bool useLiteVersion = false, bool forceDbccLoginfo = false)
        {
            string sql;
            SqlParameter[] parameters = null;

            if (forceDbccLoginfo || _sqlVersion < _minDmvLogInfoVersion)
            {
                if (useLiteVersion)
                {
                    sql = LogFileVisualizerResources.DbccLoginfoLite;
                }
                else
                {
                    sql = LogFileVisualizerResources.DbccLoginfo;
                    SqlParameter lsnParameter = new SqlParameter("lastKnownLsn", GetParameterValue(lastKnownLsn?.ToString(LsnStringType.DecimalSeparated)));
                    parameters = new SqlParameter[1] { lsnParameter };
                }
            }
            else
            {
                // Version >= 2016 SP2 and forceDbccLoginfo is false
                sql = @"set deadlock_priority low;

select cast(file_id as int) FileId,
       cast(vlf_size_mb * 1024 * 1024 as bigint) FileSize,
       vlf_begin_offset StartOffset,
       cast(vlf_sequence_number as int) FSeqNo,
       vlf_active Active,
       vlf_status Status,
       cast(vlf_parity as tinyint) Parity,
       vlf_create_lsn CreateLSN,
	   @@SERVERNAME ServerName,
	   db_name() DatabaseName,
	   sysdatetime() CaptureTime,
	   cast(null as nvarchar(50)) CurrentLsnHex
from sys.dm_db_log_info(null);";
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

                    object createLsnObject = row["CreateLSN"];
                    Type createLsnType = createLsnObject.GetType();
                    if (createLsnType == typeof(decimal))
                    {
                        item.CreateLsn = GetObjectValue<decimal>(createLsnObject);
                    }
                    else if (createLsnType == typeof(string))
                    {
                        string createLsnTypeHexSeparated = GetObjectValue<string>(createLsnObject);
                        LogSequenceNumber createLsn = new LogSequenceNumber(createLsnTypeHexSeparated, LsnStringType.HexidecimalSeparated);
                        item.CreateLsn = createLsn.ToDecimal();
                    }

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

        private void GetVersion()
        {
            string sql = @"select serverproperty('ProductVersion') Version;";

            object versionObject = ExecuteSqlScalar(sql);
            string versionString = versionObject as string;

            Version version;
            if (Version.TryParse(versionString, out version))
            {
                _sqlVersion = version;
            }
        }
    }
}
