using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenController : AUIFragmentController
{  
    private UIFragment currentScreen;

    public UIFragment CurrentScreen => currentScreen;

    protected override void SetDefaultFragmentAnimation(UIFragment uIFragment)
    {
        uIFragment.TransitionAnimation = null;
    }

    protected override void ShowUIFragment(UIFragment uIFragment, object data)
    {
        if (currentScreen != null) currentScreen.Hide();
        currentScreen = uIFragment;
        currentScreen.Show(data);
    }

    public void OnBackClick()
    {
        currentScreen?.OnBackClick();
    }
}

