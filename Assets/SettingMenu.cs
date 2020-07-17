using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] MainMenu mainmenu;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;
    public GameObject backButton;

    public void ChangeMusicVolume() {
        GameManager.musicVolume = musicSlider.value;

    }

    public void ChangeSFXVolume() {
        GameManager.SFXVolume = SFXSlider.value;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (mainmenu.lastSelected == backButton)
            {
                
                mainmenu.CloseSettingsMenu();
            }
            else {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(backButton);
            }
        }
    }
    private void OnEnable()
    {
        Invoke("SetUp", 0.01f);   
    }
    void SetUp()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SFXSlider.gameObject);
        musicSlider.value = GameManager.musicVolume;
        SFXSlider.value = GameManager.SFXVolume;
    }
}
