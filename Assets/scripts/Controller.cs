using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// This is to open the pause menu
public class Controller : MonoBehaviour
{
   
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !GameManager.death)
        {
            Time.timeScale = 0;
            if (GameManager.player)
            {
                GameManager.player.enabled = false;
                GameManager.gameInput.enabled = false;
            }
            AudioManager.current.music.source.Pause();
            GameManager.pauseMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);


        }
    }

    
}
