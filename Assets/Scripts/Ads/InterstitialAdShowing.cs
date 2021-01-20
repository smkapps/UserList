using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
using System;
using System.Collections;
using UnityEngine;

public class InterstitialAdShowing : MonoBehaviour
{
    [SerializeField] private string androidID;
    [SerializeField] private string iosID;
    private const int REPEAT_REQUEST_INTERVAL = 20;
    public static InterstitialAdShowing Instance;
    private InterstitialAd interstitialAd;
    private Coroutine coroutineDelayRequest;
    private string interstitialId;
  
    private void Awake()
    {
        Instance = this;
        DefinePlatformSpecificAdUnitID();
        AdsInitializer.Initialized += OnAdsInitialized;
    }

    private void OnDestroy()
    {
        AdsInitializer.Initialized -= OnAdsInitialized;
        DestroyOldInterstitialIfExists();
    }

    private void OnAdsInitialized()
    {
        StartCoroutine(CreateAdOnMobileAdsInitializedWithDelay());
    }

    private IEnumerator CreateAdOnMobileAdsInitializedWithDelay()
    {
        yield return new WaitForSeconds(3);
        RequestInterstitial();
    }

    public void Show()
    {
        if (interstitialAd != null && interstitialAd.IsLoaded())
        {      
            interstitialAd.Show();
        }
    }

    private void DefinePlatformSpecificAdUnitID()
    {
        #if UNITY_ANDROID
        interstitialId = androidID;
#elif UNITY_IPHONE
        interstitialId = iosID;
#else
        Debug.Log("unexpected_platformunexpected_platformunexpected_platformunexpected_platform");
        interstitialId = "unexpected_platform";
#endif
    }

    private void RequestInterstitial()
    {
        DestroyOldInterstitialIfExists();
        CreateNewInterstitial();
        AdRequest request = GetAdRequest();
        interstitialAd.LoadAd(request);

    }

    private void DestroyOldInterstitialIfExists()
    {
        if (interstitialAd == null) return;
        interstitialAd.OnAdLoaded -= HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad -= HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening -= HandleOnAdOpened;
        interstitialAd.OnAdClosed -= HandleOnAdClosed;
        interstitialAd.Destroy();
    }

    private void CreateNewInterstitial()
    {
        interstitialAd = new InterstitialAd(interstitialId);
        interstitialAd.OnAdLoaded += HandleOnAdLoaded;
        interstitialAd.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        interstitialAd.OnAdOpening += HandleOnAdOpened;
        interstitialAd.OnAdClosed += HandleOnAdClosed;
    }

    private void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            StopDelayRequestCoroutine();
        });
    }

    private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            StopDelayRequestCoroutine();
            StartCoroutine(DelayRequestInterstitial());
        });
    }

    private void HandleOnAdOpened(object sender, EventArgs args)
    {

    }

    private void HandleOnAdClosed(object sender, EventArgs args)
    {
        MobileAdsEventExecutor.ExecuteInUpdate(() =>
        {
            RequestInterstitial();
        });
    }

    
    private void StopDelayRequestCoroutine()
    {
        if (coroutineDelayRequest != null) StopCoroutine(coroutineDelayRequest);
    }

    private IEnumerator DelayRequestInterstitial()
    {
        yield return new WaitForSeconds(REPEAT_REQUEST_INTERVAL);
        RequestInterstitial();
    }

    private AdRequest GetAdRequest()
    {
        return new AdRequest.Builder().Build();
    }
}
