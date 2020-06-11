using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ShopButton : MonoBehaviour, ISelectHandler
{

    [SerializeField] Image itemIcon;
    [SerializeField] ItemDescription itemDescription;
    [SerializeField] TextMeshProUGUI costIcon, itemNameIcon;
    [SerializeField] Color available, unavailable, noGold;
    public ShopItem item;
    public event Action OnBoughtItem;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(BuyItem);
    }
    public void SetUp(ShopItem item, ItemDescription itemDescription)
    {
        itemIcon.sprite = item.icon;
        costIcon.text = "Price: " + item.cost.ToString();
        itemNameIcon.text = item.itemName;
        this.item = item;
        this.itemDescription = itemDescription;
        SetColor();
    }

    public void SetColor() {
        if (item.Buyable())
        {
            costIcon.color = available;

        }
        else {
            if (item.cost > PlayerStats.gold)
            {
                costIcon.color = noGold;
            }
            else {
                costIcon.color = unavailable;
            }

            
        
        }
    }



     public void OnSelect(BaseEventData eventData)
    {
        itemDescription.SetDescription(item);
    }
    public void BuyItem() {
        if (item.Buyable()) {
            item.Buy();
            OnBoughtItem?.Invoke();
        }
    
    }
}
