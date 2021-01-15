using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoSingleton<GameState>
{
    [SerializeField] private GameObject testerMarker;
    private bool paused;
    public static event Action FixedUpdated;

    private void Awake()
    {
        bool isTester = false;
        TestDeviceChecker.CheckIfTesterFileExists(out isTester);
        testerMarker.SetActive(isTester);
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
    }

    private void FixedUpdate()
    {
        //if (paused) return;
        FixedUpdated?.Invoke();
    }


    //private void OnApplicationFocus(bool focus)
    //{
    //    if (focus == false) paused = true;
    //    else Invoke(nameof(DisablePaused), 0.1f);
    //}

    //private void DisablePaused()
    //{
    //    paused = false;
    //}
}
