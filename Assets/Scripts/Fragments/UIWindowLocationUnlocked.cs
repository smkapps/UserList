using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowLocationUnlocked : UIFragment
{
    public static event System.Action UnlockedLocationApplied;
    [SerializeField] private Image skin;
    
    protected override void OnStartShowing(object data)
    {
        base.OnStartShowing(data);
        skin.sprite = ContentProvider.Instance.GetCurrentCircleSkinWithShapesSprite();
    }

    public void OnApplyClick()
    {
        UnlockedLocationApplied?.Invoke();
        UIControllerFacade.Instance.HideCurrentWindow();
    }

    public override void OnBackClick()
    {
        UIControllerFacade.Instance.HideCurrentWindow();
    }
}
