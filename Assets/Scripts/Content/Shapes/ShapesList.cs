using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ShapesList : ScriptableObject
{
    [SerializeField] private List<GameObject> shapes;

    public GameObject GetRandomDream()
    {
        return shapes[Random.Range(0, shapes.Count)];
    }
}
