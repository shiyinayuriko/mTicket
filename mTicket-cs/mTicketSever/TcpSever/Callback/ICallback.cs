using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace mTicket
{
    interface ICallback
    {
        void UpdateLine(SocketBackEventArgs e);
        string DealCommand(SocketBackEventArgs e);
    }
}
