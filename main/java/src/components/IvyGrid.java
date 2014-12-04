package components;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.GridBagConstraints;
import java.awt.GridBagLayout;

import javax.swing.JButton;
import javax.swing.JComponent;
import javax.swing.JFrame;
import javax.swing.JPanel;

public class IvyGrid extends IvyContainer {
	private int rows = 1, cols = 1;
	private IvyComponent[][] grid;
	
	private JPanel gridPanel;
	private GridBagConstraints constr;

	public IvyGrid(int rowNb, int colNb){
		gridPanel = new JPanel(new GridBagLayout());
		constr = new GridBagConstraints();
		
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
		setComponentAt(comp, comp.getRow(), comp.getCol());
	}

	public void setComponentAt(IvyComponent comp, int r, int c){
		if(r >= 0 && r < rows && c >= 0 && c < cols){
			grid[r][c] = comp;
			
			constr.gridx = c;
			constr.gridy = r;
			constr.gridwidth = comp.getColSpan();
			constr.gridheight = comp.getRowSpan();
			
			if(comp.getWidthType() == DimensionParameters.FILL && comp.getHeightType() == DimensionParameters.FILL){
				constr.fill = GridBagConstraints.BOTH;
			}
			else if(comp.getWidthType() == DimensionParameters.FILL){
				constr.fill = GridBagConstraints.HORIZONTAL;
			}
			else if(comp.getHeightType() == DimensionParameters.FILL){
				constr.fill = GridBagConstraints.VERTICAL;
			}
			
			gridPanel.add(comp.getComponent(), constr);
		}
	}

	@Override
	public void draw(Graphics g) {}

	@Override
	public void draw(Graphics g, int x, int y, int allowedWidth, int allowedHeight){	
		if(!isVisible()){
			return;
		}

		gridPanel.addNotify();
		gridPanel.validate();
		gridPanel.setSize(allowedWidth - x, allowedHeight - y);
		gridPanel.paintComponents(g);
	}

	@Override
	public JComponent getComponent() {
		return gridPanel;
	}
}
