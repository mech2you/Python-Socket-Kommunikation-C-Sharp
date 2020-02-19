using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using System.Windows.Forms;

namespace SocketCommunicationToPython
{
    class SocketConnect
    {
        Socket PcSocket;
        private bool connected; //Verbinungsstatus
        private TextBox Statusbox;
        public SocketConnect(object _Statusbox= null)
        {
            if (_Statusbox != null)
            {
                Statusbox = (TextBox)_Statusbox;
            }
            connected = false;
        }
        #region Hauptfunktionen Connect,Disconnect,Send
        /// <summary>
        ///  Verbindet sich zu den Roboter über eine Socket Verbindung.
        /// </summary>
        /// <param name="ip">Die aktuelle IP Adresse des Zielhost</param>
        /// <param name="port">Die Protnummer des Sockets</param>
        public bool Connect(IPAddress ip, int port)
        {
                try
                {
                    PcSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    PcSocket.SendTimeout = 0;
                    PcSocket.ReceiveTimeout = 0;
                    PcSocket.Connect(ip, port);
                    if (PcSocket.Connected)
                    {
                        SetTextToTextBox("Verbunden zu"+ ip +" : "+port);
                        connected = true;
                    }
                    else
                    {
                        SetTextToTextBox("Keine Verbinung" + ip + " : " + port);
                        connected = false;
                    }
                }
                catch
                {
                    SetTextToTextBox("Fehler beim Aufbau der Socket Verbindung " + ip + " : " + port);
                    connected = false;
                }
                return connected; ;
        }
    #endregion
    #region Extrafunktionen 
    /// <summary>
    /// SetTextToTextBox Übergibt sicher den Text zum Hauptformular
    /// <param name="text">String -> Text der an die logBox übergeben werden soll </param>
    /// <param name="text">TextBox -> Link zum Objekt</param>
    /// </summary>
    private void SetTextToTextBox(String text, object _textBox)
        {
            if (_textBox != null)
            {
                TextBox tBox = (TextBox)_textBox;

                if (tBox.InvokeRequired)
                {
                    tBox.Invoke(new Action(() =>
                    {
                        tBox.AppendText(text);

                    }));
                }
                else
                {
                    tBox.Text += text + Environment.NewLine;
                }
            }
                
        }
        /// <summary>
        /// SetTextToTextBox Übergibt sicher den Text an die LogBox zum Hauptformular
        /// <param name="text">String -> Text der an die logBox übergeben werden soll </param>
        /// </summary>
        private void SetTextToTextBox(String text)
        {

            if (Statusbox != null)
            {

                if (Statusbox.InvokeRequired)
                {
                    Statusbox.Invoke(new Action(() =>
                    {
                        Statusbox.AppendText(text);

                    }));
                }
                else
                {
                    Statusbox.Text += text + Environment.NewLine;
                }
            }

        }
        #endregion
    }
}
