using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    [SerializeField] PauseMenu PauseMenu = default;
    [SerializeField] GameObject resumeButton = default;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            if (GameManager.player) {
                GameManager.player.enabled = false;
                GameManager.gameInput.enabled = false;
            }
            PauseMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(resumeButton);

        }
    }

    
}
