using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private GameObject on;
    [SerializeField] private GameObject off;

    public void SetIsOn(bool isOn)
    {
        on.SetActive(isOn);
        off.SetActive(!isOn);
    }
}
