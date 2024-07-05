using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public DragSlot dragSlot;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!GetComponent<Slot>().isEmpty)
        {
            dragSlot.oldSlot = GetComponent<Slot>();
            dragSlot.Apply();
            dragSlot.gameObject.SetActive(true);
            dragSlot.isDragging = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        dragSlot.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragSlot.isDragging)
        {
            dragSlot.gameObject.SetActive(false);
            dragSlot.isDragging = false;
        }
    }

}
