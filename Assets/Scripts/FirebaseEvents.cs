using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseEvents : MonoBehaviour
{
    private static readonly string TESTER_EVENT = "MED_TESTER";
    private static readonly string HIT_EVENT = "MED_touch";
    private static readonly string UNLOCK_LOCATION_EVENT = "MED_new";
    private static readonly string START_AD_EVENT = "MED_start_ad";
    private static readonly string WATCHED_AD_EVENT = "MED_end_ad";

    private static readonly string DAYS_PARAMETER = "MED_days";
    private static readonly string LOCATION_ID_PARAMETER = "MED_location";
    private static readonly string MAX_STRESSES_COUNT = "MED_stress_count";
    private static readonly string MAX_TIME_PARAMETER = "MED_time_max";
    private static readonly string TIME_PARAMETER = "MED_time";
    private static readonly string ADS_STARTED_COUNT_PARAMETER = "MED_ad";


    private TimeProgress timeProgress;
    public static event Action FirebaseInitSuccess;
    private bool initialized;
    private bool errorInitialized;

    private void Awake()
    {
        timeProgress = FindObjectOfType<TimeProgress>();
    }

    private void Start()
    {
        Initialize();
        SubscribeGameEvents();
    }

    private void Initialize()
    {

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available) InitialiseFirebase();
            else errorInitialized = true;
        });
    }

    private void InitialiseFirebase()
    {
        FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
        FirebaseInitSuccess?.Invoke();
        initialized = true;
    }

    private void SubscribeGameEvents()
    {
        Hero.Stressed += Hero_Stressed;
        TimeProgress.NewLocationUnlocked += TimeProgress_NewLocationUnlocked;
        RewardedAdShowing.AdOpen += RewardedAdShowing_AdOpen;
        RewardedAdShowing.AdWatched += RewardedAdShowing_AdWatched;
    }
    private void OnDestroy()
    {

        Hero.Stressed -= Hero_Stressed;
        TimeProgress.NewLocationUnlocked -= TimeProgress_NewLocationUnlocked;
        RewardedAdShowing.AdOpen -= RewardedAdShowing_AdOpen;
        RewardedAdShowing.AdWatched -= RewardedAdShowing_AdWatched;
    }


    private void Hero_Stressed()
    {
        Debug.Log("ReportEvent " + HIT_EVENT);
        ReportEvent(HIT_EVENT, LocationCurrentPlayIDParameter(), MaxTimeParameter(), DaysParameter(), TimeParameter());
    }

    private void TimeProgress_NewLocationUnlocked(int obj)
    {
        Debug.Log("ReportEvent " + UNLOCK_LOCATION_EVENT);
        ReportEvent(UNLOCK_LOCATION_EVENT, LocationMostRecentIDParameter(), MaxTimeParameter(), DaysParameter(), TimeParameter(), MaxStressCountParameter());
    }

    private void RewardedAdShowing_AdOpen()
    {
        Debug.Log("ReportEvent " + START_AD_EVENT);
        ReportEvent(START_AD_EVENT, MaxTimeParameter(), DaysParameter(), TimeParameter(), MaxStressCountParameter());
    }

    private void RewardedAdShowing_AdWatched()
    {
        Debug.Log("ReportEvent " + WATCHED_AD_EVENT);
        ReportEvent(WATCHED_AD_EVENT, MaxTimeParameter(), DaysParameter(), TimeParameter(), MaxStressCountParameter());
    }

    private Parameter DaysParameter()
    {
        Debug.Log("DaysParameter " + GameProgress.Instance.DaysSinceFirstEnter());
        return new Parameter(DAYS_PARAMETER, GameProgress.Instance.DaysSinceFirstEnter().ToString());
    }

    private Parameter TimeParameter()
    {
        Debug.Log("TimeParameter " + ((int)TimeSpan.FromSeconds(Time.time).TotalMinutes).ToString());
        return new Parameter(TIME_PARAMETER, ((int)TimeSpan.FromSeconds(Time.time).TotalMinutes).ToString());
    }

    private Parameter MaxTimeParameter()
    {
        Debug.Log("MaxTimeParameter " + ((int)TimeSpan.FromSeconds(timeProgress.TotalTime).TotalMinutes).ToString());
        return new Parameter(MAX_TIME_PARAMETER, ((int)TimeSpan.FromSeconds(timeProgress.TotalTime).TotalMinutes).ToString());
    }

    private Parameter AdsCountParameter()
    {
        Debug.Log("AdsCountParameter " + RewardedAdShowing.Instance.AdsStartedCount);
        return new Parameter(ADS_STARTED_COUNT_PARAMETER, RewardedAdShowing.Instance.AdsStartedCount.ToString());
    }

    private Parameter LocationMostRecentIDParameter()
    {
        Debug.Log("LocationMostRecentIDParameter " + GameProgress.Instance.MostRecentUnlockedLocationID);
        return new Parameter(LOCATION_ID_PARAMETER, GameProgress.Instance.MostRecentUnlockedLocationID.ToString());
    }

    private Parameter LocationCurrentPlayIDParameter()
    {
        Debug.Log("LocationCurrentPlayIDParameter " + GameProgress.Instance.CurrentPlayLocationID);
        return new Parameter(LOCATION_ID_PARAMETER, GameProgress.Instance.CurrentPlayLocationID.ToString());
    }

    private Parameter MaxStressCountParameter()
    {
        Debug.Log("MaxStressCountParameter " + GameProgress.Instance.CountStressesAtPeakTime);
        return new Parameter(MAX_STRESSES_COUNT, GameProgress.Instance.CountStressesAtPeakTime.ToString());
    }

    private void ReportEvent(string eventName, params Parameter[] parameters)
    {
        if (errorInitialized) return;
        if (initialized == false)
        {
            StartCoroutine(WaitInitializedAndReport());
        }
        else Report();

        IEnumerator WaitInitializedAndReport()
        {
            yield return new WaitUntil(() => initialized);
            Report();
        }

        void Report()
        {
            if (TestDeviceChecker.IsTester)
            {
                FirebaseAnalytics.LogEvent(TESTER_EVENT);
                return;
            }

            FirebaseAnalytics.LogEvent(eventName, parameters);
        }
    }

}
