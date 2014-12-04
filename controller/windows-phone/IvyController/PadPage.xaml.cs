using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.Net.Sockets;

namespace IvyController
{
    public partial class PadPage : PhoneApplicationPage
    {
        /*protected static readonly int LEFT = 0;
        protected static readonly int UP_LEFT = 1;
        protected static readonly int UP = 2;
        protected static readonly int UP_RIGHT = 3;
        protected static readonly int RIGHT = 4;
        protected static readonly int DOWN_RIGHT = 5;
        protected static readonly int DOWN = 6;
        protected static readonly int DOWN_LEFT = 7;
        protected static readonly int T = 8;
        protected static readonly int H = 9;
        protected static readonly int I = 10;
        protected static readonly int S = 11;
        protected static readonly int SELECT = 12;
        protected static readonly int PAUSE = 13;
        protected static readonly int START = 14;
        protected static readonly int TH = 15;
        protected static readonly int HI = 16;
        protected static readonly int IS = 17;
        protected static readonly int ST = 18;*/

        public enum PadCode
        {
            LEFT = 0,
            UP_LEFT = 1,
            UP = 2,
            UP_RIGHT = 3,
            RIGHT = 4,
            DOWN_RIGHT = 5,
            DOWN = 6,
            DOWN_LEFT = 7,
            T = 8,
            H = 9,
            I = 10,
            S = 11,
            SELECT = 12,
            PAUSE = 13,
            START = 14,
            TH = 15,
            HI = 16,
            IS = 17,
            ST = 18
        };

        private static readonly byte DOWN_STATE = 0;
        private static readonly byte UP_STATE = 1;
        private static readonly byte MOVE_STATE = 2;

        //protected PadButton[] padButtons = new PadButton[maxButtonNb];

        /* TODO: Etablir la connexion */
        private Socket sock;
        private SocketAsyncEventArgs sa;

        private string ipAddress;
        private int port;

        private bool connected = false;
        private bool canSend = true;

        private Queue<byte[]> msgs = new Queue<byte[]>();

        public PadPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string portStr = "";

            ipAddress = "";
            port = 0;

            if (!NavigationContext.QueryString.TryGetValue("ipValue", out ipAddress))
            {
                System.Windows.MessageBox.Show("Pas d'adresse IP fournie");
                NavigationService.GoBack();
            }

            if (!NavigationContext.QueryString.TryGetValue("portValue", out portStr))
            {
                System.Windows.MessageBox.Show("Pas de port fourni");
                NavigationService.GoBack();
            }

            port = int.Parse(portStr);

            try
            {
                SocketAsyncEventArgs socketArgs = new SocketAsyncEventArgs();
                IPAddress ip = IPAddress.Parse(ipAddress);

                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                socketArgs.RemoteEndPoint = new IPEndPoint(ip, port);
                socketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(socketConnected);

                sock.ConnectAsync(socketArgs);
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("La connexion a echoue");
                NavigationService.GoBack();
            }
            
            sa = new SocketAsyncEventArgs();
            sa.Completed += new EventHandler<SocketAsyncEventArgs>(sendCompleted);
        }

        private void onTouchDown(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                /* (int)((PadButton)sender).getCode() */
                //PadCode code = 0;
                try
                {
                    //PadCode code = (PadCode)((int)(((Button)sender).Tag));
                    PadCode code = (PadCode)(int.Parse(((Button)sender).Tag.ToString()));

                    onPadTouch(code, DOWN_STATE);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
        }

        private void onTouchUp(object sender, MouseEventArgs e)
        {
            if (sender is Button)
            {
                /* (int)((PadButton)sender).getCode() */
                //PadCode code = 0;
                try
                {
                    //PadCode code = (PadCode)((int)(((Button)sender).Tag));
                    PadCode code = (PadCode)(int.Parse(((Button)sender).Tag.ToString()));

                    onPadTouch(code, UP_STATE);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.ToString());
                }
            }
        }

        public void onPadTouch(PadCode code, byte state)
        {
            if (!connected)
            {
                return;
            }

            try
            {
                switch (code)
                {
                    case PadCode.TH:
                        trySend(new byte[] { (byte)'P', (byte)PadCode.T, state, (byte)'P', (byte)PadCode.H, state });
                        break;

                    case PadCode.HI:
                        trySend(new byte[] { (byte)'P', (byte)PadCode.H, state, (byte)'P', (byte)PadCode.I, state });
                        break;

                    case PadCode.IS:
                        trySend(new byte[] { (byte)'P', (byte)PadCode.I, state, (byte)'P', (byte)PadCode.S, state });
                        break;

                    case PadCode.ST:
                        trySend(new byte[] { (byte)'P', (byte)PadCode.S, state, (byte)'P', (byte)PadCode.T, state });
                        break;

                    default:
                        trySend(new byte[] { (byte)'P', (byte)code, state });
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public void sendCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                canSend = true;

                if (msgs.Count == 0)
                {
                    return;
                }

                trySend(msgs.Dequeue());
            }
        }

        public void socketConnected(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                connected = true;
            }
        }

        public void trySend(byte[] data)
        {
            if (canSend)
            {
                sa.SetBuffer(data, 0, data.Length);
                canSend = false;
                sock.SendAsync(sa);
            }
            else
            {
                msgs.Enqueue(data);
            }
        }
    }
}