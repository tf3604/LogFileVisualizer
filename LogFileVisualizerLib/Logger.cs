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
