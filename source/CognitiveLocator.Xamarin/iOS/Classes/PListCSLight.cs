using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CognitiveLocator.iOS.Classes
{
    public class PListCSLight
    {
        private static List<int> offsetTable = new List<int>();
        private static List<byte> objectTable = new List<byte>();
        private static int refCount = 0;
        private static int objRefSize = 0;
        private static int offsetByteSize = 0;
        private static long offsetTableOffset = 0;

        public static object readPlist(string path)
        {
            using (FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return readPlist(f);
            }
        }

        public static object readPlist(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);

            using (BinaryReader reader = new BinaryReader(stream))
            {
                byte[] data = reader.ReadBytes((int)reader.BaseStream.Length);
                return readBinary(data);
            }
        }

        private static object readBinary(byte[] data)
        {
            offsetTable.Clear();
            List<byte> offsetTableBytes = new List<byte>();
            objectTable.Clear();
            refCount = 0;
            objRefSize = 0;
            offsetByteSize = 0;
            offsetTableOffset = 0;

            List<byte> bList = new List<byte>(data);

            List<byte> trailer = bList.GetRange(bList.Count - 32, 32);

            parseTrailer(trailer);

            objectTable = bList.GetRange(0, (int)offsetTableOffset);

            offsetTableBytes = bList.GetRange((int)offsetTableOffset, bList.Count - (int)offsetTableOffset - 32);

            parseOffsetTable(offsetTableBytes);

            return parseBinary(0);
        }

        private static void parseTrailer(List<byte> trailer)
        {
            offsetByteSize = BitConverter.ToInt32(RegulateNullBytes(trailer.GetRange(6, 1).ToArray(), 4), 0);
            objRefSize = BitConverter.ToInt32(RegulateNullBytes(trailer.GetRange(7, 1).ToArray(), 4), 0);
            byte[] refCountBytes = trailer.GetRange(12, 4).ToArray();
            Array.Reverse(refCountBytes);
            refCount = BitConverter.ToInt32(refCountBytes, 0);
            byte[] offsetTableOffsetBytes = trailer.GetRange(24, 8).ToArray();
            Array.Reverse(offsetTableOffsetBytes);
            offsetTableOffset = BitConverter.ToInt64(offsetTableOffsetBytes, 0);
        }
      
        private static void parseOffsetTable(List<byte> offsetTableBytes)
        {
            for (int i = 0; i < offsetTableBytes.Count; i += offsetByteSize)
            {
                byte[] buffer = offsetTableBytes.GetRange(i, offsetByteSize).ToArray();
                Array.Reverse(buffer);
                offsetTable.Add(BitConverter.ToInt32(RegulateNullBytes(buffer, 4), 0));
            }
        }
      
        private static object parseBinary(int objRef)
        {
            byte header = objectTable[offsetTable[objRef]];
            switch (header & 0xF0)
            {
                case 0:
                    {
                        //If the byte is
                        //0 return null
                        //9 return true
                        //8 return false
                        return (objectTable[offsetTable[objRef]] == 0) ? (object)null : ((objectTable[offsetTable[objRef]] == 9) ? true : false);
                    }
                case 0x10:
                    {
                        return parseBinaryInt(offsetTable[objRef]);
                    }
                case 0x20:
                    {
                        return parseBinaryReal(offsetTable[objRef]);
                    }
                case 0x30:
                    {
                        return parseBinaryDate(offsetTable[objRef]);
                    }
                case 0x40:
                    {
                        return parseBinaryByteArray(offsetTable[objRef]);
                    }
                case 0x50://String ASCII
                    {
                        return parseBinaryAsciiString(offsetTable[objRef]);
                    }
                case 0x60://String Unicode
                    {
                        return parseBinaryUnicodeString(offsetTable[objRef]);
                    }
                case 0xD0:
                    {
                        return parseBinaryDictionary(objRef);
                    }
                case 0xA0:
                    {
                        return parseBinaryArray(objRef);
                    }
            }
            throw new Exception("This type is not supported");
        }

        public static object parseBinaryDate(int headerPosition)
        {
            byte[] buffer = objectTable.GetRange(headerPosition + 1, 8).ToArray();
            Array.Reverse(buffer);
            double appleTime = BitConverter.ToDouble(buffer, 0);
            DateTime result = PlistDateConverter.ConvertFromAppleTimeStamp(appleTime);
            return result;
        }

        private static object parseBinaryInt(int headerPosition)
        {
            int output;
            return parseBinaryInt(headerPosition, out output);
        }

        private static object parseBinaryInt(int headerPosition, out int newHeaderPosition)
        {
            byte header = objectTable[headerPosition];
            int byteCount = (int)Math.Pow(2, header & 0xf);
            byte[] buffer = objectTable.GetRange(headerPosition + 1, byteCount).ToArray();
            Array.Reverse(buffer);
            //Add one to account for the header byte
            newHeaderPosition = headerPosition + byteCount + 1;
            return BitConverter.ToInt32(RegulateNullBytes(buffer, 4), 0);
        }

        private static object parseBinaryReal(int headerPosition)
        {
            byte header = objectTable[headerPosition];
            int byteCount = (int)Math.Pow(2, header & 0xf);
            byte[] buffer = objectTable.GetRange(headerPosition + 1, byteCount).ToArray();
            Array.Reverse(buffer);

            return BitConverter.ToDouble(RegulateNullBytes(buffer, 8), 0);
        }

        private static object parseBinaryAsciiString(int headerPosition)
        {
            int charStartPosition;
            int charCount = getCount(headerPosition, out charStartPosition);

            var buffer = objectTable.GetRange(charStartPosition, charCount);
            return buffer.Count > 0 ? Encoding.ASCII.GetString(buffer.ToArray()) : string.Empty;
        }

        private static object parseBinaryUnicodeString(int headerPosition)
        {
            int charStartPosition;
            int charCount = getCount(headerPosition, out charStartPosition);
            charCount = charCount * 2;

            byte[] buffer = new byte[charCount];
            byte one, two;

            for (int i = 0; i < charCount; i += 2)
            {
                one = objectTable.GetRange(charStartPosition + i, 1)[0];
                two = objectTable.GetRange(charStartPosition + i + 1, 1)[0];

                if (BitConverter.IsLittleEndian)
                {
                    buffer[i] = two;
                    buffer[i + 1] = one;
                }
                else
                {
                    buffer[i] = one;
                    buffer[i + 1] = two;
                }
            }

            return Encoding.Unicode.GetString(buffer);
        }

        private static object parseBinaryByteArray(int headerPosition)
        {
            int byteStartPosition;
            int byteCount = getCount(headerPosition, out byteStartPosition);
            return objectTable.GetRange(byteStartPosition, byteCount).ToArray();
        }

        private static int getCount(int bytePosition, out int newBytePosition)
        {
            byte headerByte = objectTable[bytePosition];
            byte headerByteTrail = Convert.ToByte(headerByte & 0xf);
            int count;
            if (headerByteTrail < 15)
            {
                count = headerByteTrail;
                newBytePosition = bytePosition + 1;
            }
            else
                count = (int)parseBinaryInt(bytePosition + 1, out newBytePosition);
            return count;
        }

        private static byte[] RegulateNullBytes(byte[] value, int minBytes)
        {
            Array.Reverse(value);
            List<byte> bytes = new List<byte>(value);
            for (int i = 0; i < bytes.Count; i++)
            {
                if (bytes[i] == 0 && bytes.Count > minBytes)
                {
                    bytes.Remove(bytes[i]);
                    i--;
                }
                else
                    break;
            }

            if (bytes.Count < minBytes)
            {
                int dist = minBytes - bytes.Count;
                for (int i = 0; i < dist; i++)
                    bytes.Insert(0, 0);
            }

            value = bytes.ToArray();
            Array.Reverse(value);
            return value;
        }

        public static class PlistDateConverter
        {
            public static long timeDifference = 978307200;

            public static long GetAppleTime(long unixTime)
            {
                return unixTime - timeDifference;
            }

            public static long GetUnixTime(long appleTime)
            {
                return appleTime + timeDifference;
            }

            public static DateTime ConvertFromAppleTimeStamp(double timestamp)
            {
                DateTime origin = new DateTime(2001, 1, 1, 0, 0, 0, 0);
                return origin.AddSeconds(timestamp);
            }

            public static double ConvertToAppleTimeStamp(DateTime date)
            {
                DateTime begin = new DateTime(2001, 1, 1, 0, 0, 0, 0);
                TimeSpan diff = date - begin;
                return Math.Floor(diff.TotalSeconds);
            }
        }

        private static object parseBinaryDictionary(int objRef)
        {
            Dictionary<string, object> buffer = new Dictionary<string, object>();
            List<int> refs = new List<int>();
            int rfCount = 0;

            int refStartPosition;
            rfCount = getCount(offsetTable[objRef], out refStartPosition);


            if (rfCount < 15)
                refStartPosition = offsetTable[objRef] + 1;
            else
                refStartPosition = offsetTable[objRef] + 2 + RegulateNullBytes(BitConverter.GetBytes(rfCount), 1).Length;

            for (int i = refStartPosition; i < refStartPosition + rfCount * 2 * objRefSize; i += objRefSize)
            {
                byte[] refBuffer = objectTable.GetRange(i, objRefSize).ToArray();
                Array.Reverse(refBuffer);
                refs.Add(BitConverter.ToInt32(RegulateNullBytes(refBuffer, 4), 0));
            }

            for (int i = 0; i < rfCount; i++)
            {
                buffer.Add((string)parseBinary(refs[i]), parseBinary(refs[i + rfCount]));
            }

            return buffer;
        }

        private static object parseBinaryArray(int objRef)
        {
            List<object> buffer = new List<object>();
            List<int> refs = new List<int>();
            int rfCount = 0;

            int refStartPosition;
            rfCount = getCount(offsetTable[objRef], out refStartPosition);


            if (rfCount < 15)
                refStartPosition = offsetTable[objRef] + 1;
            else
                //The following integer has a header aswell so we increase the refStartPosition by two to account for that.
                refStartPosition = offsetTable[objRef] + 2 + RegulateNullBytes(BitConverter.GetBytes(rfCount), 1).Length;

            for (int i = refStartPosition; i < refStartPosition + rfCount * objRefSize; i += objRefSize)
            {
                byte[] refBuffer = objectTable.GetRange(i, objRefSize).ToArray();
                Array.Reverse(refBuffer);
                refs.Add(BitConverter.ToInt32(RegulateNullBytes(refBuffer, 4), 0));
            }

            for (int i = 0; i < rfCount; i++)
            {
                buffer.Add(parseBinary(refs[i]));
            }

            return buffer;
        }
    }
}