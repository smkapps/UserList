using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Dream/Movement")]
public class MovementConfig : ScriptableObject
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float rotateSpeed = 4f;
    [SerializeField] private float minAngleToTurn = 10;
    [SerializeField] private float maxAngleToTurn = 40;

    public float Speed { get => speed; }
    public float RotateSpeed { get => rotateSpeed; }
    public float MinAngleToTurn { get => minAngleToTurn; }
    public float MaxAngleToTurn { get => maxAngleToTurn;  }
}
