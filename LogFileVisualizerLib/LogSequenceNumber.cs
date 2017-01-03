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

        public override bool Equals(object obj)
        {
            return this == (LogSequenceNumber)obj;
        }

        public override int GetHashCode()
        {
            return _lsnValue.GetHashCode();
        }
    }
}
