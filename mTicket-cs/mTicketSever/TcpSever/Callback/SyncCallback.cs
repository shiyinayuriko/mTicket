using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mTickLibs.codeData;
using Newtonsoft.Json;

namespace mTicket
{
    class SyncCallback : ICallback
    {
        private readonly DataBaseHandler _db;
        public SyncCallback(TextBox text_log, DataBaseHandler db) : base(text_log)
        {
            _db = db;
        }

        public override string DealCommand(SocketBackEventArgs e)
        {
            var line = (string) e.ReciveData;
            string args = line.Substring(line.IndexOf(' ')+1);
            
            long timestamp = Convert.ToInt64(args.Substring(0,args.IndexOf(' ')));
            CheckinData[] retCheckin = _db.GetCheckinDatas(timestamp);

            string json = args.Substring(args.IndexOf(' ') + 1).Trim();
            long newTimestamp = _db.SetCheckinDatas(JsonConvert.DeserializeObject<CheckinData[]>(json));

            return (newTimestamp+1) +" " + JsonConvert.SerializeObject(retCheckin);
        }
    }
}
