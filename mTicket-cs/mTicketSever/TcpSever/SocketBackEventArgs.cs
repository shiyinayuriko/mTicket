using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace mTicketSever
{
    public class SocketBackEventArgs : EventArgs
    {
        public object ReciveData { get; set; }
        public IPEndPoint EndPoint { get; set; }
    }
}
