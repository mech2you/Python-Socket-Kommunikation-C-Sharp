using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Text;
using System.ComponentModel;
namespace SocketCommunicationToPython
{
    class SocketConnect
    {
        /// <summary>
        /// Socketverbinung zum Zielhost
        /// </summary>
        Socket PcSocket;
        /// <summary>
        /// Verbinungsstatus der Socket Verbinung
        /// </summary>
        private bool connected;
        /// <summary>
        /// Objekt einer Textbox um Nachrichten aus der Klasse an das Formular zu übertragen
        /// </summary>
        private TextBox Statusbox;
        /// <summary>
        /// IP vom Zielhost
        /// </summary>
        private IPAddress Ip = null;
        /// <summary>
        /// Port vom Zielhost
        /// </summary>
        private int port = -1; // Port vom Zielhost
        /// <summary>
        /// Wird verwenden um Daten zu senden und diese zu empfangen
        /// </summary>
        private BackgroundWorker DatenWorker; 

        #region SocketConnect Konstrukter
        /// <summary>
        /// Im Konstruktor wird der BackgroundWorker initialisiert. Dabei kann optional eine Textbox übergeben werden die im Fehlerfall die Nachrichten oder Statusmeldungen erhält.  
        /// </summary>
        /// <param name="_Statusbox">TextBox-> Objekt zur Textbox das Fehlernachrichten oder Statusmeldung erhält</param>
        public SocketConnect(object _Statusbox = null)
        {
            if (_Statusbox != null)
            {
                Statusbox = (TextBox)_Statusbox;
            }
            connected = false;
            DatenWorker = new BackgroundWorker();
            DatenWorker.DoWork += new DoWorkEventHandler(EventHandlerDatenWorker);
            DatenWorker.WorkerReportsProgress = true;
            DatenWorker.WorkerSupportsCancellation = true;
        }
        #endregion
        #region EventHandlerDatenWorker
        /// <summary>
        /// Thread der mit den BackgroundWorker gelöst ist. Es werden Daten an den Zielhost übermittelt und auf eine Rückantwort gewartet. Im Fehlerfall gibt die Funktion e.Result false zurück.
        /// </summary>
        private void EventHandlerDatenWorker(object sender, DoWorkEventArgs e)
        {
            string kommando = (string)e.Argument;
            if (connected)
            {
                bool send = false;
                try
                {
                    byte[] msg = Encoding.UTF8.GetBytes(kommando);
                    PcSocket.Send(msg);
                    send = true;
                    SetTextToTextBox("Nachricht verschickt -> " + kommando);
                }
                catch
                {
                    SetTextToTextBox("Fehler beim Senden der Nachricht -> " + kommando);
                }
                if (send)
                {
                    SetTextToTextBox(" Nachricht -> " + Receive());
                    e.Result = true;
                }
                else
                {
                    e.Result = false;
                }
            }
        }
        #endregion
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
                PcSocket.Shutdown(SocketShutdown.Both);
            }
            catch
            {
                SetTextToTextBox("Fehler beim Trennen mit " + Ip + " Port " + port);

            }
            finally
            {
                PcSocket.Close();
                SetTextToTextBox("Trennen erfolgreich zu" + Ip + " Port " + port);
            }
            connected = false;
        }
        #endregion
        #region Send
        /// <summary>
        /// Versenden einer Nachricht. String wird mit UFT8 an den Zielhost geschickt
        /// </summary>
        /// <param name="_message">String-> Sendet den String anden Zielhost</param>
        public void Send(string _message)
        {
            DatenWorker.RunWorkerAsync(_message);
            
        }
        #endregion
        #region Receive
        /// <summary>
        /// Empfängt eine Nachricht vom Zielhost. Im Fehlerfall wird der Fehler zurückgegeben
        /// </summary>
        public string Receive()
        {
            byte[] bytes = new byte[1024];
            try
            {
                PcSocket.Receive(bytes);
            }
            catch (SocketException e)
            {
                return (Convert.ToString(e.ErrorCode)+" "+ Convert.ToString(e.Message));
            }
            return Encoding.UTF8.GetString(bytes);
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
