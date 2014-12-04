package framework;

import java.awt.Graphics;

public class IvyGrid extends IvyContainer {
	private int rows = 1, cols = 1;
	private IvyComponent[][] grid;

	public IvyGrid(int rowNb, int colNb){
		if(rows > 0){
			rows = rowNb;
		}
		if(colNb > 0){
			cols = colNb;
		}

		grid = new IvyComponent[rows][cols];

		for(int r=0; r<rows; r++){
			for(int c=0; c<cols; c++){
				grid[r][c] = null;
			}
		}
	}

	public final void setRows(int r){
		if(r > 0){
			rows = r;

			grid = new IvyComponent[rows][cols];

			for(int i=0; i<rows; i++){
				for(int j=0; j<cols; j++){
					grid[i][j] = null;
				}
			}
		}
	}

	public final void setCols(int c){
		if(c > 0){
			cols = c;
			
			grid = new IvyComponent[rows][cols];

			for(int i=0; i<rows; i++){
				for(int j=0; j<cols; j++){
					grid[i][j] = null;
				}
			}
		}
	}

	public void addComponent(IvyComponent comp){
		int r = comp.row, c = comp.col;

		setComponentAt(comp, r, c);
	}

	public void setComponentAt(IvyComponent comp, int r, int c){
		if(r >= 0 && r < rows && c >= 0 && c < cols){
			grid[r][c] = comp;
		}
	}

	@Override
	public void draw(Graphics g) {}

	@Override
	public void draw(Graphics g, int x, int y, int allowedWidth, int allowedHeight){	
		if(!isVisible()){
			return;
		}

		g.setClip(x, y, allowedWidth, allowedHeight);

		int componentWidth = allowedWidth/cols, componentHeight = allowedHeight/rows;

		for(int i=0; i<rows; i++){
			for(int j=0; j<cols; j++){
				if(grid[i][j] != null){
					grid[i][j].draw(g, x + componentWidth * j,
							y + componentHeight * i, 
							componentWidth * colSpan, 
							componentHeight * rowSpan);
				}
			}
		}
	}
}
