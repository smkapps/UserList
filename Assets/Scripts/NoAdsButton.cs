using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoAdsButton : MonoBehaviour
{
    private Button button;
    public static event Action Clicked;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => OnClick());
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    private void Start()
    {
        gameObject.SetActive(GameProgress.Instance.NoAds == false);
        GameProgress.ProgressChanged += OnProgressChanged;
    }

    private void OnProgressChanged()
    {
        if (gameObject.activeSelf && GameProgress.Instance.NoAds) gameObject.SetActive(false);
    }

    private void OnClick()
    {
        Clicked?.Invoke();
    }
}
