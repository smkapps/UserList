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
        public double FirstGameEnterDeviceTime;
        public double LastStressAtPeakTime;
        public double CurrentStageTime;
        public double TotalTime;
        public int MostRecentUnlockedLocationID;
        public int CurrentPlayLocationID;
        public int CurrentStressLevel;
        public int CountStressesAtPeakTime;
    }
    [SerializeField] private Data data = new Data();

    private void Awake()
    {
        DateTime now = DateTime.Now;
        LoadGameProgressFromStorage();
    }

    private DateTime FirstGameEnter => TimeConverter.SecondsToDateTime(data.FirstGameEnterDeviceTime);

    public int DaysSinceFirstEnter()
    {
        return (DateTime.Now - FirstGameEnter).Days;
    }

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

    public double LastStressAtPeakTime
    {
        get { return data.LastStressAtPeakTime; }
        set { data.LastStressAtPeakTime = value;  SaveGameProgressInStorage(); }
    }

    public double CurrentStageTime
    {
        get { return data.CurrentStageTime; }
        set { data.CurrentStageTime = value; }
    }

    public double TotalTime
    {
        get { return data.TotalTime; }
        set { data.TotalTime = value; }
    }

    public int MostRecentUnlockedLocationID
    {
        get { return data.MostRecentUnlockedLocationID; }
        set { data.MostRecentUnlockedLocationID = value; SaveGameProgressInStorage(); }
    }

    public int CurrentPlayLocationID
    {
        get { return data.CurrentPlayLocationID; }
        set { data.CurrentPlayLocationID = value; SaveGameProgressInStorage(); }
    }

    public int CurrentStressLevel
    {
        get { return data.CurrentStressLevel; }
        set { data.CurrentStressLevel = value; SaveGameProgressInStorage(); }
    }

    public int CountStressesAtPeakTime
    {
        get { return data.CountStressesAtPeakTime; }
        set { data.CountStressesAtPeakTime = value;  }
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
