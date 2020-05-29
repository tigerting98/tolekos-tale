using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;
using System.Transactions;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] SceneLoader loader;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Controller controller;
    [SerializeField] GameObject warningMenu;
    [SerializeField] GameObject warningBackButton;
    [SerializeField] GameObject resumeButton;
    private void Awake()
    {
        warningMenu.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StatsText();
    }

    public void StatsText() {
        int curHP = GameManager.player ? (int)GameManager.player.health.GetCurrentHP() : 0;
        StringBuilder txt = new StringBuilder(1000);
        txt.Append(string.Format("Level: {0}\n", PlayerStats.playerLevel));
        txt.Append(string.Format("EXP: {0}/{1}\n", PlayerStats.exp, PlayerStats.expToLevelUp));
        txt.Append(string.Format("Health: {0}/{1}\n", curHP, (int)PlayerStats.playerMaxHP));
        txt.Append(string.Format("Bullet Damage: {0}\n", PlayerStats.damage));
        txt.Append(string.Format("Laser Damage: {0} per second", PlayerStats.damage * PlayerStats.laserDamageRatio));
        text.text = txt.ToString() ;
    
    }

    public void Resume() {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        if (GameManager.player) {
            GameManager.player.enabled = true;
        }
        controller.gameObject.SetActive(true);
    }
    public void Quit()
    {
        Resume();
        loader.ReturnToStartPage();
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
}
