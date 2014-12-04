package components;

import java.awt.Color;
import java.awt.Graphics;

import javax.swing.JComponent;

public abstract class IvyComponent {
	public static class DimensionParameters {
		public static final int FILL = 0;
		public static final int AUTO = 1;
		public static final int FIXED = 2;
	}
	
	public static class Orientation {
		public static final int HORIZONTAL = 0;
		public static final int VERTICAL = 1;
	}
	
	public static class Alignment {
		public static final int LEFT = 0;
		public static final int CENTER = 1;
		public static final int RIGHT = 2;
	}
	
	public static class Border {
		public static final int SQUARED = 0;
		public static final int ROUNDED = 1;
		public static final int NONE = 2;
	}

	protected int marginLeft = 0, marginTop = 0, marginRight = 0, marginBottom = 0;

	protected int rowSpan = 1, colSpan = 1;
	protected int row = 0, col = 0;

	protected int preferredWidth = -1, preferredHeight = -1, assignedWidth = -1, assignedHeight = -1;
	protected int x, y;

	protected int widthType = DimensionParameters.FILL, heightType = DimensionParameters.FILL;
	
	protected int borderType = Border.NONE;
	protected Color borderColor = Color.BLACK;
	protected Color backgroundColor = null;
	
	private String id = null;

	public final void setMargin(int margin){
		marginLeft = margin;
		marginTop = margin;
		marginRight = margin;
		marginBottom = margin;
	}

	public final void setMargin(int marginLeftRight, int marginTopBottom){
		marginLeft = marginLeftRight;
		marginRight = marginLeftRight;
		marginTop = marginTopBottom;
		marginBottom = marginTopBottom;
	}

	public final void setMargin(int margLeft, int margTop, int margRight, int margBottom){
		marginLeft = margLeft;
		marginRight = margRight;
		marginTop = margTop;
		marginBottom = margBottom;
	}

	public final void setWidth(int w){
		if(w >= 0){
			if(widthType == DimensionParameters.FILL){
				preferredWidth = w;
			}
			else{
				preferredWidth = w;
				widthType = DimensionParameters.FIXED;
			}
		}
	}

	public final void setHeight(int h){
		if(h >= 0){
			if(heightType == DimensionParameters.FILL){
				preferredHeight = h;
			}
			else{
				preferredHeight = h;
				heightType = DimensionParameters.FIXED;
			}
		}
	}

	public final void setWidthType(int wType){
		/* TODO: modifier lorsque nouvelle valeur */
		if(wType == DimensionParameters.AUTO
				|| wType == DimensionParameters.FILL
				|| wType == DimensionParameters.FIXED){
			widthType = wType;
		}
	}

	public final void setHeightType(int hType){
		/* TODO: modifier lorsque nouvelle valeur */
		if(hType == DimensionParameters.AUTO
				|| hType == DimensionParameters.FILL
				|| hType == DimensionParameters.FIXED){
			heightType = hType;
		}
	}

	public final int getWidthType(){
		return widthType;
	}

	public final int getHeightType(){
		return heightType;
	}
	
	public abstract void draw(Graphics g);
	public abstract void draw(Graphics g, int x, int y, int allowedWidth, int allowedHeight);
	
	public final int getTotalWidth(){
		return marginLeft + preferredWidth + marginRight;
	}
	
	public final int getTotalHeight(){
		return marginTop + preferredHeight + marginBottom;
	}
	
	public final void setVisible(boolean vsbl){
		getComponent().setVisible(vsbl);
	}
	
	public final boolean isVisible(){
		return getComponent().isVisible();
	}
	
	public final void setEnabled(boolean enab){
		getComponent().setEnabled(enab);
	}
	
	public final boolean isEnabled(){
		return getComponent().isEnabled();
	}
	
	public abstract JComponent getComponent();
	
	public void setBorderType(int bType){		
		/* TODO: mettre les bonnes bordures */
		
		switch(bType){
		case Border.NONE:
			getComponent().setBorder(null);
			break;
			
		case Border.ROUNDED:
			getComponent().setBorder(null);
			break;
			
		case Border.SQUARED:
			getComponent().setBorder(null);
			break;
		}
	}
	
	public final void setBackgroundColor(Color c){
		backgroundColor = c;
		//this.getComponent().setBackground(c);
	}
	
	public final int getRow(){
		return row;
	}
	
	public final int getCol(){
		return col;
	}
	
	public final int getRowSpan(){
		return rowSpan;
	}
	
	public final int getColSpan(){
		return colSpan;
	}
	
	public final void setRow(int r){
		if(r >= 0){
			row = r;
		}
	}
	
	public final void setCol(int c){
		if(c >= 0){
			col = c;
		}
	}
	
	public final void setRowSpan(int rs){
		if(rs > 0){
			rowSpan = rs;
		}
	}
	
	public final void setColSpan(int cs){
		if(cs > 0){
			colSpan = cs;
		}
	}
}
