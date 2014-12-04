package components;

import java.awt.Graphics;

import javax.swing.JComponent;
import javax.swing.JLabel;
import javax.swing.SwingConstants;

import components.IvyComponent.Alignment;

public class IvyLabel extends IvyTextComponent {
	private JLabel label;	
	
	public IvyLabel(){
		label = new JLabel();
	}
	
	@Override
	public void draw(Graphics g) {}

	@Override
	public void draw(Graphics g, int x, int y, int allowedWidth, int allowedHeight) {
		if(!isVisible()){
			return;
		}		
		
	}

	@Override
	public void setText(String s) {
		if(s != null){
			label.setText(s);
		}
	}

	@Override
	public String getText() {
		return label.getText();
	}

	@Override
	public JComponent getComponent() {
		return label;
	}

	@Override
	public void setTextAlignment(int alignment) {		
		switch(alignment){
		case Alignment.LEFT:
			label.setHorizontalAlignment(SwingConstants.LEFT);
			break;
			
		case Alignment.CENTER:
			label.setHorizontalAlignment(SwingConstants.CENTER);
			break;
			
		case Alignment.RIGHT:
			label.setHorizontalAlignment(SwingConstants.RIGHT);
			break;
		}
	}
}
