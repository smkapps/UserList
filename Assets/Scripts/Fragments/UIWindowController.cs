using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIWindowController : AUIFragmentController
{
    [SerializeField] private Image darkerImage;
    private const float DARKER_ALPHA = 0.50f;

    [SerializeField] private UIFragment currentWindow;
    public UIFragment CurrentWindow => currentWindow;

    private LinkedList<System.Action> windowsQueue = new LinkedList<System.Action>();
    private TransitionAnimation layerDefaultAnimation;

    public static event Action WindowOpen;
    public static event Action WindowClosed;

    protected override void SetDefaultFragmentAnimation(UIFragment uIFragment)
    {
        layerDefaultAnimation = new ScaleTransitionAnimation(uIFragment.transform);
        uIFragment.TransitionAnimation = layerDefaultAnimation;
    }

    public void AddWindowToQueue(UIFragmentName uIFragmentName, object data)
    {
        if (currentWindow == null && isHiding == false) ShowUIFragment(uIFragmentName, data);

        else windowsQueue.AddLast(() => ShowUIFragment(uIFragmentName, data));
    }

    public void InsertWindowToQueueAsFirst(UIFragmentName uIFragmentName, object data)
    {
        if (currentWindow == null && isHiding == false) ShowUIFragment(uIFragmentName, data);

        else windowsQueue.AddFirst(() => ShowUIFragment(uIFragmentName, data));
    }

    protected override void ShowUIFragment(UIFragment uIFragment, object data)
    {
        if (TryChangeCurrentWindow(uIFragment, data)) return;
        UIControllerFacade.Instance.SetTouchLockerActive(true);
        darkerImage.color = Color.black.WithAlpha(0);
        darkerImage.gameObject.SetActive(true);
        darkerImage.DOFade(DARKER_ALPHA, uIFragment.TransitionAnimation == null ? 0 : layerDefaultAnimation.Duration);
        currentWindow = uIFragment;       
        uIFragment.Show(data, () => UIControllerFacade.Instance.SetTouchLockerActive(false));
        WindowOpen?.Invoke();
    }

    private bool TryChangeCurrentWindow(UIFragment uIFragment, object data)
    {
        if (currentWindow == null) return false;
        UIControllerFacade.Instance.SetTouchLockerActive(true);
        currentWindow?.Hide(() =>
        {
            currentWindow = uIFragment;
            uIFragment.Show(data, () => UIControllerFacade.Instance.SetTouchLockerActive(false));
        });
       
        return true;
    }


    public bool OnBackClick()
    {
        if (currentWindow)
        {
            currentWindow.OnBackClick();
            return true;
        }
        return false;
    }

    private bool isHiding;
    public void HideCurrentFragment()
    {
        if (currentWindow == null) return;
        if (windowsQueue.Count == 0)
        {          
            UIControllerFacade.Instance.SetTouchLockerActive(true);
            isHiding = true;
            currentWindow.Hide(() => UIControllerFacade.Instance.SetTouchLockerActive(false));            
            darkerImage.DOFade(0, currentWindow.TransitionAnimation == null ? 0 : layerDefaultAnimation.Duration).SetEase(Ease.InExpo).OnComplete(() => {
                darkerImage.gameObject.SetActive(false);
                isHiding = false;
                currentWindow = null;
                if (windowsQueue.Count > 0)
                {
                    windowsQueue.First.Value.Invoke();
                    windowsQueue.RemoveFirst();
                }
                   
            });
            WindowClosed?.Invoke();
            
        }
        else
        {
            windowsQueue.First.Value.Invoke();
            windowsQueue.RemoveFirst();
        }
    }

    public void ClearQueue()
    {
        windowsQueue.Clear();
    }

}
