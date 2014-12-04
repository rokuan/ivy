using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Ivy.Network
{
    public class IvySocket
    {
        private Socket sock;
        private SocketAsyncEventArgs sendArgs, recvArgs;

        private string ipAddress;
        private int port;

        private bool connected = false;
        private bool canSend = true;
        private bool canReceive = true;

        private Queue<IvyBuffer> sendMsgs = new Queue<IvyBuffer>();

        private Queue<IvyBuffer> recvMsgs = new Queue<IvyBuffer>();
        private int recvWaiting = 0;

        public IvySocket(string hostAddress, int hostPort)
        {
            ipAddress = hostAddress;
            port = hostPort;

            SocketAsyncEventArgs socketArgs = new SocketAsyncEventArgs();
            IPAddress ip = IPAddress.Parse(ipAddress);

            /* Creation de la socket */

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            /* Connexion de la socket */

            socketArgs.RemoteEndPoint = new IPEndPoint(ip, port);
            socketArgs.Completed += new EventHandler<SocketAsyncEventArgs>(socketConnected);

            sock.ConnectAsync(socketArgs);

            /* Args pour l'envoi et la reception de donnees */

            /*sendArgs = new SocketAsyncEventArgs();
            sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(sendCompleted);

            recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(receiveCompleted);*/
        }

        public IvySocket(Socket s)
        {
            sock = s;

            /* Args pour l'envoi et la reception de donnees */

            /*sendArgs = new SocketAsyncEventArgs();
            sendArgs.Completed += new EventHandler<SocketAsyncEventArgs>(sendCompleted);

            recvArgs = new SocketAsyncEventArgs();
            recvArgs.Completed += new EventHandler<SocketAsyncEventArgs>(receiveCompleted);*/
        }

        /*private void sendCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                canSend = true;

                if (sendMsgs.Count == 0)
                {
                    return;
                }

                trySend(sendMsgs.Dequeue());
            }
        }*/

        /*private void receiveCompleted(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                canReceive = true;

                if (recvWaiting == 0)
                {
                    return;
                }

                tryReceive(recvMsgs.Dequeue());
            }
        }*/

        private void socketConnected(object sender, SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                connected = true;
            }
        }

        public void send(int oneByte)
        {
            byte[] data = new byte[1];

            data[0] = (byte)oneByte;

            //trySend(new IvyBuffer(data, 0, 1));
            send(data, 0, 1);
        }

        public void send(byte[] data, int offset, int length){
            //trySend(new IvyBuffer(data, offset, length));
            sock.Send(data, offset, length, SocketFlags.None);
        }

        public int recv()
        {
            byte[] data = new byte[1];

            //tryReceive(new IvyBuffer(data, 0, 1));
            recv(data, 0, 1);

            return data[0];
        }

        public void recv(byte[] data, int offset, int length)
        {
            sock.Receive(data, offset, length, SocketFlags.None);
            //tryReceive(new IvyBuffer(data, offset, length));
        }

        /*protected void trySend(IvyBuffer buf)
        {
            if (canSend)
            {
                sendArgs.SetBuffer(buf.data, buf.offset, buf.length);
                canSend = false;
                sock.SendAsync(sendArgs);
            }
            else
            {
                sendMsgs.Enqueue(buf);
            }
        }*/

        /*protected void tryReceive(IvyBuffer buf)
        {            
            sock.Receive(buf.data, buf.offset, buf.length, SocketFlags.None);
        }*/

        public void close()
        {
            sock.Close();
        }
    }
}
