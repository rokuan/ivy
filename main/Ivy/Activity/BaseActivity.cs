using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivy.Network;
using Ivy.Exceptions;
using System.IO;
using System.Threading;
using Ivy.Event;

namespace Ivy.Activity
{
    public abstract class BaseActivity
    {
        private object access = new object();

        private static readonly int buttonNb = 15;
        //protected bool[] isDown = new bool[buttonNb];

        protected bool[][] isDownForPlayer;

        protected static readonly int PAD_EVENT = 0;
        protected static readonly int ACCELEROMETER_EVENT = 1;
        protected static readonly int TOUCH_EVENT = 2;

        private int maxPlayersNb = 4;
        private int playersNb = 0;

        private static readonly int PORT = 7890;

        protected IvyServerSocket server;
        private bool connected = false;

        protected Player[] players;

        /* TODO: mettre les bonnes valeurs */
        protected int screenWidth = 800;
        protected int screenHeight = 480;

        private static readonly byte accelX = 100;
        private static readonly byte accelY = 100;
        private static readonly byte accelZ = 100;

        public BaseActivity()
            : this(-1)
        {

        }

        public BaseActivity(int maxPlayers)
        {
            if (maxPlayers > 0)
            {
                maxPlayersNb = maxPlayers;
            }

            players = new Player[maxPlayersNb];

            for (int i = 0; i < players.Length; i++)
            {
                players[i] = null;
            }

            isDownForPlayer = new bool[maxPlayersNb][];

            for (int i = 0; i < isDownForPlayer.Length; i++)
            {
                isDownForPlayer[i] = new bool[buttonNb];

                for (int j = 0; j < isDownForPlayer[i].Length; j++)
                {
                    isDownForPlayer[i][j] = false;
                }
            }
        }

        protected bool canAcceptNewClient()
        {
            lock (access)
            {
                return playersNb + 1 <= maxPlayersNb;
            }
        }

        protected void onNewClient()
        {
            lock (access)
            {
                if (canAcceptNewClient())
                {
                    playersNb++;
                }
            }
        }

        private void removeClient(int index)
        {
            lock (access)
            {
                //Retirer le client a l'indice 'index'
                if (index >= 0 && index < players.Length)
                {
                    players[index] = null;

                    onClientDisconnected();
                }
            }
        }

        private void onClientDisconnected()
        {
            if (playersNb > 0)
            {
                playersNb--;
            }
        }

        public void connect()
        {
            server = new IvyServerSocket(PORT);
            connected = true;
        }

        public void waitForClients()
        {
            if (server == null || !connected)
            {
                throw new IvyException("Unable to wait for clients");
            }

            IvySocket clientSock;

            while (connected)
            {
                try
                {
                    clientSock = server.accept();
                }
                catch (Exception e)
                {
                    connected = false;
                    try
                    {
                        server.close();
                    }
                    catch (IOException e1) { }
                    break;
                }

                //Deja un maximum de joueurs connectes
                if (!canAcceptNewClient())
                {
                    try
                    {
                        clientSock.send('N');
                        clientSock.close();
                    }
                    catch (IOException e) { }
                    finally
                    {
                        try
                        {
                            clientSock.close();
                        }
                        catch (IOException e) { }
                    }
                }
                //Encore de la place
                else
                {
                    try
                    {
                        int index = getAvailablePlayerIndex();
                        Player p = new Player(this, index, clientSock);
                        players[index] = p;

                        //TODO: creer un thread qui communique avec le joueur
                        Thread t = new Thread(new ThreadStart(p.run));
                        t.Start();
                    }
                    catch (Exception e)
                    {
                        System.Windows.MessageBox.Show(e.StackTrace);
                    }
                }
            }
        }

        private int getAvailablePlayerIndex()
        {
            lock (access)
            {
                for (int i = 0; i < players.Length; i++)
                {
                    if (players[i] == null)
                    {
                        return i;
                    }
                }
            }

            //Ne devrait pas arriver
            return -1;
        }

        public bool isPadDownForPlayer(int playerIndex, int padCode)
        {
            lock (access)
            {
                if (padCode >= 0 && padCode < buttonNb
                        && playerIndex >= 0 && playerIndex < maxPlayersNb)
                {
                    return isDownForPlayer[playerIndex][padCode];
                }
            }

            return false;
        }

        private void notifyPadEvent(int playerIndex, PadEvent.PadButtonCommand padCode, IvyEvent.PadButtonAction state)
        {
            bool newState;

            if (state == IvyEvent.PadButtonAction.DOWN_STATE)
            {
                newState = true;
            }
            else if (state == IvyEvent.PadButtonAction.UP_STATE)
            {
                newState = false;
            }
            else
            {
                newState = isDownForPlayer[playerIndex][(int)padCode];
            }

            isDownForPlayer[playerIndex][(int)padCode] = newState;

            PadEvent pe = new PadEvent(playerIndex, padCode, state);

            Thread t = new Thread(new ThreadStart(delegate()
            {
                onPadEvent(pe);
            }));

            t.Start();

            //TODO : lancer le thread qui va appeler la fonction event
        }

        private void notifyTouchEvent(int playerIndex, int x, int y, int scrWidth, int scrHeight, int state)
        {
            int newX, newY;

            newX = (int)((x * screenWidth) / (1.0 * scrHeight));
            newY = (int)((y * screenWidth) / (1.0 * scrHeight));

            TouchEvent te = new TouchEvent(playerIndex, newX, newY, state);

            Thread t = new Thread(new ThreadStart(delegate()
            {
                onTouchEvent(te);
            }));

            t.Start();

            //TODO : lancer le thread qui va appeler la fonction event
        }

