package framework;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.Image;
import java.awt.Toolkit;
import java.net.URL;

import javax.swing.JPanel;

import event.PadEvent;

import activity.BaseActivity;

public class GamePanel extends JPanel {
	private Ponger[] pongers = new Ponger[2];
	private Image pongSprite;
	private BaseActivity activity;
	
	private int width = 800;
	private int height = 480;

	public GamePanel(BaseActivity act){
		activity = act;

		URL iconURL;

		iconURL =  ClassLoader.getSystemResource("pong.png");
		pongSprite = Toolkit.getDefaultToolkit().getImage(iconURL);
		
		this.setPreferredSize(new Dimension(width, height));
	}

	public void initPongers(){
		pongers[0] = new Ponger(this,
				10,
				height/2 - pongSprite.getHeight(null)/2, 
				pongSprite.getWidth(null),
				pongSprite.getHeight(null));
		pongers[1] = new Ponger(this, 
				//width - pongSprite.getWidth(null) - 1,
				width - 30,
				height/2 - pongSprite.getHeight(null)/2,
				pongSprite.getWidth(null),
				pongSprite.getHeight(null));
	}

	public void update(){
		if(activity.isPadDownForPlayer(0, PadEvent.UP)){
			pongers[0].moveUp();
		}
		else if(activity.isPadDownForPlayer(0, PadEvent.DOWN)){
			pongers[0].moveDown();
		}
		
		if(activity.isPadDownForPlayer(1, PadEvent.UP)){
			pongers[1].moveUp();
		}
		else if(activity.isPadDownForPlayer(1, PadEvent.DOWN)){
			pongers[1].moveDown();
		}

		this.repaint();
	}

	public void paintComponent(Graphics g){
		super.paintComponent(g);

		g.setColor(Color.BLACK);
		g.fillRect(0, 0, this.getWidth(), this.getHeight());

		g.drawImage(pongSprite, pongers[0].getX(), pongers[0].getY(), pongSprite.getWidth(null), pongSprite.getHeight(null), null);
		g.drawImage(pongSprite, pongers[1].getX(), pongers[1].getY(), pongSprite.getWidth(null), pongSprite.getHeight(null), null);
	}
}
