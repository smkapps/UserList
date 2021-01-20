using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeProgress : MonoBehaviour
{
    [SerializeField] private GameObject locationsConfigGO;
    [SerializeField] private IUnlockLocationConfig locationsConfig;
    [SerializeField] private int mostRecentUnlockedLocationID;

    public bool AllLocationsUnlocked => mostRecentUnlockedLocationID >= locationsConfig.LastLocationID;
    public int MostRecentUnlockedLocationID { get => mostRecentUnlockedLocationID; }

    private bool paused => UIControllerFacade.Instance.AnyWindowOpen;
    private double currentStageTime;
    public double TotalTime { get; private set; }

    public static event Action<int> NewLocationUnlocked;

    private void OnValidate()
    {
        if (locationsConfigGO.GetComponent<IUnlockLocationConfig>() == null)
        {
            locationsConfigGO = null;
            Debug.LogError("No Config");
        }           
    }

    private void Awake()
    {
        InitializeSavedState();
        locationsConfig = locationsConfigGO.GetComponent<IUnlockLocationConfig>();
    }

    private void InitializeSavedState()
    {
        mostRecentUnlockedLocationID = GameProgress.Instance.MostRecentUnlockedLocationID;
        currentStageTime = GameProgress.Instance.CurrentStageTime;
        TotalTime = GameProgress.Instance.TotalTime;
    }

    public double CurrentStageTime
    {
        get
        {
            if (AllLocationsUnlocked) throw new System.Exception("Current stage doesn't have target time");
            return currentStageTime;
        }
    }

    public double LeftTimeToUnlockNextStage
    {
        get
        {
            if (AllLocationsUnlocked) throw new System.Exception("Current stage doesn't have target time");
            return CurrentStageTargetTime - currentStageTime;
        }
    }

    public double CurrentStageTargetTime
    {
        get
        {
            if (AllLocationsUnlocked) throw new System.Exception("Current stage doesn't have target time");
            return locationsConfig.GetTimeSpanToUnlockNextLocation(mostRecentUnlockedLocationID).TotalSeconds;
        }
    }   

    private void Update()
    {
        if (paused) return;
        TotalTime += Time.deltaTime;
        SaveStateIfTimeIntervalReached();
        if (AllLocationsUnlocked) return;
        currentStageTime += Time.deltaTime;
        if(currentStageTime >= CurrentStageTargetTime) UnlockNewLocation();
    }

    private float lastSavedTimeSinceGameStart;

    private void SaveStateIfTimeIntervalReached()
    {
        if(Time.time - lastSavedTimeSinceGameStart > 5)
        {
            SaveState();
            lastSavedTimeSinceGameStart = Time.time;
        }
    }

    private void UnlockNewLocation()
    {
        mostRecentUnlockedLocationID++;
        currentStageTime = 0;
        SaveState();
        NewLocationUnlocked?.Invoke(mostRecentUnlockedLocationID);
        UIControllerFacade.Instance.AddUIWindowToQueue(UIFragmentName.UnlockLocation);
       
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus) SaveState();
    }

    private void SaveState()
    {
        GameProgress.Instance.TotalTime = TotalTime;
        GameProgress.Instance.CurrentStageTime = currentStageTime;
        GameProgress.Instance.MostRecentUnlockedLocationID = mostRecentUnlockedLocationID;
    }
}

public interface IUnlockLocationConfig
{
    TimeSpan GetTimeSpanToUnlockNextLocation(int id);
    int LastLocationID { get; }
}