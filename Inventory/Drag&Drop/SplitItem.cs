using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SplitItem : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI count;

    Slot oldSlot, newSlot;

    private void Start()
    {
        ChangeText();
    }
    public void ShowPanel(Slot slot, Slot oldslot, uint count)
    {
        oldSlot = oldslot;
        newSlot = slot;
        slider.maxValue = count;
        slider.minValue = 1;
        gameObject.SetActive(true);
    }
    public void ApplySplit()
    {
        newSlot.GetNewSlotItem(oldSlot, Convert.ToUInt32(slider.value));
        oldSlot.RemoveItemCount(Convert.ToUInt32(slider.value));
        gameObject.SetActive(false);
    }
    public void ChangeText()
    {
        count.text = slider.value.ToString("F0");
    }

}
