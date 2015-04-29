using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mTickLibs.codeData;
using Newtonsoft.Json;

namespace mTicket
{
    class SyncCallback : ICallback
    {
        private Form1 form1;
        private DataBaseHandler _db;
        public SyncCallback(Form1 form1, DataBaseHandler db)
        {
            this.form1 = form1;
            _db = db;
        }

        public void UpdateLine(SocketBackEventArgs e)
        {
            form1.textBox1.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ms:::") + e.EndPoint + ":" +  e.ReciveData + Environment.NewLine);
        }

        public string DealCommand(SocketBackEventArgs e)
        {
            UpdateLine(e);
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
