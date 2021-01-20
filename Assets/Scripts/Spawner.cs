using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private const int SPAWN_INTERVAL = 3;
    [SerializeField] private int currentCount = 0;
    private int targetCount = 3;    
    private Camera cameraMain;
    private bool paused => UIControllerFacade.Instance.AnyWindowOpen;
    private WaitForSeconds waitSpawnInterval = new WaitForSeconds(SPAWN_INTERVAL);
    private int dreamsLayerMask;

    private void Awake()
    {
        dreamsLayerMask = LayerMask.GetMask(new string[] { "Default" });
        cameraMain = Camera.main;
        StartCoroutine(CreateDreamsEndless());
    }
   
    private IEnumerator CreateDreamsEndless()
    {
        yield return new WaitForSeconds(1);
        while (true)
        {
            while (paused) yield return null;
            CreateNDreams(targetCount);
            yield return waitSpawnInterval;
        }
    }

    private void CreateNDreams(int count)
    {
        for (int i = 0; i < count; i++)
        {
            CreateDream();
        }
    }

    private void CreateDream()
    {
        Vector2 position;
        try
        {
            position = GetRandomPointOnPerimeter();
        }
        catch (System.StackOverflowException)
        {
            Debug.Log("Cant find point on perimeter");
            return;
        }
        
        GameObject dream = ContentProvider.Instance.GetCurrentLocationRandomDreamPrefab(position);
        dream.transform.position = position;
        dream.transform.localScale = Vector2.one * Random.Range(0.9f, 1.4f);
        Sprite definitionSprite = ContentProvider.Instance.GetRandomDreamDefinitionSprite();
        dream.SetActive(true);
        dream.GetComponent<Dream>().SetDreamDefinition(definitionSprite);
        currentCount++;
    }


    private Vector2 GetRandomPointInScreenArea()
    {
        float screenHalfHeightUnits = cameraMain.orthographicSize * 0.9f;
        float screenHalfWidthUnits = cameraMain.orthographicSize * cameraMain.aspect * 0.9f;
        return new Vector2(Random.Range(-screenHalfWidthUnits, screenHalfWidthUnits), Random.Range(-screenHalfHeightUnits, screenHalfHeightUnits));
    }

    private Vector2 GetRandomPointOnPerimeter()
    {

        Vector2 perimeterPoint = RandomPerimeterPointToVector2(Screen.width, Screen.height);
        Vector2 position = cameraMain.ScreenToWorldPoint(new Vector3(perimeterPoint.x, perimeterPoint.y, 0));
        if (Physics2D.OverlapCircleAll(position, 2, dreamsLayerMask).Length > 0) return GetRandomPointOnPerimeter();
        return position;
    }

    private int Perimeter(int width, int height)
    {
        return height * 2 + width * 2;
    }

    private Vector2 RandomPerimeterPointToVector2(int width, int height)
    {
        int point = Random.Range(0, Perimeter(width, height) + 1);
        if (point <= width) return new Vector2(point, 0);
        if (point <= width + height) return new Vector2(width, point - width);
        if (point <= width * 2 + height) return new Vector2(point - (width + height), height);
        return new Vector2(0, point - (width * 2 + height));

    }

}
