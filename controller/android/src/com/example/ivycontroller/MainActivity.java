package com.example.ivycontroller;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.OutputStream;
import java.lang.ref.WeakReference;
import java.net.Socket;
import java.net.UnknownHostException;
import java.util.ArrayList;

import org.apache.http.HttpResponse;
import org.apache.http.NameValuePair;
import org.apache.http.client.HttpClient;
import org.apache.http.client.entity.UrlEncodedFormEntity;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.impl.client.DefaultHttpClient;
import org.apache.http.message.BasicNameValuePair;

import android.os.AsyncTask;
import android.os.Bundle;
import android.app.Activity;
import android.graphics.Color;
import android.view.Menu;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.Toast;

public class MainActivity extends Activity implements OnTouchListener {
	protected static final int maxButtonNb = 15;

	protected static final int LEFT = 0;
	protected static final int UP_LEFT = 1;
	protected static final int UP = 2;
	protected static final int UP_RIGHT = 3;
	protected static final int RIGHT = 4;
	protected static final int DOWN_RIGHT = 5;
	protected static final int DOWN = 6;
	protected static final int DOWN_LEFT = 7;
	protected static final int T = 8;
	protected static final int H = 9;
	protected static final int I = 10;
	protected static final int S = 11;
	protected static final int SELECT = 12;
	protected static final int PAUSE = 13;
	protected static final int START = 14;

	private static final byte DOWN_STATE = 0;
	private static final byte UP_STATE = 1;
	private static final byte MOVE_STATE = 2;

	protected PadButton[] padButtons = new PadButton[maxButtonNb];

	private Socket s;
	private OutputStream os;

	private Runnable runnable = null;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		// setContentView(R.layout.activity_main);
		setContentView(R.layout.controller);

		s = null;
		os = null;

		//TODO
		String serverAddress = null;

		runnable = new Runnable() {
			public void run() {
				try {
					s = new Socket("192.168.0.12", 7890);
					os = s.getOutputStream();
				} catch (UnknownHostException e) {
				} catch (IOException e) {
				}
			}
		};

		initButtons();
		connect();
	}

	public void connect() {
		if (s == null) {
			new Thread(runnable).start();
		}
	}

	public void initButtons() {
		padButtons[LEFT] = (PadButton) findViewById(R.id.left_ctrl);
		padButtons[UP_LEFT] = (PadButton) findViewById(R.id.up_left_ctrl);
		padButtons[UP] = (PadButton) findViewById(R.id.up_ctrl);
		padButtons[UP_RIGHT] = (PadButton) findViewById(R.id.up_right_ctrl);
		padButtons[RIGHT] = (PadButton) findViewById(R.id.right_ctrl);
		padButtons[DOWN_RIGHT] = (PadButton) findViewById(R.id.down_right_ctrl);
		padButtons[DOWN] = (PadButton) findViewById(R.id.down_ctrl);
		padButtons[DOWN_LEFT] = (PadButton) findViewById(R.id.down_left_ctrl);
		padButtons[T] = (PadButton) findViewById(R.id.t_ctrl);
		padButtons[H] = (PadButton) findViewById(R.id.h_ctrl);
		padButtons[I] = (PadButton) findViewById(R.id.i_ctrl);
		padButtons[S] = (PadButton) findViewById(R.id.s_ctrl);

		for (int i = 0; i < padButtons.length; i++) {
			if (padButtons[i] != null) {
				padButtons[i].setOnTouchListener(this);
			}
		}
	}

	public void onPause() {
		try {
			if (os != null) {
				os.write('D');
				os.flush();
			}
		} catch (IOException e) {
		}

		try {
			if (os != null) {
				os.close();
			}
		} catch (IOException e) {
		}
		try {
			if (s != null) {
				s.close();
			}
		} catch (IOException e) {
		}

		os = null;
		s = null;

		super.onPause();
	}

	public void onResume() {
		super.onResume();

		connect();
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.main, menu);
		return true;
	}

	@Override
	public boolean onTouch(View arg0, MotionEvent arg1) {
		if (arg0 instanceof PadButton) {
			byte state = DOWN_STATE;
			int code = ((PadButton) arg0).getCode();

			switch (arg1.getAction()) {
			case MotionEvent.ACTION_DOWN:
				state = DOWN_STATE;
				switchDown(code);
				break;

			case MotionEvent.ACTION_UP:
			case MotionEvent.ACTION_CANCEL:
				state = UP_STATE;
				switchUp(code);
				break;

			default:
				return false;
			}

			try {
				if (os != null) {
					os.write('P');
					os.write(code);
					os.write(state);
					os.flush();
				}
			} catch (IOException e) {
			}

			return true;
			// return false;
		}

		return false;
	}

	private void switchDown(int padCode) {
		int c = Color.rgb(0, 127, 255);

		switch (padCode) {
		case UP_LEFT:
			padButtons[LEFT].setBackgroundColor(c);
			padButtons[UP].setBackgroundColor(c);
			break;

		case UP_RIGHT:
			padButtons[RIGHT].setBackgroundColor(c);
			padButtons[UP].setBackgroundColor(c);
			break;

		case DOWN_RIGHT:
			padButtons[RIGHT].setBackgroundColor(c);
			padButtons[DOWN].setBackgroundColor(c);
			break;

		case DOWN_LEFT:
			padButtons[LEFT].setBackgroundColor(c);
			padButtons[DOWN].setBackgroundColor(c);
			break;

		default:
			padButtons[padCode].setBackgroundColor(c);
			break;
		}
	}

	private void switchUp(int padCode) {
		int c = Color.TRANSPARENT;

		switch (padCode) {
		case UP_LEFT:
			padButtons[LEFT].setBackgroundColor(c);
			padButtons[UP].setBackgroundColor(c);
			break;

		case UP_RIGHT:
			padButtons[RIGHT].setBackgroundColor(c);
			padButtons[UP].setBackgroundColor(c);
			break;

		case DOWN_RIGHT:
			padButtons[RIGHT].setBackgroundColor(c);
			padButtons[DOWN].setBackgroundColor(c);
			break;

		case DOWN_LEFT:
			padButtons[LEFT].setBackgroundColor(c);
			padButtons[DOWN].setBackgroundColor(c);
			break;

		default:
			padButtons[padCode].setBackgroundColor(c);
			break;
		}
	}

	public void hidePad() {

	}

	public synchronized void pauseGame() {

	}

	public synchronized void resumeGame() {

	}
}
