package framework;

import java.awt.Graphics;

public class IvyLabel extends IvyTextComponent {
		
	@Override
	public void draw(Graphics g) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void draw(Graphics g, int x, int y, int allowedWidth, int allowedHeight) {
		if(!isVisible()){
			return;
		}		
		
		g.setClip(x, y, allowedWidth, allowedHeight);
	}
}
