using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowTutorial : UIFragment
{
    public override void OnBackClick()
    {
        UIControllerFacade.Instance.HideCurrentWindow();
    }
}
