using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIProgress : MonoBehaviour
{
 
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
    }

    private void OnDestroy()
    {
        TimeProgress.NewLocationUnlocked -= OnNewLocationUnlocked;
    }

    private void OnNewLocationUnlocked(int locationID)
    {
        SetNextLocationSkin();
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

    private string TimePattern(TimeSpan timespan)
    {
        if ((int)timespan.TotalHours > 0) return (int)timespan.TotalHours + timespan.ToString(@"\:mm\:ss"); 
        if (timespan.Minutes > 0) return string.Format(@"{0:mm\:ss}", timespan);
        return string.Format(@"{0:ss}", timespan);
    }
}

