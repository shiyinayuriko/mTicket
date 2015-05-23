using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTickLibs.IcCardAdapter;

namespace mTickLibs.NfcAdapter
{
    public class MifareUltraLightIcCard : AbstractIcCard
    {
        internal static AbstractIcCard Get(int hdev)
        {
            ulong cid = 0;
            var state = LoaderDll.fw_card(hdev,LoaderDll.RequestModeIdle,ref cid);
            if (state != 0) return null;
            
            List<Byte[]> datas = new List<byte[]>();
            for (Byte i = 0; i < Byte.MaxValue; i++)
            {
                byte[] data = new byte[4];
                state = LoaderDll.fw_read_ultralt(hdev, i, data);
                datas.Add(data);
                if (state != 0) break;
            }
            return new MifareUltraLightIcCard(datas.ToArray());
        }

        private readonly byte[][] _Rawdata;
        private const byte PageStart = 6;
        private const byte PageEnd = 15;

        private MifareUltraLightIcCard(byte[][] datas)
        {
            _Rawdata = datas;
            Id = new byte[7];
            Id[0] = _Rawdata[0][0];
            Id[1] = _Rawdata[0][1];
            Id[2] = _Rawdata[0][2];
            Id[3] = _Rawdata[1][0];
            Id[4] = _Rawdata[1][1];
            Id[5] = _Rawdata[1][2];
            Id[6] = _Rawdata[1][3];

        }
        public override byte[][] GetData()
        {

            List<byte[]> dataList = new List<byte[]>();
            //ONLY use 6-15
            for (int i = PageStart; i <= PageEnd; i++)
            {
                dataList.Add((byte[])_Rawdata[i].Clone());
            }
            return dataList.ToArray(); 
        }

        public override void Save(int hdev, byte[][] bytes)
        {
            if (bytes.Length != (PageEnd - PageStart + 1)) throw new IcCardException("Error bytes length");
            ulong cid = 0;
            var state = LoaderDll.fw_card(hdev, LoaderDll.RequestModeIdle, ref cid);
            if (state != 0) throw new IcCardException(String.Format("fw_card({0},{1},{2}):{3}", hdev, LoaderDll.RequestModeIdle, cid, state));
            state = LoaderDll.fw_read_ultralt(hdev, 0, new byte[4]);
            if (state != 0) throw new IcCardException(String.Format("fw_read_ultralt({0},{1},{2}):{3}", hdev, 0, "new byte[4]", state));

            for (byte i = 0; i <= PageEnd - PageStart; i++)
            {
                if (bytes[i].Length != 4) throw new IcCardException("Error bytes length " + i);

                state = LoaderDll.fw_write_ultralt(hdev, (byte)(i + PageStart), bytes[i]);
                if (state != 0) throw new IcCardException(String.Format("fw_write_ultralt({0},{1},{2}):{3}", hdev, i + PageStart, bytes[i], state));
            }
            
            state = LoaderDll.fw_halt(hdev);
            if (state != 0) throw new IcCardException(String.Format("fw_halt({0}):{1}", hdev, state));
           
        }
    }
}
