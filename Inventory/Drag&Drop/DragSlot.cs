using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour, IPointerMoveHandler
{
    public Slot oldSlot;
    public bool isDragging = false;
    public void Apply()
    {
        GetComponent<Image>().sprite = oldSlot.icon;
    }
    public void OnPointerMove(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;

    }
}
