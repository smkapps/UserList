using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeProgress : MonoBehaviour
{
    [SerializeField] private GameObject locationsConfigGO;
    [SerializeField] private IUnlockLocationConfig locationsConfig;
    [SerializeField] private int mostRecentUnlockedLocationID;
    
    private bool paused;
    private double currentStageTime;
    public float TotalTime { get; private set; }

    public static event System.Action<int> NewLocationUnlocked;

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
        locationsConfig = locationsConfigGO.GetComponent<IUnlockLocationConfig>();
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
    
    public bool AllLocationsUnlocked => mostRecentUnlockedLocationID >= locationsConfig.LastLocationID;

    public int MostRecentUnlockedLocationID { get => mostRecentUnlockedLocationID; }

    private void Update()
    {
        if (paused) return;
        TotalTime += Time.deltaTime;
        if (AllLocationsUnlocked) return;
        currentStageTime += Time.deltaTime;
        if(currentStageTime >= CurrentStageTargetTime) UnlockNewLocation();
    }

    private void UnlockNewLocation()
    {
        mostRecentUnlockedLocationID++;
        currentStageTime = 0;
        NewLocationUnlocked?.Invoke(mostRecentUnlockedLocationID);

    }
}

public interface IUnlockLocationConfig
{
    TimeSpan GetTimeSpanToUnlockNextLocation(int id);
    int LastLocationID { get; }
}