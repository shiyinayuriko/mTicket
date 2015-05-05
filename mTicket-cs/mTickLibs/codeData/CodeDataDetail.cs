using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTickLibs.codeData;

namespace mTicket.Beans
{
    public class CodeDataDetail
    {
        public Dictionary<String, String> info;
        public CheckinData[] checkin;
        public int id;
        public String code;
    }

}
