package components;

import java.awt.Color;

public abstract class IvyTextComponent extends IvyComponent {
	protected int textAlignment = Alignment.LEFT;
	protected Color textColor = Color.BLACK;
	
	public abstract void setText(String s);	
	public abstract String getText();
	
	public abstract void setTextAlignment(int alignment);
	public final int getTextAlignment(){
		return textAlignment;
	}
}
