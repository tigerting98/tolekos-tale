using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using UnityEngine.EventSystems;
using System.Transactions;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] SceneLoader loader = default;
    [SerializeField] TextMeshProUGUI text = default;
    [SerializeField] Controller controller = default;
    [SerializeField] GameObject warningMenu = default;
    [SerializeField] GameObject warningBackButton = default;
    [SerializeField] GameObject resumeButton = default;
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
        txt.Append("Damages\n");
        txt.Append(string.Format("Water Unfocus Damage: {0}\n", PlayerStats.damage));
        txt.Append(string.Format("Water Focus Damage: {0}\n", PlayerStats.damage));
        txt.Append(string.Format("Earth Unfocus Damage: {0}\n", PlayerStats.damage));
        txt.Append(string.Format("Earth Focus Damage: {0}\n", PlayerStats.damage* PlayerStats.earthFocusDaamgeRatio));
        txt.Append(string.Format("Fire Unfocus Damage: {0} per second\n", PlayerStats.damage * PlayerStats.fireUnfocusDamageRatio));
        txt.Append(string.Format("Fire Focus Damage: {0} per second", PlayerStats.damage * PlayerStats.fireFocusDamageRatio));
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
