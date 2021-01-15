using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIControllerFacade : MonoSingleton<UIControllerFacade>
{

    [SerializeField] private UIScreenController uIScreenController;
    [SerializeField] private UIWindowController uIWindowController;
    [SerializeField] private GameObject touchLocker;
    [SerializeField] private UILoaderWaitingScreen loader;

    public bool AllFragmetsInitialised => uIScreenController.Initialised && uIWindowController.Initialised;
    public UIFragment CurrentScreen => uIScreenController.CurrentScreen;
    public UIFragment CurrentWindow => uIWindowController.CurrentWindow;

    //#if UNITY_EDITOR_WIN
    //    [MenuItem("Urmobi/Generate AddListeners")]
    //    public static void AddListeners()
    //    {
    //        Button[] buttons = UIControllerFacade.Instance.GetComponentsInChildren<Button>(true);
    //        foreach (var item in buttons)
    //        {
    //            item.gameObject.AddComponent<TapSoundButton>();
    //            EditorUtility.SetDirty(item);
    //        }
    //    }

    //#endif



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && touchLocker.activeSelf == false && loader.gameObject.activeSelf == false)
        {
            if (uIWindowController.OnBackClick()) // touchLocker is active only during transition between windows. So user must wait till animation is finished 
            {
                return;
            }
            uIScreenController.OnBackClick();
        }
    }


    public UIFragmentName CurrentFragmentName
    {
        get
        {
            if (CurrentWindow) return (UIFragmentName) CurrentWindow.ID;
            if (CurrentScreen) return (UIFragmentName)CurrentScreen.ID;
            return UIFragmentName.Lobby;
        }
    }

    public void ShowUIScreen(UIFragmentName screenName, object data = null)
    {
        uIScreenController.ShowUIFragment(screenName, data);
    }

    public void ShowUIWindow(UIFragmentName screenName, object data = null)
    {
        uIWindowController.ShowUIFragment(screenName, data);
    }

    public void AddUIWindowToQueue(UIFragmentName screenName, object data = null)
    {
        uIWindowController.AddWindowToQueue(screenName, data);
    }

    public void InsertUIWindowToQueueAsFirst(UIFragmentName screenName, object data = null)
    {
        uIWindowController.InsertWindowToQueueAsFirst(screenName, data);
    }

    public void ClearWindowsQueue()
    {
        uIWindowController.ClearQueue();
    }

    public void HideCurrentWindow()
    {
        uIWindowController.HideCurrentFragment();
    }

    public void SetTouchLockerActive(bool isActive)
    {
        touchLocker.SetActive(isActive);
    }

    public void ShowWaitingLoader(Action onPlayerClose = null)
    {
        loader.Show(onPlayerClose);
    }

    public void ShowWaitingLoaderWithoutCloseBtn()
    {
        loader.ShowWithoutCloseBtn();
    }

    public void HideWaitingLoader()
    {
        loader.Hide();
    }

}


[System.Serializable]
public enum UIFragmentName
{
    Game = 0,
    Lobby = 1,
    LevelComplete = 2,
    Hut = 3,
    Quest = 4,
    DailyReward = 5,
    Shop = 6,
    Settings = 7,
    HiddenWords =8,
    Error = 9,
    PurchaseSuccess =10,
    QuestNext = 11,
    Progress =12,
    RateUs = 13,
    NoMoreLevels = 14

}
