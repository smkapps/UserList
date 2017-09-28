package com.smkapps.userlist.model;

import java.util.UUID;

public class User {

    private UUID uuid;
    private String firstName;
    private String secondName;
    private int photoID;
    private int age;
    private String country;
    private String city;

    public User(String firstName, String secondName, int photoID, int age, String country, String city) {
        uuid = UUID.randomUUID();
        this.firstName = firstName;
        this.secondName = secondName;
        this.photoID = photoID;
        this.age = age;
        this.country = country;
        this.city = city;
    }

    public UUID getUuid() {
        return uuid;
    }

    public String getFirstName() {
        return firstName;
    }

    public void setFirstName(String firstName) {
        this.firstName = firstName;
    }

    public String getSecondName() {
        return secondName;
    }

    public void setSecondName(String secondName) {
        this.secondName = secondName;
    }

    public int getPhotoID() {
        return photoID;
    }

    public void setPhotoID(int photoID) {
        this.photoID = photoID;
    }

    public int getAge() {
        return age;
    }

    public void setAge(int age) {
        this.age = age;
    }

    public String getCountry() {
        return country;
    }

    public void setCountry(String country) {
        this.country = country;
    }

    public String getCity() {
        return city;
    }

    public void setCity(String city) {
        this.city = city;
    }

    @Override
    public boolean equals(Object obj) {
        return ((User)obj).getUuid().equals(this.getUuid());
    }

    @Override
    public int hashCode() {
        return this.getUuid().hashCode();
    }
}
