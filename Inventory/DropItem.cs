using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropItem : MonoBehaviour, IDropHandler
{
    public DragSlot dragSlot;
    public GameObject dragSlotObj;
    public Transform dropPoint;
    public void OnDrop(PointerEventData eventData)
    {
        if (dragSlot.isDragging)
        {
            GameObject obj = Instantiate(dragSlot.oldSlot.item.prefab, dropPoint.position, Quaternion.identity);
            obj.GetComponent<Item>().itemCount = dragSlot.oldSlot.itemCount;
            dragSlot.oldSlot.ResetSlot();
            dragSlotObj.SetActive(false);
            dragSlot.isDragging = false;
        }
    }
}
