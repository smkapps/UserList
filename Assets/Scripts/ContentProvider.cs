using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContentProvider : MonoSingleton<ContentProvider>, IUnlockLocationConfig
{
    private TimeProgress timeProgress;    
    [SerializeField] private List<LocationAssets> locationsAssets;
    private int mostRecentUnlockedLocationID => timeProgress.MostRecentUnlockedLocationID;
    private LocationAssets currentPlayingLocationAsset => locationsAssets[currentPlayingLocationID];
    private int currentPlayingLocationID;
    public int LastLocationID => locationsAssets.Count - 1;

    private void Awake()
    {
        currentPlayingLocationID = GameProgress.Instance.CurrentPlayLocationID;
        timeProgress = FindObjectOfType<TimeProgress>();
        UIWindowLocationUnlocked.UnlockedLocationApplied += OnUnlockedLocationApplied;
        LocationChanger.CurrentPlayLocationChanged += LocationChanger_CurrentPlayLocationChanged;
    }


    protected override void OnDestroy()
    {
        base.OnDestroy();
        UIWindowLocationUnlocked.UnlockedLocationApplied -= OnUnlockedLocationApplied;
        LocationChanger.CurrentPlayLocationChanged -= LocationChanger_CurrentPlayLocationChanged;
    }

    private void OnUnlockedLocationApplied()
    {
        currentPlayingLocationID = mostRecentUnlockedLocationID;
        GameProgress.Instance.CurrentPlayLocationID = currentPlayingLocationID;
    }

    private void LocationChanger_CurrentPlayLocationChanged()
    {
        currentPlayingLocationID = GameProgress.Instance.CurrentPlayLocationID;
    }

    public TimeSpan GetTimeSpanToUnlockNextLocation(int currentLocationId)
    {
        if (currentLocationId < 0 || currentLocationId >= LastLocationID) throw new Exception("Invalid location ID");
        return TimeSpan.FromMinutes(locationsAssets[currentLocationId + 1].MinutesOnPreviousStageToUnlock);
    }

    public Sprite GetRandomDreamDefinitionSprite()
    {
        DreamSpritesList dreamSprites = currentPlayingLocationAsset.DreamsSpritesList;
        return dreamSprites.GetRandomDreamSprite();
    }

    public Sprite GetNextCircleSkinSprite()
    {
        if (timeProgress.AllLocationsUnlocked) throw new Exception("AllLocationsUnlocked Next Circle SkinSprite not available");
        return locationsAssets[mostRecentUnlockedLocationID + 1].CircleSkin;
    }

    public Sprite GetCurrentCircleSkinWithShapesSprite()
    {
        return locationsAssets[mostRecentUnlockedLocationID].CircleSkinWithShapes;
    }

    public Sprite GetCurrentPlayBGSprite()
    {
        return currentPlayingLocationAsset.Background;
    }

    public GameObject GetCurrentLocationRandomDreamPrefab(Vector2 position)
    {
        GameObject prefab = currentPlayingLocationAsset.ShapesList.GetRandomDream();
        return GetObjectDynamic(prefab, position);
    }


    private Dictionary<GameObject, ObjectPool> dynamicPools = new Dictionary<GameObject, ObjectPool>();
    private GameObject GetObjectDynamic(GameObject prefab, Vector2 position)
    {
        if (dynamicPools.ContainsKey(prefab) == false)
        {
            ObjectPool objectPool = new ObjectPool(prefab, 1, transform);
            dynamicPools.Add(prefab, objectPool);
        }
        return dynamicPools[prefab].GetObject(position);
    }

}
