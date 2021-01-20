using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocationChanger : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private int mostRecentUnlockedLocationID => GameProgress.Instance.MostRecentUnlockedLocationID;
    private int visibleBulletsCount => mostRecentUnlockedLocationID + 1;
    private int currentPlayingLocationID;

    private Bullet[] bullets;
    public static event Action CurrentPlayLocationChanged;

    private void Awake()
    {
        currentPlayingLocationID = GameProgress.Instance.CurrentPlayLocationID;
        bullets = GetComponentsInChildren<Bullet>(true);
        DefineVisiability();
        UIWindowLocationUnlocked.UnlockedLocationApplied += OnNewLocationApplied;
        TimeProgress.NewLocationUnlocked += TimeProgress_NewLocationUnlocked;
    }

    private void ChangeLocationBySwipe(bool right)
    {
        currentPlayingLocationID += right ? 1 : -1;
        if (currentPlayingLocationID < 0) currentPlayingLocationID = mostRecentUnlockedLocationID;
        if (currentPlayingLocationID > mostRecentUnlockedLocationID) currentPlayingLocationID = 0;

        GameProgress.Instance.CurrentPlayLocationID = currentPlayingLocationID;
        ShowBulletOfActiveLocationAsOn();
        CurrentPlayLocationChanged?.Invoke();
    }

    private void TimeProgress_NewLocationUnlocked(int obj)
    {
        DefineVisiability();
    }

    private void OnNewLocationApplied()
    {
        currentPlayingLocationID = mostRecentUnlockedLocationID;
        DefineVisiability();
    }

    private void DefineVisiability()
    {
        if(visibleBulletsCount >= 2)
        {
            gameObject.SetActive(true);
            SetNBulletsVisible(visibleBulletsCount);
            ShowBulletOfActiveLocationAsOn();
        }
        else gameObject.SetActive(false);
    }

    private void SetNBulletsVisible(int count)
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].gameObject.SetActive(i < visibleBulletsCount);
        }
    }

    private void ShowBulletOfActiveLocationAsOn()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetIsOn(i == currentPlayingLocationID);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        draged = false;
    }


    bool draged;
    private float timeCount;
    private Vector2 deltaValue = Vector2.zero;

    public void OnBeginDrag(PointerEventData data)
    {
        deltaValue = Vector2.zero;
    }

   


    public void OnDrag(PointerEventData data)
    {
        if (draged) return;
        deltaValue += data.delta;
        if (data.dragging)
        {
            timeCount += Time.deltaTime;
            if (timeCount > 1.075f)
            {
                timeCount = 0.0f;
                deltaValue = Vector2.zero;
            }
        }
        float pixelsInOneUnit = Screen.height /27.32f;
        if (Mathf.Abs(deltaValue.x / pixelsInOneUnit) >= .5f)
        {
            ChangeLocationBySwipe(deltaValue.x < 0);
            timeCount = 0.0f;
            deltaValue = Vector2.zero;
            draged = true;
        }
            
    }

    public void OnEndDrag(PointerEventData data)
    {
        deltaValue = Vector2.zero;
    }

    //public void OnDrag(PointerEventData eventData)
    //{
    //    float pixelsInOneUnit = Screen.height / (Camera.main.orthographicSize *2);
    //    if (draged || Mathf.Abs(eventData.delta.x / pixelsInOneUnit) < 3) return;
    //    ChangeLocationBySwipe(eventData.delta.x < 0);
    //    draged = true;
    //}

}
