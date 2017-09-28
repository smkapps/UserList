package com.smkapps.userlist.adapters;


import android.content.Context;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ArrayAdapter;
import android.widget.Filter;
import android.widget.ImageView;
import android.widget.TextView;

import com.smkapps.userlist.R;
import com.smkapps.userlist.model.User;

import java.util.ArrayList;
import java.util.List;

public class UserAdapterList extends ArrayAdapter<User> {

    protected List<User> userList;
//    Context mContext;

    public UserAdapterList(@NonNull List<User> objects, @NonNull Context context) {
        super(context, 0, objects);
        userList = new ArrayList<>(objects);
    }

    @NonNull
    @Override
    public View getView(int i, @Nullable View convertView, @NonNull ViewGroup parent) {

        if (convertView == null) {
            convertView = LayoutInflater.from(getContext()).inflate(R.layout.user_item, parent, false);
        }
        TextView name = convertView.findViewById(R.id.textViewName);
        TextView place = convertView.findViewById(R.id.textViewPlace);
        ImageView imageViewPhoto = convertView.findViewById(R.id.imageView);
        User user = getItem(i);
        name.setText(user.getFirstName() + " " + user.getSecondName() + ", возраст: " + user.getAge());
        place.setText(user.getCountry() + ", " + user.getCity());
        imageViewPhoto.setImageResource(user.getPhotoID());
        return convertView;
    }

    @Override
    public Filter getFilter() {
        return new Filter() {
            @Override
            protected FilterResults performFiltering(CharSequence constraint) {
                constraint = constraint.toString().toLowerCase();
                FilterResults result = new FilterResults();

                if (constraint != null && constraint.toString().length() > 0) {
                    List<User> founded = new ArrayList<>();

                    for (User user : userList) {
                        if (user.getSecondName().toLowerCase().contains(constraint))
                            founded.add(user);
                    }
                    result.values = founded;
                    result.count = founded.size();
                } else {
                    result.values = userList;
                    result.count = userList.size();
                }
                return result;
            }
            @Override
            protected void publishResults(CharSequence charSequence, FilterResults filterResults) {
                clear();
                for (User user : (List<User>) filterResults.values) {
                    add(user);
                }
                notifyDataSetChanged();
            }
        };
    }
}
