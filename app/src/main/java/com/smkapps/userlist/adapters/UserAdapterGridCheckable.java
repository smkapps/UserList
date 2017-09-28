package com.smkapps.userlist.adapters;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.smkapps.userlist.R;
import com.smkapps.userlist.model.User;

import java.util.List;

public class UserAdapterGridCheckable extends UserAdapterGrid {
    public UserAdapterGridCheckable(@NonNull List<User> objects, @NonNull Context context) {
        super(objects, context);
    }
    @NonNull
    @Override
    public View getView(int i, @Nullable View convertView, @NonNull ViewGroup parent) {
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.user_item_grid_choice, parent, false);
        }
        TextView name = convertView.findViewById(R.id.textViewName1);
        ImageView imageViewPhoto = convertView.findViewById(R.id.imageViewGrid);
        User user = getItem(i);
//        name.setText(user.getFirstName() + " " + user.getSecondName() + ", возраст: " + userList.get(i).getAge());
        name.setText(user.getFirstName() + ", " + user.getAge());
        imageViewPhoto.setImageResource(user.getPhotoID());
        return convertView;
    }
}
