package framework;

import java.awt.Graphics;
import java.util.Vector;

public class IvyList extends IvyContainer {
	private Vector<IvyComponent> components;
	private int orientation = Orientation.HORIZONTAL;
	private boolean scrollable = true;
	private int visibleComponents = 0;
	private int selectedIndex = 0;
	private boolean centerSelected = false;
	
	public IvyList(){
		components = new Vector<IvyComponent>();
	}	
	
	@Override
	public void draw(Graphics g) {
		// TODO Auto-generated method stub		
	}
	
	public void addComponent(IvyComponent comp){
		components.add(comp);
	}

	@Override
	public void draw(Graphics g, int x, int y, int allowedWidth, int allowedHeight) {
		if(!isVisible()){
			return;
		}
		
		g.setClip(x, y, allowedWidth, allowedHeight);	
		
		int componentWidth = 0, componentHeight = 0;
		
		if(widthType == DimensionParameters.FIXED && orientation == Orientation.HORIZONTAL){
			componentWidth = preferredWidth/components.size();
		}
		else if(heightType == DimensionParameters.FIXED && orientation == Orientation.VERTICAL){
			componentHeight = preferredHeight/components.size();
		}
		else if(widthType == DimensionParameters.AUTO && orientation == Orientation.VERTICAL){
			//TODO
		}
		else if(heightType == DimensionParameters.AUTO && orientation == Orientation.HORIZONTAL){
			//TODO
		}
		
		if(scrollable){
			
		}
		else{
			
		}
	}
	
	/* Quand la longueur de la liste est en AUTO */
	public int getMaximumChildWidth(){
		int maxWidth = 0;
		
		for(IvyComponent comp: components){
			if(comp.getTotalWidth() > maxWidth){
				maxWidth = comp.getTotalWidth();
			}
		}
		
		return maxWidth;
	}
	
	/* Quand la hauteur de la liste est en AUTO */
	public int getMaximumChildHeight(){
		int maxHeight = 0;
		
		for(IvyComponent comp: components){
			if(comp.getTotalHeight() > maxHeight){
				maxHeight = comp.getTotalWidth();
			}
		}
		
		return maxHeight;
	}
	
	public final int getVisibleComponentsNb(){
		return visibleComponents;
	}
	
	public int getSelectedIndex(){
		return selectedIndex;
	}
	
	public final boolean isSelectedCentered(){
		return centerSelected;
	}
	
	public final void setSelectedCentered(boolean centered){
		centerSelected = centered;
	}
	
	public final void setVisibleComponentsNb(int nb){
		if(nb > 0){
			visibleComponents = nb;
		}
	}	
}
