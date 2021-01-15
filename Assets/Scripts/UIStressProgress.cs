using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStressProgress : MonoBehaviour
{
    private ProgressBarView progressBar;
    [SerializeField] private TextMeshProUGUI stressText;
    private Hero hero;

    private void Awake()
    {
        hero = FindObjectOfType<Hero>();
        progressBar = GetComponentInChildren<ProgressBarView>();
        Hero.Stressed += OnHeroStressed;

        SetInitialValues();
    }

    private void OnDestroy()
    {
        Hero.Stressed -= OnHeroStressed;
    }

    private void SetInitialValues()
    {
        progressBar.SetInitialValue(hero.stressValue, hero.stressMaxValue);
        SetTextStress(new StressInfo(hero.stressValue, hero.stressMaxValue));
    }

    private void OnHeroStressed(StressInfo stressInfo)
    {
        SetTextStress(stressInfo);
        progressBar.AnimateFill(stressInfo.currentValue, stressInfo.maxValue, 0.2f);
    }

    private void SetTextStress(StressInfo stressInfo)
    {
        stressText.text = string.Format("{0}/{1}", stressInfo.currentValue, stressInfo.maxValue);
    }


}
