using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgrpound;

    private void Awake()
    {
        UIWindowLocationUnlocked.UnlockedLocationApplied += OnNewLocationApplied;
        LocationChanger.CurrentPlayLocationChanged += OnNewLocationApplied;
        SetLocationBG();
    }

    private void OnDestroy()
    {
        UIWindowLocationUnlocked.UnlockedLocationApplied -= OnNewLocationApplied;
        LocationChanger.CurrentPlayLocationChanged -= OnNewLocationApplied;
    }

    private void OnNewLocationApplied()
    {
        SetLocationBG();
    }

    private void SetLocationBG()
    {
        backgrpound.sprite = ContentProvider.Instance.GetCurrentPlayBGSprite();
    }
}
