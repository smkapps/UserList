using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgress : MonoSingleton<GameProgress>
{
    private const string GAME_PROGRESS_KEY = "GameProgress";
    public static event Action ProgressChanged;
    
    [System.Serializable]
    public class Data
    {
        public bool NoAds;
        public bool MusicIsOn = true;
        public bool SoundIsOn = true;
        public double FirstGameEnterDeviceTime;

    }

    [SerializeField] private Data data = new Data();

    private void Awake()
    {
        LoadGameProgressFromStorage();
    }

    private DateTime FirstGameEnter => TimeConverter.SecondsToDateTime(data.FirstGameEnterDeviceTime);

    public bool NoAds
    {
        get { return data.NoAds; }
        set
        {
            data.NoAds = value;
            SaveGameProgressInStorage();
        }
    }

    public bool MusicIsOn
    {
        get { return data.MusicIsOn; }
        set { data.MusicIsOn = value; SaveGameProgressInStorage(); }
    }

    public bool SoundIsOn
    {
        get { return data.SoundIsOn; }
        set { data.SoundIsOn = value; SaveGameProgressInStorage(); }
    }

    public void LoadGameProgressFromStorage()
    {
        string json = PlayerPrefs.GetString(GAME_PROGRESS_KEY);
        JsonUtility.FromJsonOverwrite(json, data);
        if (string.IsNullOrEmpty(json))
        {
            data.FirstGameEnterDeviceTime = TimeConverter.DateTimeToDouble(DateTime.Now);
            SaveGameProgressInStorage();
        }
    }

    public void SaveGameProgressInStorage()
    {
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(GAME_PROGRESS_KEY, json);
        ProgressChanged?.Invoke();
    }
}
