using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] GameObject intoShop;
    [SerializeField] GameObject shopList;
    [SerializeField] PlayerShop playerShop;
    [SerializeField] ItemDescription itemDescription;
    [SerializeField] List<ShopItem> items;
    [SerializeField] ShopButton shopButton;
    [SerializeField] EventSystem system;
    [SerializeField] Button backButton;
    List<ShopButton> menuItems = new List<ShopButton>();
    public event Action OnCloseShop;

    private void Awake()
    {
        
        for (int i = 0; i < items.Count; i++) {
            ShopButton item = Instantiate(shopButton, shopList.transform);
            item.SetUp(items[i], itemDescription);
            menuItems.Add(item);
            item.OnBoughtItem += playerShop.Refresh;
            item.OnBoughtItem += SetColor;
        
        }
        for (int i = 0; i < items.Count; i++) {
            Navigation nav = menuItems[i].GetComponent<Button>().navigation;
            nav.mode = Navigation.Mode.Explicit;
            nav.selectOnDown = i == items.Count - 1 ? backButton : menuItems[i + 1].GetComponent<Button>();
            nav.selectOnUp = i == 0 ? backButton : menuItems[i - 1].GetComponent<Button>();
            menuItems[i].GetComponent<Button>().navigation = nav;
        }
        Navigation backNav = backButton.navigation;
        backNav.mode = Navigation.Mode.Explicit;
        backNav.selectOnDown = menuItems[0].GetComponent<Button>();
        backNav.selectOnUp = menuItems[items.Count-1].GetComponent<Button>();
        backButton.navigation = backNav;
        backButton.onClick.AddListener(SetInactive);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        Invoke("EnableInvoke", 0.01f);
        
    }

    void SetColor() { 
        for (int i = 0; i < menuItems.Count; i++)
        {
            menuItems[i].SetColor();
        }
    
    }
    private void EnableInvoke() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menuItems[0].gameObject);
    }

    private void SetInactive() {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(intoShop);
        gameObject.SetActive(false);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (EventSystem.current.currentSelectedGameObject == backButton.gameObject)
            {
                SetInactive();
            }
            else { EventSystem.current.SetSelectedGameObject(backButton.gameObject); }
        
        }
    }
}
