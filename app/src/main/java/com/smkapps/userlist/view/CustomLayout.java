package com.smkapps.userlist.view;

import android.content.Context;
import android.support.constraint.ConstraintLayout;
import android.util.AttributeSet;
import android.widget.CheckBox;
import android.widget.Checkable;
import android.widget.CheckedTextView;

import com.smkapps.userlist.R;

public class CustomLayout extends ConstraintLayout implements Checkable{
    public CustomLayout(Context context, AttributeSet attrs) {
        super(context, attrs);
    }


    private boolean checked;


    @Override
    public boolean isChecked() {
        return checked;
    }

    @Override
    public void setChecked(boolean checked) {
        this.checked = checked;

        CheckedTextView iv = (CheckedTextView) findViewById(R.id.textViewName);
        if(iv !=null)
        iv.setChecked(checked ? true : false);

        CheckBox ch = (CheckBox) findViewById(R.id.checkBox);
        if(ch !=null)
        ch.setChecked(checked ? true : false);
    }

    @Override
    public void toggle() {
        this.checked = !this.checked;
    }
}
