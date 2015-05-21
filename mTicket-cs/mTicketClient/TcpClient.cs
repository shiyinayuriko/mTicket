using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using mTickLibs.Tools;

namespace mTicketClient
{
    public class TcpClient
    {
        private readonly string _ipAddr;
        private readonly int _port;

        public TcpClient(string ipAddr, int port)
        {
            _port = port;
            _ipAddr = ipAddr;
        }

        private Socket _socket;
        private StreamReader _sr;
        private void Connect(){
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                SendTimeout = 5000,
                ReceiveTimeout = 5000
            };
            _socket.Connect(_ipAddr, _port);
            _sr = new StreamReader(new NetworkStream(_socket), Encoding.GetEncoding("UTF-8"));
	    }

        private void DisConnect()
        {
            try
            {
                _sr.Close();
                _socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

        }

        public string CallRaw(string s)
        {
            if(s==null||s.Trim().Equals("")) return null;
            string line = s.EndsWith("\n") ? s : s + "\n";
            _socket.Send(Encoding.UTF8.GetBytes(line));
            var ret = _sr.ReadLine();
            return ret;
        }

        public string Call(string command, string[] values)
        {
            string deviceName = IpTools.LocalName();
            String raw = command;
            raw += " " + Base64Tools.ToBase64(deviceName);
            raw = values.Aggregate(raw, (current, str) => current + (" " + Base64Tools.ToBase64(str)));
            string ret = CallRaw(raw);
            return ret;
        }

        public string Call(string command)
        {
            return Call(command, new string[0]);
        }


    }
}
