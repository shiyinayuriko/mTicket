using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace mTicketSever
{
    public class SocketBackEventArgs : EventArgs
    {
        private object reciveData;
        public object ReciveData
        {
            get { return reciveData; }
            set { reciveData = value; }
        }

        private IPEndPoint endPoint;
        public IPEndPoint EndPoint
        {
            get { return endPoint; }
            set { endPoint = value; }
        }
    }
}
