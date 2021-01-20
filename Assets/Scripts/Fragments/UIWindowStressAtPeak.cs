using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIWindowStressAtPeak : UIFragment
{
    private Stress stress => Stress.Instance;
    private double secondsToRecoverLeft;
    [SerializeField] private Image activeProgressImage;
    
    [SerializeField] private TextMeshProUGUI timeLeftText;
    [SerializeField] private GameObject rewardedButton;
    [SerializeField] private GameObject recoverButton;

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            DefineLeftTimeOnWindowOpen();
        }
    }

    private void Update()
    {
        if (secondsToRecoverLeft <= 0) return;
        secondsToRecoverLeft -= Time.deltaTime;
        UpdateProgressbar();
        UpdateTimeText();
        if (secondsToRecoverLeft <= 0) OnTimeOut();
    }

    public void OnRewardedAdClick()
    {
        if(RewardedAdShowing.Instance.RewardedVideoReady) RewardedAdShowing.Instance.ShowRewardedAd(() => ForceRecover());

        else
        {
            StartCoroutine(WaitAdLoadedAndShowAd());
            UIControllerFacade.Instance.ShowWaitingLoader(() => OnPlayerStopWaitingAd());
        }

        IEnumerator WaitAdLoadedAndShowAd()
        {
            yield return new WaitUntil(() => RewardedAdShowing.Instance.RewardedVideoReady);
            yield return new WaitForSeconds(0.5f);
            RewardedAdShowing.Instance.ShowRewardedAd(() => ForceRecover());
            UIControllerFacade.Instance.HideWaitingLoader();            
        }

        void OnPlayerStopWaitingAd()
        {
            StopAllCoroutines();
        }
    }

    protected override void OnStartShowing(object data)
    {
        base.OnStartShowing(data);
        DefineLeftTimeOnWindowOpen();
    }

    private void DefineLeftTimeOnWindowOpen()
    {
        secondsToRecoverLeft = stress.GetSecondsToRecover();
        SwitchButtons();
        if (secondsToRecoverLeft <= 0) OnTimeOut();
    }


    private void UpdateTimeText()
    {
        if (secondsToRecoverLeft < 0) secondsToRecoverLeft = 0;
        string timeValue = TimeSpan.FromSeconds(secondsToRecoverLeft).TimeTextWithMinutes();
        if (timeLeftText.text.Equals(timeValue) == false) timeLeftText.text = timeValue;
    }

    private void UpdateProgressbar()
    {
        activeProgressImage.fillAmount = (float)(secondsToRecoverLeft / Stress.SECONDS_TO_RECOVER);
    }

    private void OnTimeOut()
    {
        secondsToRecoverLeft = 0;
        UpdateTimeText();
        UpdateProgressbar();
        stress.Recover();
        SwitchButtons();
    }

    private void ForceRecover()
    {
        secondsToRecoverLeft = 0;
        stress.Recover();
        UIControllerFacade.Instance.HideCurrentWindow();
    }

    private void SwitchButtons()
    {
        bool inStress = secondsToRecoverLeft > 0;
        rewardedButton.SetActive(inStress);
        recoverButton.SetActive(!inStress);
    }
}
