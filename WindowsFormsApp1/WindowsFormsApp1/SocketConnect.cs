using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text;

namespace SocketCommunicationToPython
{
    class SocketConnect
    {
        Socket PcSocket;
        private bool connected; //Verbinungsstatus
        private TextBox Statusbox;
        private IPAddress Ip = null;
        private int port = -1;
        public SocketConnect(object _Statusbox = null)
        {
            if (_Statusbox != null)
            {
                Statusbox = (TextBox)_Statusbox;
            }
            connected = false;
        }
        #region Connect
        /// <summary>
        /// Verbindet sich zum Zielhost über eine Socket Verbindung.
        /// </summary>
        /// <param name="_ip">String-> Die aktuelle IP Adresse des Zielhost</param>
        /// <param name="_port">Int-> Die Protnummer des Sockets</param>
        public bool Connect(String _ip, int _port)
        {
            try
            {
                Ip = IPAddress.Parse(_ip);
                port = Convert.ToInt32(_port);
            }
            catch
            {
                SetTextToTextBox("IP oder Port enthalten einen Fehler -> " + Convert.ToString(Ip) + " Port " + port);
                connected = false;
                return connected;
            }
            try
            {
                PcSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                PcSocket.SendTimeout = 0;
                PcSocket.ReceiveTimeout = 0;
                PcSocket.Connect(Ip, port);
                if (PcSocket.Connected)
                {
                    SetTextToTextBox("Verbunden zu" + Ip + " Port " + port);
                    connected = true;
                }
                else
                {
                    SetTextToTextBox("Keine Verbinung" + Ip + " Port " + port);
                    connected = false;
                }
            }
            catch
            {
                SetTextToTextBox("Fehler beim Aufbau der Socket Verbindung " + Convert.ToString(Ip) + " Port " + port);
                connected = false;
            }
            return connected;
        }
        #endregion
        #region Disconnect
        /// <summary>
        /// Trennt die Verbinung zum Zielhost
        /// </summary>
        public void Disconnect()
        {
            try
            {
                PcSocket.Close();
                SetTextToTextBox("Trennen erfolgreich zu" + Ip + " Port " + port);
            }
            catch
            {
                SetTextToTextBox("Fehler beim Trennen mit " + Ip + " Port " + port);

            }
            connected = false;
        }
        #endregion
        #region Send
        /// <summary>
        /// Versenden einer Nachricht. String wird mit UFT8 an den Zielhost geschickt
        /// </summary>
        /// <param name="_message">String-> Sendet den String anden Zielhost</param>
        public bool Send(string _message)
        {
            bool send = false;
            try
            {
                byte[] msg= Encoding.UTF8.GetBytes(_message);
                PcSocket.Send(msg);
                send = true;
                SetTextToTextBox("Nachricht verschickt -> " + _message);
            }
            catch
            {
                SetTextToTextBox("Fehler beim Senden der Nachricht -> "+ _message);
            }
            return send;
        }
        #endregion
        #region Extrafunktionen 
        /// <summary>
        /// SetTextToTextBox Übergibt sicher den Text zum Hauptformular
        /// <param name="text">String -> Text der an die logBox übergeben werden soll </param>
        /// <param name="text">TextBox -> Link zum Objekt</param>
        /// </summary>
        private void SetTextToTextBox(String _text, object _textBox)
        {
            if (_textBox != null)
            {
                TextBox tBox = (TextBox)_textBox;

                if (tBox.InvokeRequired)
                {
                    tBox.Invoke(new Action(() =>
                    {
                        tBox.AppendText(_text);

                    }));
                }
                else
                {
                    tBox.Text += _text + Environment.NewLine;
                }
            }

        }
        /// <summary>
        /// SetTextToTextBox Übergibt sicher den Text an die LogBox zum Hauptformular
        /// <param name="text">String -> Text der an die logBox übergeben werden soll </param>
        /// </summary>
        private void SetTextToTextBox(String _text)
        {

            if (Statusbox != null)
            {

                if (Statusbox.InvokeRequired)
                {
                    Statusbox.Invoke(new Action(() =>
                    {
                        Statusbox.AppendText(_text);

                    }));
                }
                else
                {
                    Statusbox.Text += _text + Environment.NewLine;
                }
            }

        }
        #endregion
    }
}
