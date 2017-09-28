package com.smkapps.userlist.model;

import com.smkapps.userlist.R;

import java.util.Random;

public class FakeUserGenerator {

    private static String[] fakeNamesArray = {"Петр", "Василий", "Алексей", "Дмитрий", "Иван", "Станислав", "Михаил", "Валентин", "Игнат", "Максим"};
    private static String[] fakeSecondNamesArray = {"Юрченко", "Петров", "Сидоров", "Шевченко", "Петренко", "Степаненко", "Дмитриев", "Алексеев", "Александров", "Иваненко"};
    private static String [] fakeCountriesArray = {"Украина", "Германия", "Франция"};
    private static String [] ukrainianCitiesArray = {"Днепр", "Киев", "Одесса", "Харьков", "Львов"};
    private static String [] germanCitiesArray = {"Берлин", "Мюнхен", "Штутгарт", "Кельн", "Дортмунд"};
    private static String [] frenchCitiesArray = {"Париж", "Марсель", "Нант", "Тулуза", "Ницца"};
    private static int [] userPhotoIDArray = {R.drawable.man1, R.drawable.man2, R.drawable.man3, R.drawable.man4, R.drawable.man5};

    public static User generateFakeUser () {

        Random random = new Random();
        int nameIndex = random.nextInt(10);
        int secondNameIndex = random.nextInt(10);
        int countryIndex = random.nextInt(3);
        int cityIndex = random.nextInt(3);
        String firstName = fakeNamesArray[nameIndex];
        String secondName = fakeSecondNamesArray[secondNameIndex];
        int age = random.nextInt(86)+14;
        int photoIDIndex = random.nextInt(5);
        int photoUserID = userPhotoIDArray[photoIDIndex];
        String country = fakeCountriesArray[countryIndex];
        String city = null;

        switch (countryIndex) {

            case 0: city = ukrainianCitiesArray[cityIndex];
                break;
            case 1: city = germanCitiesArray[cityIndex];
                break;
            case 2: city = frenchCitiesArray[cityIndex];
                break;
            default:
                break;
        }

        return new User(firstName, secondName, photoUserID, age, country, city);
    }
}

