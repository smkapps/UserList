package com.smkapps.userlist.model;

import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public class UsersLab {
    private static UsersLab ourInstance;
    private List<User> userList = new ArrayList<>(); ;
    private static final int usersCount = 100;



    public static UsersLab getInstance() {
        if (ourInstance == null)
            ourInstance = new UsersLab();
        return ourInstance;
    }

    private UsersLab() {
        userList = new ArrayList<>();
        for (int i = 0; i < usersCount; i++) {
            userList.add(FakeUserGenerator.generateFakeUser());
        }
    }

    public List<User> getUserList() {
        return userList;
    }

    public User findUserById(UUID id){
        for(User user: userList){
            if (user.getUuid().equals(id)){
                return user;
            }
        }
        return null;
    }

}
