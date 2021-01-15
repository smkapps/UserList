using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AnimUtils 
{

    public static void AnimateTextValue(TextMeshProUGUI text, string initialString, int fromValue, int targetValue, float duration)
    {
        bool needFormatString = initialString.Contains("{0}");
        float tempValue;
        DOTween.To(x => {

            tempValue = (int)x;
            text.text = needFormatString ? string.Format(initialString, tempValue) : tempValue.ToString();
        }
        , fromValue, targetValue, duration).SetEase(Ease.Linear);

    }
}
