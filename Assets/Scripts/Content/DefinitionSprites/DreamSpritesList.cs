using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DreamSpritesList : ScriptableObject
{
    [SerializeField] private List<Sprite> dreamSprites;

    public Sprite GetRandomDreamSprite()
    {
        return dreamSprites[Random.Range(0, dreamSprites.Count)];
    }
}
