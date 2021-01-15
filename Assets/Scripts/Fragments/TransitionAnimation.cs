using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class TransitionAnimation 
{
    public abstract float Duration  { get; }

    protected Transform transformTarget;

    public abstract void AnimateHide(Action onComplete);

    public abstract void AnimateShow(Action onComplete);


    public TransitionAnimation(Transform transformTarget)
    {
        this.transformTarget = transformTarget;
    }
}


public class ScaleTransitionAnimation : TransitionAnimation
{
    public const float DURATION = 0.5f;
    private CanvasGroup canvasGroup;
    private Tweener tweener;
    public ScaleTransitionAnimation(Transform transformTarget) : base(transformTarget)
    {
        canvasGroup = transformTarget.GetComponent<CanvasGroup>();
    }

    public override float Duration => DURATION;

    public override void AnimateHide(Action onComplete)
    {
#if UNITY_IOS
        if(tweener != null && tweener.IsPlaying())
        {
            tweener.OnComplete(() => onComplete?.Invoke());
            return;
        }
#endif
            
        canvasGroup.DOFade(0, Duration).SetEase(Ease.InExpo);
        tweener = transformTarget.DOScale(0.01f, Duration).SetEase(Ease.InExpo).OnComplete(() => {onComplete?.Invoke(); });

    }

    public override void AnimateShow(Action onComplete)
    {

        transformTarget.localScale = Vector2.one * 0.01f;
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1, Duration).SetEase(Ease.OutExpo);
        tweener = transformTarget.DOScale(1, Duration).SetEase(Ease.OutExpo).OnComplete(() => { onComplete?.Invoke(); });
    }
}
//}

//public class UpToDownTranslateAnimation : TransitionAnimation
//{
//    private float itemHeight;
//    private RectTransform rectT;

//    public UpToDownTranslateAnimation(Transform transformTarget) : base(transformTarget)
//    {
//        rectT = transformTarget as RectTransform;
//        rectT.pivot = new Vector2(0.5f, 0f);
        
//    }

//    public override float Duration => 0.3f;

//    public override void AnimateHide(Action onComplete)
//    {
//        rectT.DOAnchorPosY(0, Duration).OnComplete(() => onComplete?.Invoke());
//    }

//    public override void AnimateShow(Action onComplete)
//    {
//        itemHeight = rectT.rect.height * rectT.localScale.y;
//        rectT.anchoredPosition = Vector2.zero;
//        rectT.DOAnchorPosY(-itemHeight, Duration).OnComplete(() => onComplete?.Invoke());

//    }
//}