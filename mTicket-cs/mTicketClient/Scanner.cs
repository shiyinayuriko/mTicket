using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace mTicketClient
{
    public partial class Scanner : Form
    {
        private ServiceContainer _service;

        public Scanner()
        {
            InitializeComponent();
        }

        public Scanner(string ipAddr, int port)
        {
            _service = new ServiceContainer(ipAddr, port);
        }
    }
}
