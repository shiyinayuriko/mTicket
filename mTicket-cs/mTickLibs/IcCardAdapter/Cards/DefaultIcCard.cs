using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTickLibs.IcCardAdapter;

namespace mTickLibs.NfcAdapter
{
    public class DefaultIcCard : AbstractIcCard
    {

        public override byte[][] GetData()
        {
            return new byte[0][];
        }

        public override void Save(int hdev, byte[][] bytes)
        {
            return;
        }

        internal static AbstractIcCard Get()
        {
            return new DefaultIcCard()
            {
                Id = new byte[] { 0 }
            };
        }
    }
}
