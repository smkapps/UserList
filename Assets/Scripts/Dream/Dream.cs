using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dream : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    private DreamDefinition dreamDefinition;
    private SpriteRenderer[] spriteRenderers;

    private void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        dreamDefinition = GetComponentInChildren<DreamDefinition>();

    }

    private void Start()
    {
        SetOrders();
    }

    private void OnEnable()
    {
        draged = false;
        
    }

    private void SetOrders()
    {
        OrderHolder.orderLayer += 3;
        foreach (var item in spriteRenderers)
        {
            item.sortingOrder += (OrderHolder.orderLayer);
        }
    }

    public void SetDreamDefinition(Sprite sprite)
    {
        dreamDefinition.Setup(sprite);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        draged = false;
    }


    bool draged;
    public void OnDrag(PointerEventData eventData)
    {
        if (draged || eventData.delta.magnitude < 5) return;
        float euler = (Quaternion.LookRotation(Vector3.forward, -Vector2.zero + eventData.delta)).eulerAngles.z;
        GetComponent<Movement>().ChangeDirection(euler);
        draged = true;
    }

}


public static class OrderHolder
{
    public static int orderLayer;
}