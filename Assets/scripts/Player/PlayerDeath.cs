using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Controls the player's death
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
        AudioManager.current.music.source.Pause();
        Time.timeScale = 0;
        if (GameManager.player)
        {
            GameManager.player.enabled = false;
            GameManager.gameInput.enabled = false;
        }
        
    }
}
