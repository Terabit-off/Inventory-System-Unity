using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<Slot> slots;
    [SerializeField] GameObject splitPanel;
    public void PickUp(Items item, GameObject obj)
    {
        uint id = item.id;
        uint maxStack = item.maxStack;
        uint count = obj.GetComponent<Item>().itemCount;

        foreach(Slot slot in slots)
        {
            if (slot.isEmpty)
            {
                if(count > maxStack || count == maxStack)
                {
                    slot.itemCount = maxStack;
                    slot.isEmpty = false;
                    slot.itemId = id;
                    slot.isFull = true;
                    slot.icon = item.icon;
                    slot.item = item;
                    slot.Apply();
                    count -= maxStack;
                }
                else
                {
                    slot.itemCount = count;
                    slot.isEmpty = false;
                    slot.itemId = id;
                    slot.isFull = false;
                    slot.icon = item.icon;
                    slot.item = item;
                    slot.Apply();
                    Destroy(obj);
                    return;
                }
            }
            else
            {
                if(slot.itemId == id)
                {
                    if (!slot.isFull)
                    {
                        uint need = maxStack - slot.itemCount;
                        if(need > count)
                        {
                            slot.itemCount += count;
                            slot.isEmpty = false;
                            slot.itemId = id;
                            slot.isFull = false;
                            slot.icon = item.icon;
                            slot.item = item;
                            slot.Apply();
                            Destroy(obj);
                            return;
                        }
                        else if(need == count)
                        {
                            slot.itemCount += need;
                            slot.isEmpty = false;
                            slot.itemId = id;
                            slot.isFull = true;
                            slot.icon = item.icon;
                            slot.item = item;
                            slot.Apply();
                            Destroy(obj);
                            return;
                        }
                        else
                        {
                            slot.itemCount += need;
                            slot.isEmpty = false;
                            slot.itemId = id;
                            slot.isFull = false;
                            slot.icon = item.icon;
                            slot.item = item;
                            slot.Apply();

                            count -= need;
                        }
                    }
                }
            }
        }
    }

    public void SplitPanelDisable() => splitPanel.SetActive(false);
}
