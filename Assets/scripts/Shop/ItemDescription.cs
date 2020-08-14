using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDescription : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemName, itemText;
    [SerializeField] Image itemImage;

    public void SetDescription(ShopItem item) {
        itemName.text = item.itemName;
        itemText.text = item.description;
        itemImage.sprite = item.icon;
    }
}
