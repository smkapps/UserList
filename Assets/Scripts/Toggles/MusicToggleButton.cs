using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicToggleButton : MonoBehaviour, IToggleButtonListener
{
    public  bool GetIsOnStateInitial()
    {
        return GameProgress.Instance.MusicIsOn;
    }

    public void HandleClick(bool isOn)
    {
        GameProgress.Instance.MusicIsOn = isOn;

        if (isOn) AudioManager.Instance.PlayMelody();
        else AudioManager.Instance.PauseMelody();
    }
}
