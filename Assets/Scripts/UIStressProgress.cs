using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStressProgress : MonoBehaviour
{
    private ProgressBarView progressBar;
    [SerializeField] private TextMeshProUGUI stressText;
    private Stress stress;

    private void Awake()
    {
        stress = FindObjectOfType<Stress>();
        progressBar = GetComponentInChildren<ProgressBarView>();
        Stress.StressChanged += OnStressChanged;

        SetInitialValues();
    }

    private void OnDestroy()
    {
        Stress.StressChanged -= OnStressChanged;
    }

    private void OnStressChanged(int stressValue)
    {
        SetTextStress(stress.StressValue);
        progressBar.AnimateFill(stressValue, stress.StressMaxValue, 0.2f);
    }


    private void SetInitialValues()
    {
        progressBar.SetInitialValue(stress.StressValue, stress.StressMaxValue);
        SetTextStress(stress.StressValue);
    }



    private void SetTextStress(int stressValue)
    {
        stressText.text = string.Format("{0}/{1}", stressValue, stress.StressMaxValue);
    }


}
