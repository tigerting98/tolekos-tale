using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : Death
{
    // Start is called before the first frame update
    public override void Die()
    {
        GeneralDie();
        PlayerStats.deathCount++;
        Invoke("OpenDeathMenu", 2f);
        gameObject.SetActive(false);
    }

    void OpenDeathMenu() {
        GameManager.deathMenu.gameObject.SetActive(true);
        Time.timeScale = 0;
        if (GameManager.player)
        {
            GameManager.player.enabled = false;
            GameManager.gameInput.enabled = false;
        }
        
    }
}
