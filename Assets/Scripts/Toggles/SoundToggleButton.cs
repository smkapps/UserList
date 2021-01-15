using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggleButton : MonoBehaviour, IToggleButtonListener
{
    public bool GetIsOnStateInitial()
    {
        return GameProgress.Instance.SoundIsOn;
    }

    public void HandleClick(bool isOn)
    {
        GameProgress.Instance.SoundIsOn = isOn;
    }
}
