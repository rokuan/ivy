package components;

import java.awt.Color;
import java.awt.Graphics;

import javax.swing.JButton;
import javax.swing.JComponent;
import javax.swing.SwingConstants;

public class IvyButton extends IvyTextComponent {
	private JButton button;

	public IvyButton(){		
		button = new JButton();
		//setTextAlignment(textAlignment);
		borderType = Border.ROUNDED;
		borderColor = Color.DARK_GRAY;
		backgroundColor = Color.LIGHT_GRAY;
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
	public JComponent getComponent() {
		return button;
	}

	@Override
	public void setText(String s) {
		if(s != null){
			button.setText(s);
		}
	}

	@Override
	public String getText() {
		return button.getText();
	}

	@Override
	public void setTextAlignment(int alignment) {
		if(alignment == textAlignment){
			return;
		}
		
		switch(alignment){
		case Alignment.LEFT:
			button.setHorizontalAlignment(SwingConstants.LEFT);
			break;
			
		case Alignment.CENTER:
			button.setHorizontalAlignment(SwingConstants.CENTER);
			break;
			
		case Alignment.RIGHT:
			button.setHorizontalAlignment(SwingConstants.RIGHT);
			break;
		}
	}
}
