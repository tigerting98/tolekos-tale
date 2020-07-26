using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//Setting menu used in the pause screen
public class SettingPauseMenu : SettingMenu
{
    [SerializeField] PauseMenu pauseMenu;
    
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenu.lastSelected == backButton)
            {

                pauseMenu.CloseSetting();
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(backButton);
            }
        }
    }
    protected override void OnEnable()
    {
        musicSlider.value = GameManager.musicVolume;
        SFXSlider.value = GameManager.SFXVolume;
        StartCoroutine(Config());
    }

    IEnumerator Config() {
        yield return new WaitForSecondsRealtime(0.01f);
        SetUp();
    }

}
