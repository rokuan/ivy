package activity;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Vector;

import event.AccelerometerEvent;
import event.IvyEvent;
import event.PadEvent;
import event.TouchEvent;
import exception.IvyException;

public abstract class BaseActivity {
	private static final int buttonNb = 15;
	//protected boolean[] isDown = new boolean[buttonNb];
	
	protected boolean[][] isDownForPlayer;

	protected static final int PAD_EVENT = 0;
	protected static final int ACCELEROMETER_EVENT = 1;
	protected static final int TOUCH_EVENT = 2;

	private int maxPlayersNb = 4;
	private int playersNb = 0;

	private static final int PORT = 7890;

	protected ServerSocket server;
	private boolean connected = false;

	protected Player[] players;

	/* TODO: mettre les bonnes valeurs */
	protected int screenWidth = 800;
	protected int screenHeight = 480;

	private static final byte accelX = 100;
	private static final byte accelY = 100;
	private static final byte accelZ = 100;

	public BaseActivity(){
		this(-1);
	}	

	public BaseActivity(int maxPlayers){
		/*for(int i=0; i<isDown.length; i++){
			isDown[i] = false;
		}*/

		if(maxPlayers > 0){
			maxPlayersNb = maxPlayers;
		}

		players = new Player[maxPlayersNb];
		
		for(int i=0; i<players.length; i++){
			players[i] = null;
		}
		
		isDownForPlayer = new boolean[maxPlayersNb][buttonNb];
		
		for(int i=0; i<isDownForPlayer.length; i++){
			for(int j=0; j<isDownForPlayer[0].length; j++){
				isDownForPlayer[i][j] = false;
			}
		}
	}

	protected synchronized boolean canAcceptNewClient(){
		return playersNb + 1 <= maxPlayersNb;
	}

	protected synchronized void onNewClient(){
		if(canAcceptNewClient()){
			playersNb++;
		}
	}

	private synchronized void removeClient(int index){
		//Retirer le client a l'indice 'index'
		if(index >= 0 && index < players.length){
			players[index] = null;
			
			onClientDisconnected();
		}
	}

	private synchronized void onClientDisconnected(){
		if(playersNb > 0){
			playersNb--;
		}
	}

	public void connect() throws Exception {
		server = new ServerSocket(PORT);
		connected = true;
	}

	protected void waitForClients() throws IvyException {
		if(server == null || !connected){
			throw new IvyException("Unable to wait for clients");
		}

		Socket clientSock;
		OutputStream os;

		System.out.println("Serveur en ecoute");
		
		while(connected){
			try {
				clientSock = server.accept();
				System.out.println("Client connected");
			} catch (IOException e) {
				connected = false;
				try {
					server.close();
				} catch (IOException e1) {}
				break;
			} 

			//Deja un maximum de joueurs connectes
			if(!canAcceptNewClient()){
				try {
					os = clientSock.getOutputStream();				
					os.write('N');
					os.flush();
					os.close();
					clientSock.close();
				} catch (IOException e) {}
				finally {
					try {
						clientSock.close();
					} catch (IOException e) {}
				}
			}
			//Encore de la place
			else{
				int index = getAvailablePlayerIndex();
				Player p = new Player(this, index, clientSock);
				players[index] = p;
				new Thread(p).start();
			}
		}
	}

	private synchronized int getAvailablePlayerIndex(){
		for(int i=0; i<players.length; i++){
			if(players[i] == null){
				return i;
			}
		}
		
		//Ne devrait pas arriver
		return -1;
	}
	
	public synchronized boolean isPadDownForPlayer(int playerIndex, int padCode){
		if(padCode >= 0 && padCode < buttonNb 
				&& playerIndex >= 0 && playerIndex < maxPlayersNb){
			return isDownForPlayer[playerIndex][padCode];
		}
		
		return false;
	}

	private void notifyPadEvent(int playerIndex, int padCode, int state){
		if(padCode >= 0 && padCode < buttonNb){
			boolean newState;

			if(state == IvyEvent.DOWN_STATE){
				newState = true;
			}
			else if(state == IvyEvent.UP_STATE){
				newState = false;
			}
			else{
				newState = isDownForPlayer[playerIndex][padCode];
			}

			isDownForPlayer[playerIndex][padCode] = newState;
		}

		final PadEvent pe = new PadEvent(playerIndex, padCode, state);

		new Thread(new Runnable(){
			public void run(){
				onPadEvent(pe);
			}
		}).start();
	}

