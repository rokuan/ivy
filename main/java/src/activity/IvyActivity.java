package activity;

import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

import javax.swing.JFrame;

import event.AccelerometerEvent;
import event.PadEvent;
import event.TouchEvent;
import framework.GamePanel;

public class IvyActivity extends BaseActivity {
	private JFrame frame;
	private GamePanel gPanel;

	public IvyActivity(){
		super();
	}

	public IvyActivity(int maxPlayers){
		super(maxPlayers);
	}

	public void createGui(){
		frame = new JFrame();

		//frame.setSize(800, 480);
		frame.setTitle("Ivy Game");

		//JPanel game = new JPanel(new BorderLayout());
		
		gPanel = new GamePanel(this);
		gPanel.initPongers();

		ActionListener taskPerformer = new ActionListener() {
			public void actionPerformed(ActionEvent evt) {
				gPanel.update();
			}
		};

		new javax.swing.Timer(60, taskPerformer).start();

		//game.add(gPanel, BorderLayout.CENTER);
		frame.setContentPane(gPanel);
		
		frame.pack();
		frame.setResizable(false);
		frame.setLocationRelativeTo(null);
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		frame.setVisible(true);
	}

	@Override
	public boolean onPadEvent(PadEvent pe) {
		//System.out.println(pe);
		return false;
	}

	@Override
	public boolean onTouchEvent(TouchEvent te) {
		//System.out.println(te);
		return false;
	}

	@Override
	public boolean onAcceleratorEvent(AccelerometerEvent ae) {
		//System.out.println(ae);
		return false;
	}

	public static void main(String[] args){
		javax.swing.SwingUtilities.invokeLater(new Runnable(){
			public void run(){
				final IvyActivity ia = new IvyActivity(2);

				ia.createGui();

				new Thread(new Runnable(){
					public void run(){ 
						try {
							ia.connect();
							ia.waitForClients();
						} catch (Exception e) {
							e.printStackTrace();
						}
					}
				}).start();
			}
		});
	}
}
