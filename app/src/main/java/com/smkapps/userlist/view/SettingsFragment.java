package com.smkapps.userlist.view;

import android.os.Bundle;
import android.support.v7.preference.PreferenceFragmentCompat;

import com.smkapps.userlist.R;

/**
 * Created by Solo_Family on 15.09.2017.
 */

public class SettingsFragment extends PreferenceFragmentCompat {
    @Override
    public void onCreatePreferences(Bundle savedInstanceState, String rootKey) {
        addPreferencesFromResource(R.xml.settings);
    }
}
