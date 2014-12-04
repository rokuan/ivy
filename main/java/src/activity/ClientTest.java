package activity;

import java.io.IOException;
import java.io.OutputStream;
import java.net.Socket;
import java.net.UnknownHostException;

public class ClientTest {
	public static void main(String[] args){
		try {
			Socket s = new Socket("localhost", 7890);
			OutputStream os = s.getOutputStream();
			
			int x = 5400, y = 4572, z = 3300, maxX = 5899, maxY = 7754, maxZ = 4759;
			
			os.write('A');
			
			os.write(x >> 8);
			os.write(x);
			
			os.write(y >> 8);
			os.write(y);
			
			os.write(z >> 8);
			os.write(z);
			
			os.write(maxX >> 8);
			os.write(maxX);
			
			os.write(maxY >> 8);
			os.write(maxY);
			
			os.write(maxZ >> 8);
			os.write(maxZ);
			
			os.flush();
			
			os.close();
			s.close();
		} catch (UnknownHostException e) {

		} catch (IOException e) {
			
		}		
	}
}
