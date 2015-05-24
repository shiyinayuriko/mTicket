using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace mTicket
{
    public class CodeTable
    {
        public string[] columns;
        public CodeInfo[] infos;

        public string GetJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class CodeInfo
    {
        public int id;
        public string code;
        public string[] info;

        public override bool Equals(object obj)
        {
            if (obj is CodeInfo)
            {
                return ((CodeInfo) obj).id == this.id;
            }
            else return false;
        }

        public void SetCode(string code)
        {
            this.code = code;
            info[1] = code;
        }
    }

}
