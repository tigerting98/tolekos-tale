using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;

//This class cxontrols the pause menu of the game
public class PauseMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = default;
    [SerializeField] Controller controller = default;
    [SerializeField] GameObject warningMenu = default;
    [SerializeField] GameObject warningBackButton = default;
    [SerializeField] GameObject resumeButton = default;
    [SerializeField] GameObject settingButton = default;
    [SerializeField] GameObject settingMenu;
    [SerializeField] GameObject controlButton, controlMenu, controlBackButton;
    public GameObject lastSelected;

    private void Awake()
    {
        GameManager.pauseMenu = this;
        warningMenu.SetActive(false);
        gameObject.SetActive(false);
        settingMenu.SetActive(false);
        controlMenu.SetActive(false);
    }

    private void OnEnable() {
        Cursor.visible = true;
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine("Enable");
    }
    IEnumerator Enable() {
        yield return new WaitForSecondsRealtime(0.1f);
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        lastSelected = resumeButton.gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        StatsText();
        GameObject current = EventSystem.current.currentSelectedGameObject;
        if (current != lastSelected && current != null) {
            lastSelected = current;
        }
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected&&lastSelected.activeInHierarchy) 
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)&&controlMenu.gameObject.activeInHierarchy) {
            CloseControls();
         
        }
    }

    public void StatsText() {
        int curHP = GameManager.player ? (int)GameManager.player.health.GetCurrentHP() : 0;
        StringBuilder txt = new StringBuilder(1000);
        txt.Append(string.Format("Difficulty: {0}\n", GameManager.difficultyLevel.ToString()));
        txt.Append(string.Format("Level: {0}\n", PlayerStats.playerLevel));
        txt.Append(string.Format("EXP: {0}/{1}\n", PlayerStats.exp, PlayerStats.expToLevelUp));
        txt.Append(string.Format("Health: {0}/{1}\n", curHP, (int)PlayerStats.playerMaxHP));
        txt.Append(string.Format("Water Unfocus Damage: {0}\n", PlayerStats.damage * PlayerStats.shotDamageMultiplier));
        txt.Append(string.Format("Water Focus Damage: {0}\n", PlayerStats.damage * PlayerStats.shotDamageMultiplier ));
        txt.Append(string.Format("Earth Unfocus Damage: {0}\n", PlayerStats.damage * PlayerStats.shotDamageMultiplier* PlayerStats.earthUnfocusRatio));
        txt.Append(string.Format("Earth Focus Damage: {0}\n", PlayerStats.damage* PlayerStats.earthFocusDaamgeRatio * PlayerStats.shotDamageMultiplier));
        txt.Append(string.Format("Fire Unfocus Damage: {0} per second\n", PlayerStats.damage * PlayerStats.fireUnfocusDamageRatio * PlayerStats.shotDamageMultiplier));
        txt.Append(string.Format("Fire Focus Damage: {0} per second\n", PlayerStats.damage * PlayerStats.fireFocusDamageRatio * PlayerStats.shotDamageMultiplier));
        txt.Append(string.Format("Bomb Damage: {0} per part\n", PlayerStats.bombDamage*PlayerStats.bombEffectiveness));
        txt.Append(string.Format("Number Of Deaths: {0}\n", PlayerStats.deathCount));
        text.text = txt.ToString() ;
    
    }

    public void Resume() {
        Cursor.visible = false;
        Time.timeScale = 1f;
        AudioManager.current.music.source.Play();
        gameObject.SetActive(false);
        if (GameManager.player) {
            GameManager.player.enabled = true;
            
        }
        GameManager.gameInput.enabled = true;
        controller.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Resume();
        GameManager.sceneLoader.ReturnToStartPage();
    }
    public void OpenWarning()
    {
        warningMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(warningBackButton);
    }

    public void CloseWarning()
    {
        warningMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(resumeButton);

    }
    public void OpenSetting()
    {
        settingMenu.SetActive(true);
        
    }
    public void OpenControls() {
        controlMenu.SetActive(true);
        StartCoroutine(ControlsOpen());
    }
    IEnumerator ControlsOpen() {
        yield return new WaitForSecondsRealtime(0.01f);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlBackButton);
    }
    public void CloseControls()
    {
        controlMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlButton);

    }
    public void CloseSetting()
    {
        settingMenu.GetComponent<SettingMenu>().backButton.GetComponent<ButtonPointer>().OnDeselect(null);
        settingMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(settingButton);

    }
}
