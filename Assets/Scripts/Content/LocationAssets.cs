using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LocationAssets : ScriptableObject
{
    [SerializeField] private Sprite background;
    [SerializeField] private ShapesList shapesList;
    [SerializeField] private DreamSpritesList dreamsSpritesList;
    [SerializeField] private Sprite circleSkinWithShapes;
    [SerializeField] private Sprite circleSkin;
    [SerializeField] private float minutesOnPreviousStageToUnlock;

    public ShapesList ShapesList => shapesList;
    public Sprite CircleSkinWithShapes { get => circleSkinWithShapes; }
    public Sprite CircleSkin { get => circleSkin; }
    public Sprite Background { get => background; }
    public float MinutesOnPreviousStageToUnlock { get => minutesOnPreviousStageToUnlock;}
    public DreamSpritesList DreamsSpritesList { get => dreamsSpritesList; }
}
