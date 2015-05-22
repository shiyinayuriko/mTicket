using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using mTicket.Beans;

namespace mTicketClient
{
    public partial class Scanner : Form
    {
        private ServiceContainer _service;

        public Scanner(string ipAddr, int port)
        {
            InitializeComponent();
            _service = new ServiceContainer(ipAddr, port, this)
            {
                Invoke = OnUpdateState,
                FinishInitialize = OnFinishInitialize
            };

            Thread t = new Thread(_service.ConnectHost);
            t.Start();
        }

        private void OnUpdateState(string message)
        {
            label_client_state.Text = message;
        }


        private void OnFinishInitialize()
        {
            textBox_manually_input.Enabled = true;
            listView_checkin.Enabled = true;
            listView_info.Enabled = true;

            _service.StartSync();
            _service.Checkin("1f7sLEqlW7D2fjZG", "test", CheckinSuccess);
        }

        private void CheckinSuccess(bool isSuccess,CodeDataDetail codeData)
        {
            Console.WriteLine(isSuccess+""+codeData);
        }

    }
}
