using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleDownOnNarrowScreen : MonoBehaviour
{
    private const float REFERENCE_SCREEN_HEIGHT = 2732;
    private const float MAX_WIDTH_THRESHOLD_FACTOR = 1f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        if (ElementWidth() > MaxElementWidthForCurrentScreen())
        {
            transform.localScale *=  (MaxElementWidthForCurrentScreen()  / ElementWidth());
        }
    }

    private float ElementWidth()
    {
        return rectTransform.rect.width;
    }

    private float MaxElementWidthForCurrentScreen() 
    {
        return  REFERENCE_SCREEN_HEIGHT* Camera.main.aspect * MAX_WIDTH_THRESHOLD_FACTOR;
    }

}
