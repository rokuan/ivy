package event;

public class PadEvent extends IvyEvent {	
	public static final int LEFT = 0;
	public static final int UP_LEFT = 1;
	public static final int UP = 2;
	public static final int UP_RIGHT = 3;
	public static final int RIGHT = 4;
	public static final int DOWN_RIGHT = 5;
	public static final int DOWN = 6;
	public static final int DOWN_LEFT = 7;
	public static final int T = 8;
	public static final int H = 9;
	public static final int I = 10;
	public static final int S = 11;
	public static final int SELECT = 12;
	public static final int PAUSE = 13;
	public static final int START = 14;
	
	private int state = -1;
	private int button = -1;
	private int x = -1;
	private int y = -1;
	
	
	public PadEvent(int playerIdx, int key, int eventState){
		super(playerIdx);
		//playerIndex = playerIdx;
		button = key;
		state = eventState;
	}
	
	public PadEvent(int playerIdx, int key, int eventState, int eventX, int eventY){
		this(playerIdx, key, eventState);
		
		x = eventX;
		y = eventY;
	}
	
	public int getSource(){
		return button;
	}
	
	public int getAction(){
		return state;
	}
	
	public int getX(){
		return x;
	}
	
	public int getY(){
		return y;
	}
	
	public static String padToString(int code){
		switch(code){
		case LEFT:
			return "LEFT";			
		case UP_LEFT:
			return "UP_LEFT";			
		case UP:
			return "UP";			
		case UP_RIGHT:
			return "UP_RIGHT";			
		case RIGHT:
			return "RIGHT";
		case DOWN_RIGHT:
			return "DOWN_RIGHT";
		case DOWN:
			return "DOWN";
		case DOWN_LEFT:
			return "DOWN_LEFT";
		case T:
			return "T";
		case H:
			return "H";
		case I:
			return "I";
		case S:
			return "S";
		case SELECT:
			return "SELECT";
		case PAUSE:
			return "PAUSE";
		case START:
			return "START";			
			
		default:
			return "UNKNOWN";
		}
	}
	
	public String toString(){
		StringBuffer str = new StringBuffer();
		
		str.append("[PAD]");
		str.append(" PLAYER=");
		str.append(getPlayer());
		str.append(", BUTTON=");
		str.append(padToString(button));
		str.append(", STATE=");
		str.append(stateToString(state));
		
		return str.toString();
	}
}
