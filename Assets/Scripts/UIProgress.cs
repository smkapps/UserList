using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIProgress : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgrpound;
    [SerializeField] private Transform totalTimeHolder;
    [SerializeField] private TextMeshProUGUI totalTimeText;
    [SerializeField] private TextMeshProUGUI currentStageTimeText;
    [SerializeField] private Image nextLocationCircleSkin;

    private TimeProgress timeProgress;

    private void Awake()
    {
        timeProgress = FindObjectOfType<TimeProgress>();
        TimeProgress.NewLocationUnlocked += OnNewLocationUnlocked;
        SetNextLocationSkin();
        SetLocationBG();
    }

    private void OnDestroy()
    {
        TimeProgress.NewLocationUnlocked -= OnNewLocationUnlocked;
    }

    private void OnNewLocationUnlocked(int obj)
    {
        SetNextLocationSkin();
        SetLocationBG();
    }

    private void LateUpdate()
    {
        UpdateTotalTimeText();
        if (timeProgress.AllLocationsUnlocked) return;
        UpdateCurrentStageTimeText();

    }

    private void SetNextLocationSkin()
    {
        if (timeProgress.AllLocationsUnlocked)
        {
            totalTimeHolder.localPosition = totalTimeHolder.localPosition.WithX(0);
            nextLocationCircleSkin.gameObject.SetActive(false);
            return;
        }
            
        nextLocationCircleSkin.sprite = ContentProvider.Instance.GetNextCircleSkinSprite();
    }

    private void SetLocationBG()
    {
        backgrpound.sprite = ContentProvider.Instance.GetCurrentPlayBGSprite();
    }

    private void UpdateTotalTimeText()
    {
        string timeValue = TimePattern(TimeSpan.FromSeconds(timeProgress.TotalTime)); 
        if (totalTimeText.text.Equals(timeValue) == false) totalTimeText.text = timeValue;
    }

    private void UpdateCurrentStageTimeText()
    {
        string timeValue = TimePattern(TimeSpan.FromSeconds(timeProgress.LeftTimeToUnlockNextStage));
        if (currentStageTimeText.text.Equals(timeValue) == false) currentStageTimeText.text = timeValue;
    }

    //private void UpdateCurrentStageImageProgress()
    //{
    //    float progress = (float) (timeProgress.CurrentStageTime / timeProgress.CurrentStageTargetTime);
    //    currentStageProgressImage.fillAmount = progress;
    //}

    private string TimePattern(TimeSpan timespan)
    {
        if (timespan.Hours > 0) return string.Format(@"{0:hh\:mm\:ss}", timespan);
        if (timespan.Minutes > 0) return string.Format(@"{0:mm\:ss}", timespan);
        return string.Format(@"{0:ss}", timespan);
    }

    //private string SecondsToHoursAndMinutes(float seconds)
    //{
    //    int hours = TimeSpan.FromSeconds(seconds).Hours;
    //    int minutes = TimeSpan.FromSeconds(seconds).Minutes;
    //    return string.Format("{0}h {1}min", hours, minutes);
    //}

    //private string SecondsToMinutesAndSeconds(float seconds)
    //{

    //    int minutes = (int)TimeSpan.FromSeconds(seconds).TotalMinutes;
    //    int sec = TimeSpan.FromSeconds(seconds).Seconds;
    //    return string.Format("{0}min {1}sec", minutes, sec);
    //}
}
