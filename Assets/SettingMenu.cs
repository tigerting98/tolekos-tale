using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] MainMenu mainmenu;
    [SerializeField] protected Slider musicSlider;
    [SerializeField] protected Slider SFXSlider;
    public GameObject backButton;

    private void Awake()
    {
        musicSlider.value = GameManager.musicVolume;
        SFXSlider.value = GameManager.SFXVolume;
    }
    public void ChangeMusicVolume() {
        if (EventSystem.current.currentSelectedGameObject == musicSlider.gameObject)
        {
            GameManager.musicVolume = musicSlider.value;
            AudioManager.current.music.SetVolume();
        }
    }

    public void ChangeSFXVolume() {
        if (EventSystem.current.currentSelectedGameObject == SFXSlider.gameObject)
        {
            GameManager.SFXVolume = SFXSlider.value;
        }
    }
    protected virtual void Update()
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
    protected virtual void OnEnable()
    {
        musicSlider.value = GameManager.musicVolume;
        SFXSlider.value = GameManager.SFXVolume;
        Invoke("SetUp", 0.01f);   
    }
    protected void SetUp()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(SFXSlider.gameObject);
        musicSlider.value = GameManager.musicVolume;
        SFXSlider.value = GameManager.SFXVolume;
    }
}
