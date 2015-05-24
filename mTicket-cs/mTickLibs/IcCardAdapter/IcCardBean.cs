using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace mTickLibs.IcCardAdapter
{
    public class IcCardBean
    {
        public static byte[] ToBytes(IcCardStruct stract)
        {
            int size = Marshal.SizeOf(stract);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.StructureToPtr(stract, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static IcCardStruct FromByte(byte[] bytes)
        {
            IcCardStruct strcutType = new IcCardStruct();
            int size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                return (IcCardStruct) Marshal.PtrToStructure(buffer, strcutType.GetType());
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        public static byte[] GetBytesLine(byte[][] bytes)
        {
            int c = 0;
            int size = Marshal.SizeOf(new IcCardStruct());
            byte[] ret = new byte[size];
            for (int i = 0; i < bytes.Length; i++)
            {
                for (int j = 0; j < bytes[i].Length; j++)
                {
                    ret[c++] = bytes[i][j];
                    if(c==size) goto l;
                }
            }
            l:
            return ret;
        }

        public static byte[][] CopyToBytes(byte[] source,byte[][] bytes)
        {
            byte[][] ret = new byte[bytes.Length][];
            int c = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                ret[i] = (byte[]) bytes[i].Clone();
                for (int j = 0; j < bytes[i].Length && c < source.Length; j++)
                {
                    ret[i][j] = source[c++];
                }
            }
            return ret;
        }
    }
    [Serializable]
    public struct IcCardStruct
    {
        public int id;
        public bool canIn;
        public long lastTime;
    }
}
