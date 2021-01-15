using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ObjectPool 
{
  
    private GameObject objectPrefab;
	private List<GameObject> instantiatedObjects = new List<GameObject>();
	private Transform parent;
    
	public ObjectPool(GameObject objectPrefab, int initialSize, Transform parent)
	{
		this.objectPrefab = objectPrefab;
		this.parent = parent;


        for (int i = 0; i < initialSize; i++)
		{
			GameObject obj = CreateObject();            
            obj.SetActive(false);
        }
    }

	//public GameObject GetObject()
	//{
	//	for (int i = 0; i < instantiatedObjects.Count; i++)
	//	{
 //           if (!instantiatedObjects[i].activeSelf)
	//		{
	//			return instantiatedObjects[i];
	//		}
	//	}
	//	return CreateObject();
	//}

    public GameObject GetObject(Vector2 atPosition)
	{
		for (int i = 0; i < instantiatedObjects.Count; i++)
		{
            if (!instantiatedObjects[i].activeSelf)
			{
                instantiatedObjects[i].transform.position = atPosition;

                return instantiatedObjects[i];
			}
		}
		return CreateObject(atPosition);
	}

 //   public GameObject GetActiveObject()
	//{
 //       GameObject obj = GetObject();
 //       obj.SetActive(true);
 //       return obj;
	//}

    public void DeactivateAll()
    {
        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            if (instantiatedObjects[i].activeSelf)
            {
                instantiatedObjects[i].SetActive(false);
            }
        }

    }

    private GameObject CreateObject()
	{
		GameObject obj = GameObject.Instantiate(objectPrefab, parent);
		instantiatedObjects.Add(obj);
		return obj;
	}

    private GameObject CreateObject(Vector2 position)
	{
		GameObject obj = GameObject.Instantiate(objectPrefab, position, Quaternion.identity, parent);
		instantiatedObjects.Add(obj);
		return obj;
	}

}
