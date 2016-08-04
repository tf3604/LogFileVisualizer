using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    public class LogSequenceNumber
    {
        private decimal _lsnValue;

        public LogSequenceNumber(decimal lsnValue)
        {
            _lsnValue = lsnValue;
        }

        public LogSequenceNumber(byte[] lsnBytes)
        {
            _lsnValue = LsnConverter.BytesToDecimal(lsnBytes);
        }

        public LogSequenceNumber(string lsnValue, LsnStringType stringType)
        {
            switch (stringType)
            {
                case LsnStringType.DecimalSeparated:
                    _lsnValue = LsnConverter.DecimalSeparatedToDecimal(lsnValue);
                    break;

                case LsnStringType.HexidecimalSeparated:
                    _lsnValue = LsnConverter.HexSeparatedToDecimal(lsnValue);
                    break;

                default:
                    throw new ArgumentException("Unknown LsnStringType specified.", nameof(stringType));
            }
        }

        public decimal ToDecimal()
        {
            return _lsnValue;
        }

        public byte[] ToBytes()
        {
            return LsnConverter.DecimalToBytes(_lsnValue);
        }

        public string ToString(LsnStringType stringType)
        {
            switch (stringType)
            {
                case LsnStringType.DecimalSeparated:
                    return LsnConverter.DecimalToDecimalSeparated(_lsnValue);

                case LsnStringType.HexidecimalSeparated:
                    return LsnConverter.DecimalToHexSeparated(_lsnValue);

                case LsnStringType.Hexadecimal:
                    return LsnConverter.DecimalToHexadecimal(_lsnValue);

                default:
                    throw new ArgumentException("Unknown LsnStringType specified.", nameof(stringType));
            }
        }

        public enum LsnStringType
        {
            HexidecimalSeparated,
            DecimalSeparated,
            Hexadecimal
        }
    }
}
