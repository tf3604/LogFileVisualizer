using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    public static class Logger
    {
        private static object _logLocker = new object();

        static Logger()
        {
            DefaultLogLevel = LogLevel.Informational;
            ThresholdLogLevel = LogLevel.Informational;
            LogFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "LogFileVisualizer.log");
        }

        public static LogLevel DefaultLogLevel
        {
            get;
            set;
        }

        public static LogLevel ThresholdLogLevel
        {
            get;
            set;
        }

        public static string LogFileName
        {
            get;
            set;
        }

        public static void Write(LogLevel logLevel, string format, params object[] args)
        {
            if (logLevel >= ThresholdLogLevel)
            {
                string message = string.Format(format, args);
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

                lock (_logLocker)
                {
                    FileStream stream = null;
                    try
                    {
                        stream = new FileStream(LogFileName, FileMode.Append, FileAccess.Write, FileShare.Read);
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            stream = null;
                            writer.WriteLine("{0}: {1}: {2}", time, logLevel, message);
                        }
                    }
                    catch
                    {
                        if (stream != null)
                        {
                            stream.Dispose();
                        }
                        throw;
                    }
                }
            }
        }

        public static void Write(LogLevel logLevel, string message)
        {
            Write(logLevel, "{0}", message);
        }

        public static void Write(string format, params object[] args)
        {
            Write(DefaultLogLevel, format, args);
        }

        public static void Write(string message)
        {
            Write(DefaultLogLevel, message);
        }
    }
}
