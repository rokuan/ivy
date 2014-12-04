using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Ivy.Network
{
    public class IvyServerSocket
    {
        private Socket server;
        private int localPort;

        private bool connected = false;

        private SocketAsyncEventArgs acceptArgs;
        private bool canAccept = true;

        private int acceptWaiting = 0;

        public IvyServerSocket(int port)
        {
            localPort = port;

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint endPoint = new IPEndPoint(ip, port);

            /* Creation de la socket */

            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            /* Ecoute de la socket */

            server.Bind(endPoint);
            server.Listen(100);

            /* Args pour l'envoi et la reception de donnees */

            acceptArgs = new SocketAsyncEventArgs();
            acceptArgs.Completed += new EventHandler<SocketAsyncEventArgs>(acceptCompleted);
        }

        private void socketConnected(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                connected = true;
            }
        }

        private void acceptCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                canAccept = true;

                if (acceptWaiting == 0)
                {
                    return;
                }

                tryAccept();
            }
        }

        public IvySocket accept()
        {
            //tryAccept();

            //TODO: renvoyer la socket acceptee
            return new IvySocket(server.Accept());
        }

        private void tryAccept()
        {
            if (!connected)
            {
                return;
            }

            if (canAccept)
            {

            }
            else
            {

            }
        }

        public void close()
        {
            server.Close();
        }
    }
}
