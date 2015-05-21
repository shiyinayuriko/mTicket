using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace mTickLibs.Beans
{
    public class Settings
    {
        public int timer;
        public int proress_step_update_database;
        public int tcp_timeout;
        public string checkin_logic;
        public long restart_scanner_delay;

        public string GetJson()
        {
            var serialStr = JsonConvert.SerializeObject(this);
            return serialStr;
        }
    }
}
