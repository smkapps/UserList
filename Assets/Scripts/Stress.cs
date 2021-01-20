using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stress : MonoSingleton<Stress>
{
    private int stressValue;
    private int stressMaxValue = 15;
    public int StressMaxValue => stressMaxValue;
    public int StressValue { get => stressValue;  }
    public const double SECONDS_TO_RECOVER = 90;
    public static event Action<int> StressChanged;

    private void Awake()
    {
        stressValue = GameProgress.Instance.CurrentStressLevel;
        if (!IsStillInStressByTime() && stressValue >= stressMaxValue) Recover();
        Hero.Stressed += OnStressed;
    }

    protected override void OnDestroy()
    {
        Hero.Stressed -= OnStressed;
    }

    private void Start()
    {
        if (IsStillInStressByTime()) UIControllerFacade.Instance.ShowUIWindow(UIFragmentName.StressPeak);

    }

    public bool IsStillInStressByTime()
    {
        double now = TimeConverter.DateTimeToDouble(DateTime.Now);
        return now - GameProgress.Instance.LastStressAtPeakTime <= SECONDS_TO_RECOVER;
    }

    public double GetSecondsToRecover()
    {
        double now = TimeConverter.DateTimeToDouble(DateTime.Now);
        return SECONDS_TO_RECOVER - (now - GameProgress.Instance.LastStressAtPeakTime);
    }

    public void Recover()
    {
        GameProgress.Instance.LastStressAtPeakTime = 0;
        SetStressValue(0);
    }

    private void OnStressed()
    {
        SetStressValue(stressValue + 1);
        if (stressValue >= stressMaxValue)
        {
            GameProgress.Instance.CountStressesAtPeakTime++;
            GameProgress.Instance.LastStressAtPeakTime = TimeConverter.DateTimeToDouble(DateTime.Now);
            UIControllerFacade.Instance.AddUIWindowToQueue(UIFragmentName.StressPeak);
        }           
    }

    private void SetStressValue(int value)
    {
        if (value < 0 || value > stressMaxValue) return;
        stressValue = value;
        GameProgress.Instance.CurrentStressLevel = stressValue;
        StressChanged?.Invoke(stressValue);
    }

}
