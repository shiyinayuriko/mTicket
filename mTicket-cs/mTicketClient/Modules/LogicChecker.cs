using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTicket.Beans;
using mTickLibs.IcCardAdapter;
using mTickLibs.Tools;
using MSScriptControl;
using Newtonsoft.Json;

namespace mTicketClient
{
    public class LogicChecker
    {
        private const String PreCheckin = "function pre(tmps) {var tmp = eval('(' + tmps + ')');return checkin(tmp);}";

        private readonly ScriptControlClass _sc;
        public LogicChecker(string script)
        {
            _sc = new ScriptControlClass();
            _sc.Language = "javascript";

            _sc.AddCode(PreCheckin);
            _sc.AddCode(PreCheckinIc);
            _sc.AddCode(script);
        }

        public bool Checkin(CodeDataDetail codeData)
        {
            if (codeData == null) return false;
            string json = JsonConvert.SerializeObject(codeData);
            var result = _sc.Eval("pre('" + json + "')");
            return (bool)result;
        }

        private const String PreCheckinIc = "function preIc(tmps,tmps2,ctime) {var tmp = eval('(' + tmps + ')');var tmp2 = eval('(' + tmps2 + ')');return checkinIc(tmp,tmp2,ctime);}";

        public bool Checkin(CodeDataDetail codeData, IcCardStruct icCard)
        {
            if (codeData == null) return false;
            string json2 = JsonConvert.SerializeObject(icCard);
            string json = JsonConvert.SerializeObject(codeData);
            var result = _sc.Eval("preIc('" + json + "' , '" + json2 + "', "+ TimeTools.CurrentTimeMillis()+")");
            return (bool)result;
        }
    }
}
