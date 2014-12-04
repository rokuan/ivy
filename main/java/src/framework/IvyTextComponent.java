package framework;

import java.awt.Color;

public abstract class IvyTextComponent extends IvyComponent {
	protected String text = "";
	protected int textAlignment = Alignment.LEFT;
	protected Color textColor = Color.BLACK;

	public final void setText(String t){
		if(t != null){
			text = t; 
		}
	}
	
	public final String getText(){
		return text;
	}
}
