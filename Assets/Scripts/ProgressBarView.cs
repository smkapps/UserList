using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ProgressBarView : MonoBehaviour
{
    //[SerializeField] private float fillTime = 1;
    [SerializeField] private Image activeProgressImage;
    [SerializeField] private TextMeshProUGUI progressText;

    public void SetInitialValue(float startValue, float target)
    {
        if (progressText != null) progressText.text = string.Format("{0}/{1}", startValue, target);
        activeProgressImage.fillAmount = (startValue / target);
    }

    public void AnimateFill(int startValue, int endValue, int target, float duration)
    {
        activeProgressImage.DOFillAmount((float) endValue / target, duration);
        int value = startValue;
        DOTween.To(() =>   value, x =>
        {
            value = x;
            if (progressText != null) progressText.text = string.Format("{0}/{1}",  x, target);
        }

       , endValue, duration).SetEase(Ease.OutSine);
    }

    public void AnimateFill(int endValue, int target, float duration)
    {
        activeProgressImage.DOFillAmount((float) endValue / target, duration);
    }


}
