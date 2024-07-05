using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SlotInfo : MonoBehaviour
{
    public TextMeshProUGUI nameText, countText, categoryText;
    public GameObject childObject;
    
    public void TextShow(string name, uint count, string category)
    {
        nameText.text = name;
        countText.text = count.ToString();
        categoryText.text = category;
        childObject.SetActive(true);
    }
    public void TextHide()
    {
        childObject.SetActive(false);
    }
    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}