	private void notifyTouchEvent(int playerIndex, int x, int y, int scrWidth, int scrHeight, int state){
		int newX, newY;

		newX = (int)((x * screenWidth)/(1.0 * scrHeight));
		newY = (int)((y * screenWidth)/(1.0 * scrHeight));

		final TouchEvent te = new TouchEvent(playerIndex, newX, newY, state);

		new Thread(new Runnable(){
			public void run(){
				onTouchEvent(te);
			}
		}).start();
	}

	private void notifyAccelerometerEvent(int playerIndex, int x, int y, int z, int maxAccelX, int maxAccelY, int maxAccelZ){
		int newX, newY, newZ;

		newX = (int)((x * accelX)/Math.abs(maxAccelX));
		newY = (int)((y * accelY)/Math.abs(maxAccelY));
		newZ = (int)((z * accelZ)/Math.abs(maxAccelZ));

		final AccelerometerEvent ae = new AccelerometerEvent(playerIndex, newX, newY, newZ);

		new Thread(new Runnable(){
			public void run(){
				onAcceleratorEvent(ae);
			}
		}).start();
	}

	public abstract boolean onPadEvent(PadEvent pe);
	public abstract boolean onTouchEvent(TouchEvent te);
	public abstract boolean onAcceleratorEvent(AccelerometerEvent ae);

	class Player implements Runnable {
		private BaseActivity act;
		private int playerIndex;
		private Socket sock;

		public Player(BaseActivity ba, int pIdx, Socket s){
			act = ba;
			playerIndex = pIdx;
			sock = s;
		}

		@Override
		public void run() {			
			if(sock == null){
				act.removeClient(playerIndex);
			}

			InputStream is;
			int instr, padCode, padState,
			x, y, z, width, height, touchState,
			maxX, maxY, maxZ;

			boolean connected = true;

			try {
				is = sock.getInputStream();

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

				while(connected){
					instr = is.read();

					switch(instr){
					case 'P':
						padCode = is.read();
						padState = is.read();

						act.notifyPadEvent(playerIndex, padCode, padState);
						break;

					case 'T':
						x = 0;
						x += (int)((is.read() << 8) & 0xFFFF);
						x += (int)(is.read() & 0xFF);

						y = 0;
						y += (int)((is.read() << 8) & 0xFFFF);
						y += (int)(is.read() & 0xFF);

						width = 0;
						width += (int)((is.read() << 8) & 0xFFFF);
						width += (int)(is.read() & 0xFF);

						height = 0;
						height += (int)((is.read() << 8) & 0xFFFF);
						height += (int)(is.read() & 0xFF);

						touchState = is.read();

						act.notifyTouchEvent(playerIndex, x, y, width, height, touchState);
						break;

					case 'A':
						/*x = is.read();
						y = is.read();
						z = is.read();

						maxX = is.read();
						maxY = is.read();
						maxZ = is.read();*/
						
						x = 0;
						x += (int)((is.read() << 8) & 0xFFFF);
						x += (int)(is.read() & 0xFF);
						
						y = 0;
						y += (int)((is.read() << 8) & 0xFFFF);
						y += (int)(is.read() & 0xFF);
						
						z = 0;
						z += (int)((is.read() << 8) & 0xFFFF);
						z += (int)(is.read() & 0xFF);
						
						maxX = 0;
						maxX += (int)((is.read() << 8) & 0xFFFF);
						maxX += (int)(is.read() & 0xFF);
						
						maxY = 0;
						maxY += (int)((is.read() << 8) & 0xFFFF);
						maxY += (int)(is.read() & 0xFF);
						
						maxZ = 0;
						maxZ += (int)((is.read() << 8) & 0xFFFF);
						maxZ += (int)(is.read() & 0xFF);

						act.notifyAccelerometerEvent(playerIndex, x, y, z, maxX, maxY, maxZ);
						break;
						
					case 'D':
						connected = false;
						break;						
					}
				}


			} catch (IOException e) {
				connected = false;
			}

			act.removeClient(playerIndex);
		}		
	}
}
