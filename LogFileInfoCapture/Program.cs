using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogFileVisualizerLib;

namespace LogFileInfoCapture
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Logger.Write(LogLevel.Informational, "Starting capture of MMS.");
                LogStatsCapture.CaptureDbccLogInfo(@"prodtrn\transdb", "MMS", @"ci1460\sql2014", "LogStatsCapture");
                Logger.Write(LogLevel.Informational, "Starting capture of MMS_DATAMODULES.");
                LogStatsCapture.CaptureDbccLogInfo(@"prodtrn\transdb", "MMS_DATAMODULES", @"ci1460\sql2014", "LogStatsCapture");
                Logger.Write(LogLevel.Informational, "Capture complete.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("{0}", ex);
                Logger.Write(LogLevel.Fatal, ex.ToString());
            }
        }
    }
}
