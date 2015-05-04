using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace mTicket
{
    class HostNameHolder
    {
        private readonly Dictionary<IPAddress,string> map = new Dictionary<IPAddress, string>();
        public static HostNameHolder Instance = new HostNameHolder();

        private HostNameHolder()
        {
            map.Clear();
        }

        public void SetName(IPAddress ipaddress,string name)
        {
            map.Remove(ipaddress);
            map.Add(ipaddress,name);
        }

        public string GetName(IPAddress ipaddress)
        {
            return map.ContainsKey(ipaddress) ? map[ipaddress] : "error?";
        }
    }
}
