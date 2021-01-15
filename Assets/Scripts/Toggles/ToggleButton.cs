using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof (Button))]
[RequireComponent (typeof (IToggleButtonListener))]
public class ToggleButton : MonoBehaviour
{
    private bool isOn;
    private Button button;
    private IToggleButtonListener listener;
    [SerializeField] private GameObject activeStateView;
    [SerializeField] private GameObject notActiveStateView;

    private void Start()
    {
        button = GetComponent<Button>();
        listener = GetComponent<IToggleButtonListener>();
        if (listener == null)
        {
            throw new System.Exception("Toggle Button needs IToggleButtonListener Component");
        }
        SetIsOnState(listener.GetIsOnStateInitial());
        button.onClick.AddListener(OnClick);
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    private void SetIsOnState(bool isOn)
    {
        this.isOn = isOn;
        activeStateView.SetActive(isOn);
        notActiveStateView.SetActive(isOn == false);
    }

    private void OnClick()
    {
        SetIsOnState(!isOn);
        listener.HandleClick(isOn);
    }
}


public interface IToggleButtonListener
{
    void HandleClick(bool isOn);
    bool GetIsOnStateInitial();
}