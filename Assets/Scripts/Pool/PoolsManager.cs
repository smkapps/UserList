using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoolsManager : MonoSingleton<PoolsManager>
{

    //[System.Serializable]
    //public class PoolSettings
    //{
    //    public GameObject prefab;
    //    public int initialCount;
    //    public Transform parent;
    //}

    //[SerializeField] private List<PoolSettings> poolsSettings;
    //private Dictionary<GameObject, ObjectPool> pools = new Dictionary<GameObject, ObjectPool>();

    private Dictionary<GameObject, ObjectPool> dynamicPools = new Dictionary<GameObject, ObjectPool>();

    //private void Awake()
    //{
    //    for (int i = 0; i < poolsSettings.Count; i++)
    //    {
    //        PoolSettings poolSettings = poolsSettings[i];
    //        ObjectPool objectPool = new ObjectPool(poolSettings.prefab, poolSettings.initialCount, poolSettings.parent);         
    //        pools.Add(poolSettings.prefab, objectPool);

    //    }
    //}

    //public GameObject GetObject(GameObject prefab)
    //{
    //    return pools[prefab].GetObject();
    //}

    //public GameObject GetObjectDynamic(GameObject prefab)
    //{
    //    if(dynamicPools.ContainsKey(prefab) == false)
    //    {
    //        ObjectPool objectPool = new ObjectPool(prefab, 1, transform);
    //        dynamicPools.Add(prefab, objectPool);
    //    }
    //    return dynamicPools[prefab].GetObject();
    //}

    //public void DeactivateAllObjectsOfPool(GameObject prefab)
    //{
    //    ObjectPool pool = pools[prefab];
    //    pool.DeactivateAll();
    //}
}

