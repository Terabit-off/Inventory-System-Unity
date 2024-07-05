using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public DragSlot dragSlot;
    public SlotInfo slotInfo;
    public SplitItem splitItem;
    [Space(15)]
    public bool isEmpty = false;
    public uint itemId;
    public bool isFull = false;
    public Sprite icon;
    public Items item;
    public uint itemCount;
    [Space]
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI text;
    public void Apply()
    {
        img.sprite = icon;
 /*       if (itemCount >= item.maxStack) isFull = true;
        else isFull = false;*/

        if (itemCount > 1) text.text = itemCount.ToString();
        else text.text = " ";

        if (itemCount == 0) ResetSlot();
    }

    // ----------------------------------
    public void GetNewSlotItem(Slot oldSlot)
    {
        isEmpty = oldSlot.isEmpty;
        itemId = oldSlot.itemId;
        isFull = oldSlot.isFull;
        icon = oldSlot.icon;
        item = oldSlot.item;
        itemCount = oldSlot.itemCount;
        Apply();
    }
    public void GetNewSlotItem(Slot oldSlot, uint count)
    {
        isEmpty = oldSlot.isEmpty;
        itemId = oldSlot.itemId;
        isFull = oldSlot.isFull;
        icon = oldSlot.icon;
        item = oldSlot.item;
        itemCount = count;
        Apply();
    }
    // ----------------------------------

    public void ResetSlot()
    {
        isEmpty = true;
        itemId = 0;
        isFull = false;
        icon = null;
        item = null;
        itemCount = 0;
        img.sprite = icon;
        text.text = " ";
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (dragSlot.isDragging)
        {
            if (dragSlot.oldSlot != this)
            {
                if (isEmpty && !Input.GetKey(KeyCode.LeftControl))
                {
                    GetNewSlotItem(dragSlot.oldSlot);
                    dragSlot.oldSlot.ResetSlot();
                }
                else if (isEmpty && Input.GetKey(KeyCode.LeftControl) && dragSlot.oldSlot.itemCount > 1)
                {
                    dragSlot.gameObject.SetActive(false);
                    SplitSlot(dragSlot.oldSlot);
                }
                else
                {
                    if (itemId == dragSlot.oldSlot.itemId && !isFull)
                    {
                        uint can = dragSlot.oldSlot.item.maxStack - itemCount;
                        if (dragSlot.oldSlot.itemCount > can)
                        {

                            uint remains = dragSlot.oldSlot.itemCount - can;
                            dragSlot.oldSlot.itemCount = remains;
                            dragSlot.oldSlot.Apply();


                            isEmpty = false;
                            itemId = dragSlot.oldSlot.itemId;
                            isFull = true;
                            icon = dragSlot.oldSlot.icon;
                            item = dragSlot.oldSlot.item;
                            itemCount += can;
                            Apply();
                        }
                        else
                        {

                            isEmpty = false;
                            itemId = dragSlot.oldSlot.itemId;
                            isFull = true;
                            icon = dragSlot.oldSlot.icon;
                            item = dragSlot.oldSlot.item;
                            itemCount += dragSlot.oldSlot.itemCount;
                            Apply();
                            dragSlot.oldSlot.ResetSlot();
                        }
                    }
                    else ReplaceSlots(dragSlot.oldSlot);
                }
            }
            dragSlot.gameObject.SetActive(false);
            dragSlot.isDragging = false;
        }
    }
    void SplitSlot(Slot oldSlot)
    {
        splitItem.gameObject.SetActive(true);
        splitItem.ShowPanel(this, dragSlot.oldSlot, oldSlot.itemCount);
    }
    public void RemoveItemCount(uint count)
    {
        itemCount -= count;
        Apply();
    }
    private void ReplaceSlots(Slot oldSlot)
    {
        // T = Temp
        bool isEmptyT = oldSlot.isEmpty;
        uint itemIdT = oldSlot.itemId;
        bool isFullT = oldSlot.isFull;
        Sprite iconT = oldSlot.icon;
        Items itemT = oldSlot.item;
        uint itemCountT = oldSlot.itemCount;

        oldSlot.isEmpty = isEmpty;
        oldSlot.itemId = itemId;
        oldSlot.isFull = isFull;
        oldSlot.icon = icon;
        oldSlot.item = item;
        oldSlot.itemCount = itemCount;

        isEmpty = isEmptyT;
        itemId = itemIdT;
        isFull = isFullT;
        icon = iconT;
        item = itemT;
        itemCount = itemCountT;

        Apply();
        oldSlot.Apply();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!isEmpty)
            slotInfo.TextShow(item.itemName, itemCount, item.category.ToString());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if(!isEmpty)
            slotInfo.TextHide();
    }
}
