package activity;

import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.net.Socket;

public class Player implements Runnable {
	private Socket playerSock;
	private OutputStream os;
	private InputStream is;
	private BaseActivity activity;

	private Boolean connected;

	public Player(BaseActivity a, Socket s){
		playerSock = s;
		activity = a;
		connected = false;
	}

	@Override
	public void run() {
		try {
			os = playerSock.getOutputStream();

			os.write('Y');
			os.flush();	

			connected = true;
		} catch (IOException e) {
			try {
				playerSock.close();
			} catch (IOException e1) {}

			//activity.onClientDisconnected();
		}
	}

	public void send(byte oneByte){
		synchronized(os){
			
		}
	}
	
	public void send(byte[] data, int offset, int len){
		
	}
	
	public void hidePad(){
		
	}
	
	public synchronized boolean isConnected(){
		return connected;
	}
}
