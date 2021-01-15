using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class UIFragment : MonoBehaviour
{

    [SerializeField] protected UIFragmentName screenName;
    public int ID => (int)screenName;
    public TransitionAnimation TransitionAnimation {get; set; }

    private Canvas canvas;

    public virtual void Init()
    {
        canvas = GetComponent<Canvas>();
        transform.localPosition = Vector2.zero;
    }

    public virtual void Show(object data, Action onCompleteOnClient = null) 
    {
  
        transform.SetAsLastSibling();
        SetIsActive(true);
        OnStartShowing(data);
        if (TransitionAnimation == null) OnFinish();
        else TransitionAnimation.AnimateShow(OnFinish);

        void OnFinish()
        {
            
            onCompleteOnClient?.Invoke();
            OnShown();
        }
    }

    public virtual void Hide(Action onCompleteOnClient = null)
    {
  
        if (TransitionAnimation == null) OnFinish();         
        else TransitionAnimation.AnimateHide(OnFinish);

        void OnFinish()
        {
            OnHidden();
            onCompleteOnClient?.Invoke();
        }
    }

    protected void SetIsActive(bool isOn)
    {
        if (canvas == null)
            gameObject.SetActive(isOn);
        else canvas.enabled = isOn;

    }

    protected virtual void OnHidden()
    {
        SetIsActive(false);
        //UIControllerFacade.Instance.SetTouchLockerActive(false);
    }

    protected virtual void OnStartShowing(object data)
    {

    }

    protected virtual void OnShown()
    {
        //UIControllerFacade.Instance.SetTouchLockerActive(false);
    }

    public virtual void OnBackClick()
    {
       
    }


}





