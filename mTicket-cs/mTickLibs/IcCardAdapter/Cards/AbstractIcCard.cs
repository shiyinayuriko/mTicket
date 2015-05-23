using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTickLibs.IcCardAdapter
{
    public abstract class AbstractIcCard
    {
        protected byte[] Id;

        public virtual byte[] GetIdBytes()
        {
            return Id;
        }

        public virtual string GetIdHex()
        {
            string str = "";
            for (int i =0;i<Id.Length;i++)
            {
                str += String.Format(":{0:X2}", Id[i]);
            }
            return str.Substring(1);
        }

        public abstract byte[][] GetData();

        public abstract void Save(int hdev, byte[][] bytes);
    }
}
