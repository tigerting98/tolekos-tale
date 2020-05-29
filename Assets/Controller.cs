using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] PauseMenu PauseMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Time.timeScale = 0;
            if (GameManager.player) {
                GameManager.player.enabled = false;
            }
            PauseMenu.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
