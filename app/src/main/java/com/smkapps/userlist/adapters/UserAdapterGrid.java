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

public class UserAdapterGrid extends UserAdapterList {

    public UserAdapterGrid(@NonNull List<User> objects, @NonNull Context context) {
        super(objects, context);
    }

    @NonNull
    @Override
    public View getView(int i, @Nullable View convertView, @NonNull ViewGroup parent) {
        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.user_item_grid, parent, false);
        }
        TextView name = convertView.findViewById(R.id.nameGrid);
        ImageView imageViewPhoto = convertView.findViewById(R.id.imageViewGrid);
        User user = getItem(i);
//        name.setText(user.getFirstName() + " " + user.getSecondName() + ", возраст: " + userList.get(i).getAge());
        name.setText(user.getFirstName() + ", " + user.getAge());
        imageViewPhoto.setImageResource(user.getPhotoID());
        return convertView;
    }
}


//{
//
//    List<User> userList;
//    Context mContext;
//
//    public UserAdapterGrid(List<User> userList, Context mContext) {
//        this.userList = userList;
//        this.mContext = mContext;
//    }
//
//    @Override
//    public int getCount() {
//        return userList.size();
//    }
//
//    @Override
//    public Object getItem(int i) {
//        return userList.get(i);
//    }
//
//    @Override
//    public long getItemId(int i) {
//        return 0;
//    }
//
//    @Override
//    public View getView(int i, View view, ViewGroup viewGroup) {
//
//        View customView = LayoutInflater.from(mContext).inflate(R.layout.user_item_grid, viewGroup, false);
//        TextView name = customView.findViewById(R.id.nameGrid);
//        ImageView imageViewPhoto = customView.findViewById(R.id.imageViewGrid);
//
//
//        User user = userList.get(i);
//        name.setText(user.getFirstName() + ", " + userList.get(i).getAge());
//        imageViewPhoto.setImageResource(user.getPhotoID());
//
//        return customView;
//    }
//}
