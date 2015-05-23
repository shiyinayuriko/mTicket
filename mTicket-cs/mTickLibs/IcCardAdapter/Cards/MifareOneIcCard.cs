using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTickLibs.IcCardAdapter;

namespace mTickLibs.NfcAdapter
{
    public class MifareOneIcCard : AbstractIcCard
    {

        public override byte[][] GetData()
        {
            throw new NotImplementedException();
        }

        public override void Save(int hdev, byte[][] bytes)
        {
            throw new NotImplementedException();
        }

        public static AbstractIcCard Get()
        {
            throw new NotImplementedException();
        }
    }
}
