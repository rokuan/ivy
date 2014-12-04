package event;

public class TouchEvent extends IvyEvent {
	private int x = -1;
	private int y = -1;
	private int state = -1;
	
	public TouchEvent(int playerIdx, int xTouch, int yTouch, int state){
		super(playerIdx);
		//playerIndex = playerIdx;
		x = xTouch;
		y = yTouch;
	}
	
	public int getX(){
		return x;
	}
	
	public int getY(){
		return y;
	}
	
	public int getAction(){
		return state;
	}
	
	public String toString(){
		StringBuffer str = new StringBuffer();
		
		str.append("[TOUCH]");
		str.append(" PLAYER=");
		str.append(getPlayer());
		str.append(", X=");
		str.append(x);
		str.append(", Y=");
		str.append(y);
		str.append(", STATE=");
		str.append(stateToString(state));
		
		return str.toString();
	}
}
