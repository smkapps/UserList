using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{
    static List<object> tempShuffle = new List<object>();
    public static void Shuffle<T>(this System.Random rng, List<T> list)
    {
        tempShuffle.Clear();
        for (int i = 0; i < list.Count; i++)
        {
            tempShuffle.Add(list[i]);

        }
        if (tempShuffle.Count == 0) return;
        int n = list.Count;      
        while (n > 1)
        {
            int k = rng.Next(n--);
            T temp = list[n];
            list[n] = list[k];
            list[k] = temp;
        }

        bool shuffledListIsTheSame = true;
        for (int i = 0; i < tempShuffle.Count; i++)
        {
            if (tempShuffle[i].Equals(list[i]) == false)
            {
                shuffledListIsTheSame = false;
                break;
            }                
        }

        if (shuffledListIsTheSame) Shuffle(rng, list);
    }

    public static Color WithAlpha(this Color color, float alpha)
    {
        color.a = alpha;
        return color;
    }

    public static Vector2 WithY(this Vector2 vector2, float y)
    {
        return new Vector2(vector2.x, y);
    }

    public static Vector2 WithX(this Vector2 vector2, float x)
    {
        return new Vector2(x, vector2.y);
    }

    public static Vector3 WithX(this Vector3 vector3, float x)
    {
        return new Vector3(x, vector3.y, vector3.z);
    }


    public static Vector3 WithY(this Vector3 vector3, float y)
    {
        return new Vector3(vector3.x, y, vector3.z);
    }

    public static Vector3 WithZ(this Vector3 vector3, float z)
    {
        return new Vector3(vector3.x, vector3.y, z);
    }

    public static IEnumerator DoFade(this SpriteRenderer renderer, float duration, float targetAlpha, Action onComplete = null)
    {
        float time = duration;
        float elapsedTime = 0;
        float startAlpha = renderer.color.a;
        while (elapsedTime <= time)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / time);
            renderer.color = renderer.color.WithAlpha(alpha);
            yield return null;
        }
        renderer.color = renderer.color.WithAlpha(targetAlpha);
        onComplete?.Invoke();
    }
}