        private void notifyAccelerometerEvent(int playerIndex, int x, int y, int z, int maxAccelX, int maxAccelY, int maxAccelZ)
        {
            int newX, newY, newZ;

            newX = (int)((x * accelX) / Math.Abs(maxAccelX));
            newY = (int)((y * accelY) / Math.Abs(maxAccelY));
            newZ = (int)((z * accelZ) / Math.Abs(maxAccelZ));

            AccelerometerEvent ae = new AccelerometerEvent(playerIndex, newX, newY, newZ);

            Thread t = new Thread(new ThreadStart(delegate()
            {
                onAcceleratorEvent(ae);
            }));

            t.Start();

            //TODO : lancer le thread qui va appeler la fonction event
        }

        public abstract bool onPadEvent(PadEvent pe);
        public abstract bool onTouchEvent(TouchEvent te);
        public abstract bool onAcceleratorEvent(AccelerometerEvent ae);

        public void disconnect()
        {
            if (!connected)
            {
                return;
            }

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    try
                    {
                        players[i].exit();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }

            try
            {
                server.close();
            }
            catch (Exception e)
            {

            }

            connected = false;
        }

        /* TODO: creer la classe Player qui est un runnable */

        public class Player
        {
            private BaseActivity act;
            private int playerIndex;
            private IvySocket sock;

            public Player(BaseActivity ba, int pIdx, IvySocket s)
            {
                act = ba;
                playerIndex = pIdx;
                sock = s;
            }

            public void run()
            {
                if (sock == null)
                {
                    act.removeClient(playerIndex);
                }

                int instr,
                x, y, z, width, height, touchState,
                maxX, maxY, maxZ;

                IvyEvent.PadButtonAction padState;
                PadEvent.PadButtonCommand padCode;

                bool connected = true;

                try
                {
                    /*
                     * 
                     * PAD:
                     * 
                     * 'P' (1 octet)
                     * padCode (1 octet)
                     * padState (1 octet)
                     * 
                     * 
                     * 
                     * TOUCH:
                     * 
                     * 'T' (1 octet)
                     * x (2 octets)
                     * y (2 octets)
                     * screenWidth (2 octets)
                     * screenHeight (2 octets)
                     * touchState (1 octet)
                     * 
                     * 
                     * 
                     * ACCEL:
                     * 
                     * 'A' (1 octet)
                     * x (2 octets)
                     * y (2 octets)
                     * z (2 octets)
                     * maxX (2 octets)
                     * maxY (2 octets)
                     * maxZ (2 octets)
                     * 
                     * 
                     * DISCONNECTION:
                     * 
                     * 'D' (1 octet)
                     * 
                     */

                    while (connected)
                    {
                        instr = sock.recv();

                        switch (instr)
                        {
                            case 'P':
                                padCode = (PadEvent.PadButtonCommand)sock.recv();
                                padState = (IvyEvent.PadButtonAction)sock.recv();

                                act.notifyPadEvent(playerIndex, padCode, padState);
                                break;

                            case 'T':
                                x = 0;
                                x += (int)((sock.recv() << 8) & 0xFFFF);
                                x += (int)(sock.recv() & 0xFF);

                                y = 0;
                                y += (int)((sock.recv() << 8) & 0xFFFF);
                                y += (int)(sock.recv() & 0xFF);

                                width = 0;
                                width += (int)((sock.recv() << 8) & 0xFFFF);
                                width += (int)(sock.recv() & 0xFF);

                                height = 0;
                                height += (int)((sock.recv() << 8) & 0xFFFF);
                                height += (int)(sock.recv() & 0xFF);

                                touchState = sock.recv();

                                act.notifyTouchEvent(playerIndex, x, y, width, height, touchState);
                                break;

                            case 'A':
                                x = 0;
                                x += (int)((sock.recv() << 8) & 0xFFFF);
                                x += (int)(sock.recv() & 0xFF);

                                y = 0;
                                y += (int)((sock.recv() << 8) & 0xFFFF);
                                y += (int)(sock.recv() & 0xFF);

                                z = 0;
                                z += (int)((sock.recv() << 8) & 0xFFFF);
                                z += (int)(sock.recv() & 0xFF);

                                maxX = 0;
                                maxX += (int)((sock.recv() << 8) & 0xFFFF);
                                maxX += (int)(sock.recv() & 0xFF);

                                maxY = 0;
                                maxY += (int)((sock.recv() << 8) & 0xFFFF);
                                maxY += (int)(sock.recv() & 0xFF);

                                maxZ = 0;
                                maxZ += (int)((sock.recv() << 8) & 0xFFFF);
                                maxZ += (int)(sock.recv() & 0xFF);

                                System.Windows.MessageBox.Show("x=" + x + ", y=" + y + ", z=" + z);

                                act.notifyAccelerometerEvent(playerIndex, x, y, z, maxX, maxY, maxZ);
                                break;

                            case 'D':
                                connected = false;
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    connected = false;
                }

                act.removeClient(playerIndex);
            }

            public void exit()
            {
                if (sock != null)
                {
                    //Envoyer un message comme quoi le serveur a coupe
                    try
                    {
                        sock.close();
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
        }
    }
}
