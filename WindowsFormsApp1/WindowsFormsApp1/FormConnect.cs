using System;
using System.Windows.Forms;

namespace SocketCommunicationToPython
{
    public partial class SocketPythonKommunikation : Form
    {
        private SocketConnect SocketPython;
        public SocketPythonKommunikation()
        {
            InitializeComponent();
            SocketPython = new SocketConnect(Status);
            Disconnect.Visible = false;
            Connect.Visible = true;
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            if(SocketPython.Connect(Ip.Text, Convert.ToInt32(Port.Value))){
                Disconnect.Visible = true;
                Connect.Visible = false;
            }
            else
            {
                Disconnect.Visible = false;
                Connect.Visible = true;
            }
        }

        private void Disconnect_Click(object sender, EventArgs e)
        {
            SocketPython.Disconnect();
            Disconnect.Visible = false;
            Connect.Visible = true;
        }

        private void Send_Click(object sender, EventArgs e)
        {
            SocketPython.Send(SendText.Text);
        }
    }
}
