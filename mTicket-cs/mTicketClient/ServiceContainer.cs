using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mTicketClient
{
    class ServiceContainer
    {
        private TcpClient _tcp;
        public ServiceContainer(string ipAddr, int port)
        {
            _tcp = new TcpClient(ipAddr,port);
        }
    }
}
