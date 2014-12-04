package event;

public class AccelerometerEvent extends IvyEvent {
	public static final int MAXX_VALUE = 100, MAXY_VALUE = 100, MAXZ_VALUE = 100, 
			MINX_VALUE = -100, MINY_VALUE = 100, MINZ_VALUE = 100;
	
	private int x = 0;
	private int y = 0;
	private int z = 0;	

	public AccelerometerEvent(int playerIdx, int eventX, int eventY, int eventZ){
		super(playerIdx);
		//playerIndex = playerIdx;
		x = eventX;
		y = eventY;
		z = eventZ;
	}

	public int getX(){
		return x;
	}

	public int getY(){
		return y;
	}

	public int getZ(){
		return z;
	}

	public String toString(){
		StringBuffer str = new StringBuffer();

		str.append("[ACCEL]");
		str.append(" PLAYER=");
		str.append(getPlayer());
		str.append(", X=");
		str.append(x);
		str.append(", Y=");
		str.append(y);
		str.append(", Z=");
		str.append(z);

		return str.toString();	
	}
}
