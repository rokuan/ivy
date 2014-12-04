package event;

public abstract class IvyEvent {
	public static final int DOWN_STATE = 0;
	public static final int UP_STATE = 1;
	public static final int MOVE_STATE = 2;

	private int playerIndex = -1;

	protected IvyEvent(int playerIdx){
		playerIndex = playerIdx;
	}
	
	public final int getPlayer(){
		return playerIndex;
	}

	public static String stateToString(int stateMode){
		switch(stateMode){
		case DOWN_STATE:
			return "DOWN";

		case UP_STATE:
			return "UP";

		case MOVE_STATE:
			return "MOVE";

		default:
			return "UNKNOWN";
		}
	}
}
