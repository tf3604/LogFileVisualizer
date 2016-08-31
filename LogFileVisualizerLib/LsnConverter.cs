using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogFileVisualizerLib
{
    public static class LsnConverter
    {
        public static string HexSeparatedToDecimalSeparated(string hexSeparated)
        {
            if (string.IsNullOrEmpty(hexSeparated))
            {
                throw new ArgumentNullException(nameof(hexSeparated));
            }

            string[] parts = hexSeparated.Split(':');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Hex-separated LSN must contain exactly three parts separated by colons.", nameof(hexSeparated));
            }

            int[] integerParts = new int[3];
            integerParts[0] = HexToInt(parts[0]);
            integerParts[1] = HexToInt(parts[1]);
            integerParts[2] = HexToInt(parts[2]);

            string decimalSeparated = integerParts[0].ToString() + ":" + integerParts[1].ToString() + ":" + integerParts[2].ToString();
            return decimalSeparated;
        }

        public static string DecimalSeparatedToHexSeparated(string decimalSeparated)
        {
            if (string.IsNullOrEmpty(decimalSeparated))
            {
                throw new ArgumentNullException(nameof(decimalSeparated));
            }

            string[] parts = decimalSeparated.Split(':');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Decimal-separated LSN must contain exactly three parts separated by colons.", nameof(decimalSeparated));
            }

            int[] integerParts = new int[3];
            integerParts[0] = int.Parse(parts[0]);
            integerParts[1] = int.Parse(parts[1]);
            integerParts[2] = int.Parse(parts[2]);

            string[] hexParts = new string[3];
            hexParts[0] = IntToHex(integerParts[0], 8);
            hexParts[1] = IntToHex(integerParts[1], 8);
            hexParts[2] = IntToHex(integerParts[2], 4);

            string hexSeparated = hexParts[0] + ":" + hexParts[1] + ":" + hexParts[2];
            return hexSeparated;
        }

        public static decimal HexSeparatedToDecimal(string hexSeparated)
        {
            if (string.IsNullOrEmpty(hexSeparated))
            {
                throw new ArgumentNullException(nameof(hexSeparated));
            }

            string[] parts = hexSeparated.Split(':');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Hex-separated LSN must contain exactly three parts separated by colons.", nameof(hexSeparated));
            }

            int[] integerParts = new int[3];
            integerParts[0] = HexToInt(parts[0]);
            integerParts[1] = HexToInt(parts[1]);
            integerParts[2] = HexToInt(parts[2]);

            decimal decimalValue = integerParts[0] * 1000000000000000m + integerParts[1] * 100000m + integerParts[2];
            return decimalValue;
        }

        public static decimal BytesToDecimal(byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (bytes.Length != 10)
            {
                throw new ArgumentException("LSN byte array must be of length 10.", nameof(bytes));
            }

            decimal vlfNumber = BitConverter.ToInt32(bytes, 0);
            decimal logBlockNumber = BitConverter.ToInt32(bytes, 4);
            decimal logRecordNumber = BitConverter.ToInt16(bytes, 8);

            decimal decimalValue = vlfNumber * 1000000000000000m + logBlockNumber * 100000m + logRecordNumber;
            return decimalValue;

        }

        public static decimal DecimalSeparatedToDecimal(string decimalSeparated)
        {
            if (string.IsNullOrEmpty(decimalSeparated))
            {
                throw new ArgumentNullException(nameof(decimalSeparated));
            }

            string[] parts = decimalSeparated.Split(':');
            if (parts.Length != 3)
            {
                throw new ArgumentException("Hex-separated LSN must contain exactly three parts separated by colons.", nameof(decimalSeparated));
            }

            int[] integerParts = new int[3];
            integerParts[0] = int.Parse(parts[0]);
            integerParts[1] = int.Parse(parts[1]);
            integerParts[2] = int.Parse(parts[2]);

            decimal decimalValue = integerParts[0] * 1000000000000000m + integerParts[1] * 100000m + integerParts[2];
            return decimalValue;
        }

        public static decimal HexadecimalToDecimal(string hexadecimal)
        {
            if (string.IsNullOrEmpty(hexadecimal))
            {
                throw new ArgumentNullException(nameof(hexadecimal));
            }

            hexadecimal = hexadecimal.ToLowerInvariant();

            if (hexadecimal.StartsWith("0x") == false)
            {
                throw new ArgumentException("Hexadecimal LSN must start with '0x'.", nameof(hexadecimal));
            }

            // Strip off "0x"
            hexadecimal = hexadecimal.Substring(2);

            byte[] array = HexToByteArray(hexadecimal);
            decimal decimalValue = BytesToDecimal(array);

            return decimalValue;
        }

        public static string DecimalToHexSeparated(decimal lsnValue)
        {
            int vlfNumber = (int)(lsnValue / 1000000000000000m);
            int logBlockNumber = (int)((lsnValue % 1000000000000000m) / 100000m);
            int logRecordNumber = (int)(lsnValue % 100000m);

            string hexSeparated = IntToHex(vlfNumber, 8) + ":" + IntToHex(logBlockNumber, 8) + ":" + IntToHex(logRecordNumber, 4);
            return hexSeparated;
        }

        public static string DecimalToHexadecimal(decimal lsnValue)
        {
            int vlfNumber = (int)(lsnValue / 1000000000000000m);
            int logBlockNumber = (int)((lsnValue % 1000000000000000m) / 100000m);
            int logRecordNumber = (int)(lsnValue % 100000m);

            string hexadecimal = "0x" + IntToHex(vlfNumber, 8) + IntToHex(logBlockNumber, 8) + IntToHex(logRecordNumber, 4);
            return hexadecimal;
        }

        public static string DecimalToDecimalSeparated(decimal lsnValue)
        {
            int vlfNumber = (int)(lsnValue / 1000000000000000m);
            int logBlockNumber = (int)((lsnValue % 1000000000000000m) / 100000m);
            int logRecordNumber = (int)(lsnValue % 100000m);

            string decimalSeparated = vlfNumber.ToString() + ":" + logBlockNumber.ToString() + ":" + logRecordNumber.ToString();
            return decimalSeparated;
        }

        public static byte[] DecimalToBytes(decimal lsnValue)
        {
            int vlfNumber = (int)(lsnValue / 1000000000000000m);
            int logBlockNumber = (int)((lsnValue % 1000000000000000m) / 100000m);
            int logRecordNumber = (int)(lsnValue % 100000m);

            List<byte> bytes = new List<byte>();
            bytes.AddRange(IntToByte(vlfNumber, 4));
            bytes.AddRange(IntToByte(logBlockNumber, 4));
            bytes.AddRange(IntToByte(logRecordNumber, 2));

            return bytes.ToArray();
        }

        private static int HexToInt(string hex)
        {
            return int.Parse(hex, NumberStyles.HexNumber);
        }

        private static byte[] HexToByteArray(string hex)
        {
            if (hex.Length % 2 != 0)
            {
                hex = "0" + hex;
            }

            List<byte> bytes = new List<byte>();
            for (int index = 0; index < hex.Length; index += 2)
            {
                byte value = (byte)HexToInt(hex.Substring(index, 2));
                bytes.Add(value);
            }

            return bytes.ToArray();
        }

        private static string IntToHex(int value, int nbrCharacters)
        {
            string hex = value.ToString("x");
            if (hex.Length > nbrCharacters)
            {
                throw new ApplicationException("Cannot truncate to specified number of chars.");
            }
            else if (hex.Length < nbrCharacters)
            {
                hex = new string('0', nbrCharacters - hex.Length) + hex;
            }

            return hex;
        }

        private static List<byte> IntToByte(int value, int nbrBytes)
        {
            List<byte> bytes = BitConverter.GetBytes(value).ToList();
            bytes.Reverse();
            if (bytes.Count > nbrBytes)
            {
                while (bytes.Count > nbrBytes)
                {
                    if (bytes.Count > 0 &&
                        bytes[0] == 0)
                    {
                        bytes.RemoveAt(0);
                    }
                    else
                    {
                        throw new ApplicationException("Cannot truncate to specified number of bytes.");
                    }
                }
            }
            else
            {
                while (bytes.Count < nbrBytes)
                {
                    bytes.Insert(0, 0);
                }
            }

            return bytes;
        }
    }
}
