using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Borders : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    private float offfsetUnits = 5;

    private void Awake()
    {
        float screenHalfWidthUnits = Camera.main.orthographicSize * Camera.main.aspect;
        left.position = new Vector2(-screenHalfWidthUnits - offfsetUnits, 0);
        right.position = new Vector2(screenHalfWidthUnits + offfsetUnits, 0);
    }
}
