using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public int stressValue;
    public int stressMaxValue = 10;

    public static event Action<StressInfo> Stressed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        stressValue++;
        collision.gameObject.SetActive(false);
        Stressed?.Invoke(new StressInfo(stressValue, 10));
    }
}


public struct StressInfo
{
    public int currentValue;
    public int maxValue;

    public StressInfo(int currentValue, int maxValue)
    {
        this.currentValue = currentValue;
        this.maxValue = maxValue;
    }
}