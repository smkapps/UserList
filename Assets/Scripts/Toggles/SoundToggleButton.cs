using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggleButton : MonoBehaviour, IToggleButtonListener
{
    public bool GetIsOnStateInitial()
    {
        return false;
    }

    public void HandleClick(bool isOn)
    {
    }
}
