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
    }

}
