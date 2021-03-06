﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using mTickLibs.Beans;
using Newtonsoft.Json;

namespace mTicket
{
    class SettingCountainer:Settings
    {
        [JsonIgnore]
        public static readonly Settings Instance = GetSettings();

        public static Settings GetSettings()
        {
            string tmp;
            try
            {
                string path = ConfigurationManager.AppSettings["checkin_logic"];
                StreamReader sr = new StreamReader(path, Encoding.Default);
                tmp = sr.ReadToEnd();
            }
            catch (Exception )
            {
                tmp = Properties.Resources.checkinLogic;
            }
            tmp = Regex.Replace(tmp, "[\r\n]", "");
            var ret = new Settings
            {
                timer = Convert.ToInt32(ConfigurationManager.AppSettings["timer"]),
                proress_step_update_database = Convert.ToInt32(ConfigurationManager.AppSettings["proress_step_update_database"]),
                tcp_timeout = Convert.ToInt32(ConfigurationManager.AppSettings["tcp_timeout"]),
                restart_scanner_delay = Convert.ToInt64(ConfigurationManager.AppSettings["restart_scanner_delay"]),
                checkin_logic = tmp
            };
            return ret;
        }
    }


}
