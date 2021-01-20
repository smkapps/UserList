using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField] private GameObject testerMarker;

    private void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        bool isTester = false;
        TestDeviceChecker.CheckIfTesterFileExists(out isTester);
        testerMarker.SetActive(isTester);
        Application.targetFrameRate = 60;
        Input.multiTouchEnabled = false;
        CheckIfFirstSessionAndShowTutorial();
#if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
#endif
    }

    private void CheckIfFirstSessionAndShowTutorial()
    {
        if(GameProgress.Instance.TotalTime == 0)
        {
            UIControllerFacade.Instance.ShowUIWindow(UIFragmentName.Tutorial);
        }
    }

}
