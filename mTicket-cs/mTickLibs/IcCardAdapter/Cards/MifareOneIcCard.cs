using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTickLibs.IcCardAdapter;

namespace mTickLibs.NfcAdapter
{
    public class MifareOneIcCard : AbstractIcCard
    {
        private static readonly byte[] key = {0xff,0xff,0xff,0xff,0xff,0xff};
        public static AbstractIcCard Get(int hdev)
        {
            ulong cid = 0;
            var state = LoaderDll.fw_card(hdev, LoaderDll.RequestModeIdle, ref cid);
            if (state != 0) return null;

            List<Byte[]> datas = new List<byte[]>();
            for (byte sector = 0; sector < 16;sector++)
            {
                //TODO need more test
                state = LoaderDll.fw_load_key(hdev, LoaderDll.LoadKeyModeKeyA, sector, key);
                if (state != 0) return null;

                state = LoaderDll.fw_authentication(hdev, LoaderDll.LoadKeyModeKeyA, sector);
                if (state != 0) return null;

                for (byte i = 0; i < 4; i++)
                {
                    byte[] data = new byte[16];
                    state = LoaderDll.fw_read(hdev, (byte) (sector*4 + i), data);
                    datas.Add(data);
                }
                if (state != 0) break;
            }
            return new MifareOneIcCard(datas.ToArray());
        }

        private static readonly byte[] addrList = {4,5,6};
        private readonly byte[][] _Rawdata;
        private MifareOneIcCard(byte[][] datas)
        {
            _Rawdata = datas;
            Id = new byte[4];
            Id[0] = _Rawdata[0][0];
            Id[1] = _Rawdata[0][1];
            Id[2] = _Rawdata[0][2];
            Id[3] = _Rawdata[0][3];
        }
        public override byte[][] GetData()
        {
            List<byte[]> dataList = new List<byte[]>();
            //ONLY use 6-15
            for (int i = 0; i <= addrList.Length; i++)
            {
                dataList.Add((byte[])_Rawdata[i].Clone());
            }
            return dataList.ToArray(); 
        }

        public override void Save(int hdev, byte[][] bytes)
        {
            if (bytes.Length != addrList.Length) throw new IcCardException("Error bytes length");
            ulong cid = 0;
            var state = LoaderDll.fw_card(hdev, LoaderDll.RequestModeIdle, ref cid);
            if (state != 0) throw new IcCardException(String.Format("fw_card({0},{1},{2}):{3}", hdev, LoaderDll.RequestModeIdle, cid, state));

            for (byte i = 0; i < addrList.Length; i++)
            {
                if (bytes[i].Length != 16) throw new IcCardException("Error bytes length " + i);

                state = LoaderDll.fw_load_key(hdev, LoaderDll.LoadKeyModeKeyA, (byte) (addrList[i]/4), key);
                if (state != 0) throw new IcCardException(String.Format("fw_load_key({0},{1},{2},{3}):{4}", hdev, LoaderDll.LoadKeyModeKeyA, addrList[i] / 4, bytes[i], state));

                state = LoaderDll.fw_authentication(hdev,  LoaderDll.LoadKeyModeKeyA, (byte)(addrList[i] / 4));
                if (state != 0) throw new IcCardException(String.Format("fw_authentication({0},{1},{2}):{3}", hdev, LoaderDll.LoadKeyModeKeyA, addrList[i] / 4, state));

                state = LoaderDll.fw_write(hdev, addrList[i], bytes[i]);
                if (state != 0) throw new IcCardException(String.Format("fw_write({0},{1},{2}):{3}", hdev, addrList[i], bytes[i], state));
            }

            state = LoaderDll.fw_halt(hdev);
            if (state != 0) throw new IcCardException(String.Format("fw_halt({0}):{1}", hdev, state));
        }
    }
}
