package framework;

import java.awt.BasicStroke;
import java.awt.Color;
import java.awt.FontMetrics;
import java.awt.Graphics;
import java.awt.Graphics2D;

public class IvyButton extends IvyTextComponent {
	public IvyButton(){
		borderType = Border.ROUNDED;
		borderColor = Color.DARK_GRAY;
		backgroundColor = Color.LIGHT_GRAY;
	}

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

		int xStart = x + marginLeft, newWidth = allowedWidth - marginLeft - marginRight,
				yStart = y + marginTop, newHeight = allowedHeight - marginTop - marginBottom;

		Graphics2D g2d = (Graphics2D)g;

		if(borderType == Border.ROUNDED){
			g2d.setColor(backgroundColor);
			g2d.fillRoundRect(xStart, yStart, newWidth, newHeight, newWidth/5, newHeight/5);

			//g2d.setStroke(new BasicStroke(3));
			g2d.setColor(borderColor);
			g2d.drawRoundRect(xStart, yStart, newWidth, newHeight, newWidth/5, newHeight/5);

			if(textAlignment == Alignment.LEFT){
				//TODO: changer en fonction de l'epaisseur du contour du bouton
				g2d.drawString("k", xStart, yStart);
				
				/*if(textAlignment == Alignment.LEFT){
					//TODO: probleme avec getFontMetrics()
					return;
				}
				
				FontMetrics metrics = g2d.getFontMetrics();
				// get the height of a line of text in this
				// font and render context
				int hgt = metrics.getHeight();
				// get the advance of my text in this font
				// and render context
				int adv = metrics.stringWidth(text);
				// calculate the size of a box to hold the
				// text with some padding.
				//g2d.drawString(text, xStart + (newWidth - (adv+2))/2, yStart + (newHeight - (hgt+2))/2);
				
				int fontSize = g2d.getFont().getSize();
				int i = 0;
				
				while((newWidth < adv || newHeight < hgt) && fontSize > 0){
					fontSize--;
					g2d.setFont(g2d.getFont().deriveFont(fontSize));
					
					metrics = g2d.getFontMetrics();
					hgt = metrics.getHeight();
					adv = metrics.stringWidth(text);
					i++;
				}
				
				//g2d.drawString(text, xStart, yStart);
				//g2d.drawString("Hello", xStart, yStart);*/
			}
			else if(textAlignment == Alignment.CENTER){
				FontMetrics metrics = g2d.getFontMetrics();
				// get the height of a line of text in this
				// font and render context
				int hgt = metrics.getHeight();
				// get the advance of my text in this font
				// and render context
				int adv = metrics.stringWidth(text);
				// calculate the size of a box to hold the
				// text with some padding.
				//g2d.drawString(text, xStart + (newWidth - (adv+2))/2, yStart + (newHeight - (hgt+2))/2);
				
				int fontSize = g2d.getFont().getSize();
				
				while(newWidth < adv && newHeight < hgt){
					fontSize--;
					g2d.setFont(g2d.getFont().deriveFont(fontSize));
					
					metrics = g2d.getFontMetrics();
					hgt = metrics.getHeight();
					adv = metrics.stringWidth(text);
				}
				
				g2d.drawString(text, xStart, yStart);
			}
			else if(textAlignment == Alignment.RIGHT){
				//TODO: aligner a droite
				FontMetrics metrics = g2d.getFontMetrics(g2d.getFont());
				// get the height of a line of text in this
				// font and render context
				int hgt = metrics.getHeight();
				// get the advance of my text in this font
				// and render context
				int adv = metrics.stringWidth(text);
				// calculate the size of a box to hold the
				// text with some padding.
				g2d.drawString(text, xStart + (newWidth - (adv+2))/2, yStart + (newHeight - (hgt+2))/2);
			}
			//g2d.setStroke(new BasicStroke(1));
		}
		else if(borderType == Border.SQUARED){
			g2d.setColor(backgroundColor);
			g2d.fillRect(xStart, yStart, newWidth, newHeight);

			g2d.setStroke(new BasicStroke(3));
			g2d.setColor(borderColor);
			g2d.drawRect(xStart, yStart, newWidth, newHeight);
			g2d.setStroke(new BasicStroke(1));
		}
		else if(borderType == Border.NONE){
			g2d.setColor(backgroundColor);
			g2d.fillRect(xStart, yStart, newWidth, newHeight);
		}
	}
}
