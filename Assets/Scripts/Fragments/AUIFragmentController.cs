using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AUIFragmentController : MonoBehaviour
{
    [SerializeField] protected List<UIFragment> uIFragments;
    protected abstract void SetDefaultFragmentAnimation(UIFragment uIFragment);
    protected abstract void ShowUIFragment(UIFragment uIFragment, object data);

    //public void OpenWindow<T>(string screenId, T properties) where T : IWindowProperties
    //{
    //    windowLayer.ShowScreenById<T>(screenId, properties);
    //}


    public bool Initialised { get; private set; }

    private void Awake()
    {
        for (int i = 0; i < uIFragments.Count; i++)
        {
            uIFragments[i].Init();
            SetDefaultFragmentAnimation(uIFragments[i]);
        }
        Initialised = true;
    }

    
    public void ShowUIFragment(UIFragmentName name, object data = null)
    {
        UIFragment fragmentToShow = GetUIFragmentByName(name);
       
        if (fragmentToShow != null) ShowUIFragment(fragmentToShow, data);
    }


    private UIFragment GetUIFragmentByName(UIFragmentName name)
    {
        for (int i = 0; i < uIFragments.Count; i++)
        {
            if (uIFragments[i].ID == (int)name) return uIFragments[i];
        }

        return null;
    }
}
