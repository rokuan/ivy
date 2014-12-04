package framework;

public class Ponger {
	private int x;
	private int y;
	private GamePanel gPanel;
	
	private int width = 20;
	private int height = 60;
	
	public Ponger(GamePanel gp, int xPong, int yPong, int pongWidth, int pongHeight){
		gPanel = gp;
		x = xPong;
		y = yPong;
		width = pongWidth;
		height = pongHeight;
	}
	
	public int getX(){
		return x;
	}
	
	public int getY(){
		return y;
	}
	
	public void moveUp(){
		y = Math.max(0, y-10);
	}
	
	public void moveDown(){
		y = Math.min(gPanel.getPreferredSize().height-height, y+10);
	}
}
