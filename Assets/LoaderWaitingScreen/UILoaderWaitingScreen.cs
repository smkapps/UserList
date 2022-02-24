using System;
using System.Collections;
using UnityEngine;

public class UILoaderWaitingScreen : MonoBehaviour
{
    private const int DelayShowHideButton = 3;
    private const int DelayForceHide = 10;
    [SerializeField] private GameObject closeButton;
    private Action onHide;

    public void Show(Action onHide)
    {
        gameObject.SetActive(true);
        this.onHide = onHide;
        closeButton.SetActive(false);
        StartCoroutine(DoWithDelay(DelayShowHideButton, ShowCloseButton));
        StartCoroutine(DoWithDelay(DelayForceHide, Hide));
    }

    private IEnumerator DoWithDelay(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    public void ShowWithoutCloseBtn()
    {
        gameObject.SetActive(true);
        closeButton.SetActive(false);
    }

    public void Hide()
    {
        StopAllCoroutines();
        gameObject.SetActive(false);
        onHide?.Invoke();
    }

    private void ShowCloseButton()
    {
        closeButton.SetActive(true);
    }
    
}
