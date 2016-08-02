using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    internal class LogStatsDal : DalBase
    {
        public LogStatsDal(string instanceName, string databaseName)
            : base (instanceName, databaseName)
        {
        }

        public List<DbccLogInfoItem> ReadDbccLogInfo(LogSequenceNumber lastKnownLsn)
        {
            string sql = LogFileVisualizerResources.DbccLoginfo;
            SqlParameter lsnParameter = new SqlParameter("lastKnownLsn", GetParameterValue(lastKnownLsn?.ToString(LogSequenceNumber.LsnStringType.DecimalSeparated)));

            using (DataTable table = ExecuteSqlOneResultset(sql, lsnParameter))
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
                    item.LastKnownLsn = new LogSequenceNumber(GetObjectValue<string>(row, "CurrentLsnHex"), LogSequenceNumber.LsnStringType.HexidecimalSeparated);

                    list.Add(item);
                }

                return list;
            }
        }
    }
}
