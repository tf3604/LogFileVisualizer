using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    class DbccLogInfoItem
    {
        public int RecoveryUnitId
        {
            get; set;
        }

        public int FileId
        {
            get; set;
        }

        public long FileSize
        {
            get; set;
        }

        public long StartOffset
        {
            get; set;
        }

        public int VirtualLogFileNumber
        {
            get; set;
        }

        public int Status
        {
            get; set;
        }

        public byte Parity
        {
            get; set;
        }

        public decimal CreateLsn
        {
            get; set;
        }

        public string ServerName
        {
            get; set;
        }

        public string DatabaseName
        {
            get; set;
        }

        public DateTime CaptureTime
        {
            get; set;
        }

        public LogSequenceNumber LastKnownLsn
        {
            get; set;
        }
    }
}
