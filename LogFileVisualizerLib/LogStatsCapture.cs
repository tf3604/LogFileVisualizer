using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    public static class LogStatsCapture
    {
        public static void CaptureDbccLogInfo(string sourceInstanceName, string sourceDatabaseName, string targetInstanceName, string targetDatabaseName)
        {
            using (CaptureDal captureDal = new CaptureDal(targetInstanceName, targetDatabaseName))
            {
                Logger.Write(LogLevel.Informational, "Reading most recent LSN.");
                LogSequenceNumber lsn = captureDal.ReadMostRecentLsn(sourceInstanceName, sourceDatabaseName);

                List<DbccLogInfoItem> dbccLogInfo;
                using (LogStatsDal sourceSystemDal = new LogStatsDal(sourceInstanceName, sourceDatabaseName))
                {
                    Logger.Write(LogLevel.Informational, "Reading LOGINFO.");
                    dbccLogInfo = sourceSystemDal.ReadDbccLogInfo(lsn);
                }

                Logger.Write(LogLevel.Informational, "Writing captured log information.");
                captureDal.SaveDbccLoginfoCapture(dbccLogInfo);
            }
        }
    }
}
