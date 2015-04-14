using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace mTicketSever
{
    interface ICallback
    {
        void updateLine(SocketBackEventArgs e);
        string dealCommand(SocketBackEventArgs e);
    }
}
