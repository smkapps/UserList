using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ClickHandler : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent Clicked;

    public void OnPointerClick(PointerEventData eventData)
    {
        Clicked?.Invoke();
    }
}
