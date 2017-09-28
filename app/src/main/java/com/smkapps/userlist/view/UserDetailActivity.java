package com.smkapps.userlist.view;

import android.databinding.DataBindingUtil;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;

import com.smkapps.userlist.R;
import com.smkapps.userlist.model.User;
import com.smkapps.userlist.model.UsersLab;
import com.smkapps.userlist.databinding.ActivityUserDetailBinding;

import java.util.UUID;

public class UserDetailActivity extends AppCompatActivity {
    public static final String EXTRA_DETAIL_USER = "userdetail";
    ActivityUserDetailBinding binding;
    User user;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        binding = DataBindingUtil.setContentView(this, R.layout.activity_user_detail);
        UUID id = (UUID) getIntent().getSerializableExtra(EXTRA_DETAIL_USER);
        user = UsersLab.getInstance().findUserById(id);
        final int index = UsersLab.getInstance().getUserList().indexOf(user);
        setSupportActionBar(binding.toolbar);
        getSupportActionBar().setTitle(user.getFirstName()+" " + user.getSecondName());
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);

        binding.photoIv.setImageResource(user.getPhotoID());
        binding.firsttNameEt.setText(user.getFirstName());
        binding.lastNameEt.setText(user.getSecondName());
        binding.ageEt.setText(user.getAge()+"");
        binding.countryEt.setText(user.getCountry());
        binding.cityEt.setText(user.getCity());

        binding.saveBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                user.setFirstName(binding.firsttNameEt.getText().toString());
                user.setSecondName(binding.lastNameEt.getText().toString());
                if(!binding.ageEt.getText().toString().isEmpty())
                user.setAge(Integer.parseInt(binding.ageEt.getText().toString()));
                user.setCountry(binding.countryEt.getText().toString());
                user.setCity(binding.cityEt.getText().toString());

                UsersLab.getInstance().getUserList().set(index, user);
                setResult(RESULT_OK);
                finish();
            }
        });
    }

}
