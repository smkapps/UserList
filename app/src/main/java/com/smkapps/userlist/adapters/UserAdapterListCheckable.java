package com.smkapps.userlist.adapters;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CheckedTextView;
import android.widget.ImageView;
import android.widget.TextView;

import com.smkapps.userlist.R;
import com.smkapps.userlist.model.User;

import java.util.List;


public class UserAdapterListCheckable extends UserAdapterList {

    public UserAdapterListCheckable(List<User> userList, Context mContext) {
        super(userList, mContext);
    }

    @NonNull
    @Override
    public View getView(int i, @Nullable View convertView, @NonNull ViewGroup parent) {
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.user_item_choice, parent, false);
        }
        CheckedTextView name = convertView.findViewById(R.id.textViewName);
        TextView place = convertView.findViewById(R.id.textViewPlace);
        ImageView imageViewPhoto = convertView.findViewById(R.id.imageView);

        User user = getItem(i);
        name.setText(user.getFirstName() + " "+ user.getSecondName() + ", возраст: " + user.getAge());
        place.setText(user.getCountry() + ", " + user.getCity());
        imageViewPhoto.setImageResource(user.getPhotoID());

        return convertView;
    }
}
