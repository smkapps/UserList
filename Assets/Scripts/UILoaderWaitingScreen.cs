using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoaderWaitingScreen : MonoBehaviour
{
    [SerializeField] private GameObject closeButton;
    private System.Action onHide;


    public void Show(System.Action onHide)
    {
        gameObject.SetActive(true);
        this.onHide = onHide;
        closeButton.SetActive(false);
        Timer.Schedule(this, 3, () => ShowCloseButton());
        Timer.Schedule(this, 10, () => Hide());
    }

    public void ShowWithoutCloseBtn()
    {
        gameObject.SetActive(true);
        closeButton.SetActive(false);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        onHide?.Invoke();
    }

    private void ShowCloseButton()
    {
        closeButton.SetActive(true);
    }


}
