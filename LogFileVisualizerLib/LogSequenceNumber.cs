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

                case LsnStringType.Hexadecimal:
                    _lsnValue = LsnConverter.HexadecimalToDecimal(lsnValue);
                    break;

                case LsnStringType.Decimal:
                    _lsnValue = decimal.Parse(lsnValue);
                    break;

                default:
                    throw new ArgumentException("Unknown LsnStringType specified.", nameof(stringType));
            }
        }

        public static bool operator ==(LogSequenceNumber lsn1, LogSequenceNumber lsn2)
        {
            if ((object)lsn1 == null &&
                (object)lsn2 == null)
            {
                return true;
            }

            if (((object)lsn1 == null && (object)lsn2 != null) |
                ((object)lsn1 != null && (object)lsn2 == null))
            {
                return false;
            }

            return lsn1._lsnValue == lsn2._lsnValue;
        }

        public static bool operator !=(LogSequenceNumber lsn1, LogSequenceNumber lsn2)
        {
            return !(lsn1 == lsn2);
        }

        public static bool operator >(LogSequenceNumber lsn1, LogSequenceNumber lsn2)
        {
            if ((object)lsn1 == null &&
                (object)lsn2 == null)
            {
                return true;
            }

            if (((object)lsn1 == null && (object)lsn2 != null) |
                ((object)lsn1 != null && (object)lsn2 == null))
            {
                return false;
            }

            return lsn1._lsnValue > lsn2._lsnValue;
        }

        public static bool operator <(LogSequenceNumber lsn1, LogSequenceNumber lsn2)
        {
            if ((object)lsn1 == null &&
                (object)lsn2 == null)
            {
                return true;
            }

            if (((object)lsn1 == null && (object)lsn2 != null) |
                ((object)lsn1 != null && (object)lsn2 == null))
            {
                return false;
            }

            return lsn1._lsnValue < lsn2._lsnValue;
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

                case LsnStringType.Decimal:
                    return _lsnValue.ToString();

                default:
                    throw new ArgumentException("Unknown LsnStringType specified.", nameof(stringType));
            }
        }

        public enum LsnStringType
        {
            HexidecimalSeparated,
            DecimalSeparated,
            Hexadecimal,
            Decimal
        }
    }
}
