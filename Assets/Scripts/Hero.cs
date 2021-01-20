using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public static event Action Stressed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
        animator?.SetTrigger("off");
        Stressed?.Invoke();

    }

    //private void GetStressHit()
    //{
    //    stressValue++;       
    //    Stressed?.Invoke(new StressInfo(stressValue, stressMaxValue));
       
    //    if (stressValue == stressMaxValue) UIControllerFacade.Instance.ShowUIWindow(UIFragmentName.StressPeak);
    //}
}

