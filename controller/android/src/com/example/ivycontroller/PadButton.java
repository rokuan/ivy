package com.example.ivycontroller;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.widget.Button;
import android.widget.ImageButton;

public class PadButton extends ImageButton {
//public class PadButton extends Button {
	protected int buttonCode;
	
	public PadButton(Context context) {
		super(context);
	}
	
	public PadButton(Context context, AttributeSet attrs) {
		super(context, attrs);
		
		TypedArray ta = context.obtainStyledAttributes(attrs,
				R.styleable.PadButton, 0, 0);

		buttonCode = ta.getInt(R.styleable.PadButton_code, -1);
		
		ta.recycle();
	}
	
	public PadButton(Context context, AttributeSet attrs, int defStyle) {
		this(context, attrs);
	}

	public int getCode(){
		return buttonCode;
	}
}
