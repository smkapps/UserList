package com.smkapps.userlist.view;

import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.preference.PreferenceManager;
import android.support.v7.view.ActionMode;
import android.support.v7.widget.SearchView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.util.SparseBooleanArray;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AbsListView;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Filterable;
import android.widget.GridView;
import android.widget.ListView;

import com.smkapps.userlist.R;
import com.smkapps.userlist.model.User;
import com.smkapps.userlist.model.UsersLab;
import com.smkapps.userlist.adapters.UserAdapterGrid;
import com.smkapps.userlist.adapters.UserAdapterGridCheckable;
import com.smkapps.userlist.adapters.UserAdapterList;
import com.smkapps.userlist.adapters.UserAdapterListCheckable;

import java.util.ArrayList;
import java.util.List;
import java.util.ListIterator;
import java.util.UUID;

public class MainActivity extends AppCompatActivity implements SharedPreferences.OnSharedPreferenceChangeListener, SearchView.OnQueryTextListener {
    final List<User> userListInitial = UsersLab.getInstance().getUserList();
    final List<User> userList = new ArrayList<>();

    Toolbar toolBar;
    ListView listView;
    GridView gridView;
    SharedPreferences preferences;
    boolean displayGridView;
    ArrayAdapter adapter;
    int columnCount;
    SearchView searchView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        preferences = PreferenceManager.getDefaultSharedPreferences(this);
        preferences.registerOnSharedPreferenceChangeListener(this);
        toolBar = (Toolbar) findViewById(R.id.toolbar);
        listView = (ListView) findViewById(R.id.listView);
        gridView = (GridView) findViewById(R.id.gridView);
        setSupportActionBar(toolBar);
        displayGridView = preferences.getBoolean("switch_grid", false);
        columnCount = Integer.parseInt(preferences.getString("number_of_columns", "3"));
        fillLayout(userListInitial);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu, menu);
        MenuItem searchItem = menu.findItem(R.id.item_search);
        searchView = (SearchView) searchItem.getActionView();
        searchView.setQueryHint("введите фамилию");
        searchView.setOnQueryTextListener(this);
        return true;

    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case R.id.item_settings: {
                startActivity(new Intent(MainActivity.this, SettingsActivity.class));
                break;
            }

        }
        return true;
    }

    @Override
    public void onSharedPreferenceChanged(SharedPreferences sharedPreferences, String key) {
        if (key.equals("switch_grid")) {
            displayGridView = sharedPreferences.getBoolean("switch_grid", false);
            fillLayout(userListInitial);
        }
        if (key.equals("number_of_columns"))
            columnCount = Integer.valueOf(sharedPreferences.getString("number_of_columns", "2"));
        gridView.setNumColumns(Integer.valueOf(sharedPreferences.getString("number_of_columns", "2")));

    }

    @Override
    public boolean onQueryTextSubmit(String query) {
        return false;
    }

    @Override
    public boolean onQueryTextChange(String newText) {
        if (!displayGridView)
            ((Filterable) listView.getAdapter()).getFilter().filter(newText);
        else ((Filterable) gridView.getAdapter()).getFilter().filter(newText);
        return true;
    }

    @Override
    protected void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        adapter.notifyDataSetChanged();
    }

    private void fillLayout(final List<User> userList1) {
        userList.clear();
        userList.addAll(userList1);
        if (!displayGridView) {
            gridView.setVisibility(View.GONE);
            listView.setVisibility(View.VISIBLE);
            adapter = new UserAdapterList(userList, this);
            listView.setAdapter(adapter);

            listView.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
                @Override
                public boolean onItemLongClick(AdapterView<?> adapterView, View view, int i, long l) {
                    adapter = new UserAdapterListCheckable(userList, MainActivity.this);
                    listView.setAdapter(adapter);
                    listView.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
                    listView.clearChoices();

                    final ContextMenuAction action = new ContextMenuAction();
                    startSupportActionMode(action);
                    listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                        @Override
                        public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                            if (listView.getCheckedItemCount() > 1) {
                                action.editItem.setVisible(false);
                            } else action.editItem.setVisible(true);

                        }
                    });
                    listView.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
                        @Override
                        public boolean onItemLongClick(AdapterView<?> adapterView, View view, int i, long l) {
                            return false;
                        }
                    });
                    return true;
                }
            });
        } else {
            listView.setVisibility(View.GONE);
            gridView.setVisibility(View.VISIBLE);
            gridView.setNumColumns(columnCount);
            adapter = new UserAdapterGrid(userList, this);
            gridView.setAdapter(adapter);
            gridView.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
                @Override
                public boolean onItemLongClick(AdapterView<?> adapterView, View view, int i, long l) {
                    adapter = new UserAdapterGridCheckable(userList, MainActivity.this);
                    gridView.setAdapter(adapter);
                    gridView.clearChoices();
                    gridView.setChoiceMode(AbsListView.CHOICE_MODE_MULTIPLE);
                    final ContextMenuAction action = new ContextMenuAction();
                    startSupportActionMode(action);
                    gridView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
                        @Override
                        public void onItemClick(AdapterView<?> adapterView, View view, int i, long l) {
                            if (gridView.getCheckedItemCount() > 1) {
                                Log.i("tag", "second");
                                action.editItem.setVisible(false);
                            } else action.editItem.setVisible(true);
                        }
                    });
                    gridView.setOnItemLongClickListener(new AdapterView.OnItemLongClickListener() {
                        @Override
                        public boolean onItemLongClick(AdapterView<?> adapterView, View view, int i, long l) {
                            return false;
                        }
                    });
                    return true;
                }
            });
        }
    }

    class ContextMenuAction implements ActionMode.Callback {
        private MenuItem editItem;

        @Override
        public boolean onCreateActionMode(ActionMode mode, Menu menu) {
            mode.getMenuInflater().inflate(R.menu.action_mode_menu, menu);
            editItem = menu.findItem(R.id.edit_item);
            return true;
        }

        @Override
        public boolean onPrepareActionMode(ActionMode mode, Menu menu) {
            return false;
        }

        @Override
        public boolean onActionItemClicked(ActionMode mode, MenuItem item) {
            SparseBooleanArray checkedItems;
            switch (item.getItemId()) {
                case R.id.delete_item: {

                    if (!displayGridView) {
                        checkedItems = listView.getCheckedItemPositions();
                    } else {
                        checkedItems = gridView.getCheckedItemPositions();
                    }
                    ListIterator<User> iterator = userList.listIterator();
                    int pos = 0;
                    while (iterator.hasNext()) {
                        User user = iterator.next();
                        if (checkedItems.get(pos)) {
                            iterator.remove();
                            userListInitial.remove(user);
                            listView.setItemChecked(pos, false);
                            gridView.setItemChecked(pos, false);
                        }
                        pos++;
                    }
                    adapter.notifyDataSetChanged();
                }
                break;
                case R.id.edit_item: {
                    if (!displayGridView) {
                        checkedItems = listView.getCheckedItemPositions();
                    } else {
                        checkedItems = gridView.getCheckedItemPositions();
                    }
                    User user = null;
                    for (int i = 0; i < userList.size(); i++) {
                        if (checkedItems.get(i)) {
                            user = userList.get(i);
                            break;
                        }

                    }
                    if (user != null) {
                        UUID id = user.getUuid();
                        Intent intent = new Intent(MainActivity.this, UserDetailActivity.class);
                        intent.putExtra(UserDetailActivity.EXTRA_DETAIL_USER, id);
                        startActivityForResult(intent, 101);
                    }
                }
                break;
            }

            return true;
        }

        @Override
        public void onDestroyActionMode(ActionMode mode) {
            fillLayout(userListInitial);

        }

    }
}
