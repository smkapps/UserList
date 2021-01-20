using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class RewardedAdShowing : MonoBehaviour
{
    [SerializeField] private string androidID;
    [SerializeField] private string iosID;
    private const float REPEAT_REQUEST_INTERVAL = 20;
    private string rewardedId;
    private RewardedAd rewardedAd;
    public bool RewardedVideoReady => rewardedAd != null && rewardedAd.IsLoaded();

    

    private Coroutine coroutineDelayRequest;
    public static RewardedAdShowing Instance;
    private Action rewardForWatching;
    private int adsStartedCount;
    public int AdsStartedCount { get => adsStartedCount; }
    public static event Action AdOpen;
    public static event Action AdWatched;

    private void Awake()
    {
        Instance = this;
        DefinePlatformSpecificAdUnitID();
        AdsInitializer.Initialized += OnAdsInitialized;
    }

    private void OnDestroy()
    {
        AdsInitializer.Initialized -= OnAdsInitialized;
        if (rewardedAd == null) return;
        rewardedAd.OnAdLoaded -= HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
        rewardedAd.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
        rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
    }

    private void OnAdsInitialized()
    {
        StartCoroutine(CreateAdOnMobileAdsInitializedWithDelay());
    }

    private IEnumerator CreateAdOnMobileAdsInitializedWithDelay()
    {

        yield return new WaitForSeconds(1);
        CreateAndLoadRewardedAd();
    }

    private void DefinePlatformSpecificAdUnitID()
    {
        #if UNITY_ANDROID
        rewardedId = androidID;
        #elif UNITY_IPHONE
        rewardedId = iosID;
        #else
        rewardedId = "unexpected_platform";
        #endif
    }

    public void CreateAndLoadRewardedAd()
    {
   
        UnregisterRewardedEvents();
        rewardedAd = new RewardedAd(rewardedId);
        RegisterRewadedEvents();
        AdRequest request = GetAdRequest();        
        rewardedAd.LoadAd(request);
    }

    private void RegisterRewadedEvents()
    {
        rewardedAd.OnAdOpening += HandleRewardedAdOpening;
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        rewardedAd.OnAdClosed += HandleRewardedAdClosed;
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
    }

    private void UnregisterRewardedEvents()
    {
        if (rewardedAd == null) return;
        rewardedAd.OnAdOpening -= HandleRewardedAdOpening;
        rewardedAd.OnAdLoaded -= HandleRewardedAdLoaded;
        rewardedAd.OnUserEarnedReward -= HandleUserEarnedReward;
        rewardedAd.OnAdClosed -= HandleRewardedAdClosed;
        rewardedAd.OnAdFailedToLoad -= HandleRewardedAdFailedToLoad;
    }

   
    public void ShowRewardedAd(Action onRewardedWatched)
    {
        this.rewardForWatching = onRewardedWatched;
        if (RewardedVideoReady)
        {
            rewardedAd.Show();
        }
        else
        {
            Debug.Log("Not ready");
        }
    }

    private void HandleRewardedAdOpening(object sender, EventArgs e)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            adsStartedCount++;
            AdOpen?.Invoke();
        });
  
    }

    private void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {

        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
         
            StopDelayRequestCoroutine();
            StartCoroutine(DelayRequest());
        });
    }

    private void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            Debug.Log("HandleRewardedAdLoaded");
            StopDelayRequestCoroutine();
        });
    }

    private void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            CreateAndLoadRewardedAd();
        });
    }

    private void HandleUserEarnedReward(object sender, Reward args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            rewardForWatching?.Invoke();
            AdWatched?.Invoke();
        });
    }

    
    private void StopDelayRequestCoroutine()
    {
        if (coroutineDelayRequest != null) StopCoroutine(coroutineDelayRequest);
    }

    private IEnumerator DelayRequest()
    {
        yield return new WaitForSeconds(REPEAT_REQUEST_INTERVAL);
        CreateAndLoadRewardedAd();
    }


    private AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
}
