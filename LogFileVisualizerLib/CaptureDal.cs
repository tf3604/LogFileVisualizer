using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    internal class CaptureDal : DalBase
    {
        private Dictionary<string, int> _captureSystemCache = new Dictionary<string, int>();
        private Dictionary<string, int> _captureDatabaseCache = new Dictionary<string, int>();

        public CaptureDal(string instanceName, string databaseName)
            : base (instanceName, databaseName)
        {
            InitializeCaptureSystemCache();
            InitializeCaptureDatabaseCache();
        }

        public void SaveDbccLoginfoCapture(List<DbccLogInfoItem> dbccLogInfo)
        {
            using (DataTable table = new DataTable())
            {
                table.Columns.Add("CaptureEventId", typeof(int));
                table.Columns.Add("RecoveryUnitId", typeof(int));
                table.Columns.Add("FileId", typeof(int));
                table.Columns.Add("FileSize", typeof(long));
                table.Columns.Add("StartOffset", typeof(long));
                table.Columns.Add("VirtualLogFileNumber", typeof(int));
                table.Columns.Add("Status", typeof(int));
                table.Columns.Add("Parity", typeof(byte));
                table.Columns.Add("CreateLsn", typeof(decimal));

                int? captureEventId = null;

                foreach (DbccLogInfoItem item in dbccLogInfo)
                {
                    DataRow row = table.NewRow();

                    if (captureEventId == null)
                    {
                        captureEventId = CreateCaptureEvent(item.ServerName, item.DatabaseName, item.CaptureTime, item.LastKnownLsn);
                    }

                    row["CaptureEventId"] = GetParameterValue(captureEventId.Value);
                    row["RecoveryUnitId"] = GetParameterValue(item.RecoveryUnitId);
                    row["FileId"] = GetParameterValue(item.FileId);
                    row["FileSize"] = GetParameterValue(item.FileSize);
                    row["StartOffset"] = GetParameterValue(item.StartOffset);
                    row["VirtualLogFileNumber"] = GetParameterValue(item.VirtualLogFileNumber);
                    row["Status"] = GetParameterValue(item.Status);
                    row["Parity"] = GetParameterValue(item.Parity);
                    row["CreateLsn"] = GetParameterValue(item.CreateLsn);

                    table.Rows.Add(row);
                }

                BulkInsert("DbccLoginfoCapture", table);
            }
        }

        public LogSequenceNumber ReadMostRecentLsn(string serverName, string databaseName)
        {
            if (string.IsNullOrEmpty(serverName))
            {
                throw new ArgumentNullException(nameof(serverName));
            }
            if (string.IsNullOrEmpty(databaseName))
            {
                throw new ArgumentNullException(nameof(databaseName));
            }

            int serverId = ReadCaptureSystemCached(serverName);
            int databaseId = ReadCaptureDatabaseCached(databaseName);

            string sql = @"select top 1 CurrentLsn from CaptureEvent where CaptureSystemId = @CaptureSystemId and CaptureDatabaseId = @CaptureDatabaseId order by CaptureTime desc;";
            SqlParameter serverParameter = new SqlParameter("CaptureSystemId", serverId);
            SqlParameter databaseParameter = new SqlParameter("CaptureDatabaseId", databaseId);

            object lastKnownLsn = ExecuteSqlScalar(sql, serverParameter, databaseParameter);
            if (lastKnownLsn == null)
            {
                return null;
            }

            LogSequenceNumber lsn = new LogSequenceNumber((decimal)lastKnownLsn);
            return lsn;
        }

        public int ReadCaptureSystemCached(string serverName)
        {
            serverName = serverName.ToLowerInvariant();
            if (_captureSystemCache.ContainsKey(serverName))
            {
                return _captureSystemCache[serverName];
            }

            string sql = @"insert CaptureSystem (ServerName) values (@ServerName); select cast(scope_identity() as int) ID;";
            SqlParameter nameParameter = new SqlParameter("ServerName", serverName);

            int id = (int)ExecuteSqlScalar(sql, nameParameter);
            _captureSystemCache.Add(serverName, id);
            return id;
        }

        public int CreateCaptureEvent(string serverName, string databaseName, DateTime captureTime, LogSequenceNumber currentLsn)
        {
            string sql = @"insert CaptureEvent (CaptureSystemId, CaptureDatabaseId, CaptureTime, CurrentLsn) values (@CaptureSystemId, @CaptureDatabaseId, @CaptureTime, @CurrentLsn); select cast(scope_identity() as int) ID;";
            SqlParameter systemParameter = new SqlParameter("CaptureSystemId", ReadCaptureSystemCached(serverName));
            SqlParameter databaseParameter = new SqlParameter("CaptureDatabaseId", ReadCaptureDatabaseCached(databaseName));
            SqlParameter timeParameter = new SqlParameter("CaptureTime", captureTime);
            timeParameter.SqlDbType = SqlDbType.DateTime2;
            SqlParameter lsnParameter = new SqlParameter("CurrentLsn", currentLsn.ToDecimal());

            int id = (int)ExecuteSqlScalar(sql, systemParameter, databaseParameter, timeParameter, lsnParameter);
            return id;
        }

        public int ReadCaptureDatabaseCached(string databaseName)
        {
            databaseName = databaseName.ToLowerInvariant();
            if (_captureDatabaseCache.ContainsKey(databaseName))
            {
                return _captureDatabaseCache[databaseName];
            }

            string sql = @"insert CaptureDatabase (DatabaseName) values (@DatabaseName); select cast(scope_identity() as int) ID;";
            SqlParameter nameParameter = new SqlParameter("DatabaseName", databaseName);

            int id = (int)ExecuteSqlScalar(sql, nameParameter);
            _captureDatabaseCache.Add(databaseName, id);
            return id;
        }

        private void InitializeCaptureSystemCache()
        {
            string sql = @"select CaptureSystemId, ServerName from CaptureSystem;";
            using (DataTable table = ExecuteSqlOneResultset(sql))
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = GetObjectValue<int>(row, "CaptureSystemId");
                    string serverName = GetObjectValue<string>(row, "ServerName");

                    _captureSystemCache.Add(serverName.ToLowerInvariant(), id);
                }
            }
        }

        private void InitializeCaptureDatabaseCache()
        {
            string sql = @"select CaptureDatabaseId, DatabaseName from CaptureDatabase;";
            using (DataTable table = ExecuteSqlOneResultset(sql))
            {
                foreach (DataRow row in table.Rows)
                {
                    int id = GetObjectValue<int>(row, "CaptureDatabaseId");
                    string databaseName = GetObjectValue<string>(row, "DatabaseName");

                    _captureDatabaseCache.Add(databaseName.ToLowerInvariant(), id);
                }
            }
        }
    }
}
