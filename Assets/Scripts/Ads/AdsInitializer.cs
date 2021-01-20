using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsInitializer : MonoBehaviour
{
    public static event Action Initialized;



    //private void Awake()
    //{
    //    FirebaseEvents.FirebaseInitSuccess += OnFirebaseInitSuccess;
    //}

    //private void OnDestroy()
    //{
    //    FirebaseEvents.FirebaseInitSuccess -= OnFirebaseInitSuccess;
    //}



    private void Start()
    {
        StartCoroutine(InitializeAds());
    }

    private IEnumerator InitializeAds()
    {
        Debug.Log("InitializeAds");
        //yield return new WaitUntil(() => purchaseInitialized);
        yield return new WaitForSeconds(.2f);
        MobileAds.SetiOSAppPauseOnBackground(true);
        AddTestDevices();
        MobileAds.Initialize(OnInitialized);
    }

    private void AddTestDevices()
    {
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(new List<string>(TestDeviceChecker.AllTestDevices))
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
    }

    private void OnInitialized(InitializationStatus obj)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Initialized?.Invoke();
        });
    }

}
