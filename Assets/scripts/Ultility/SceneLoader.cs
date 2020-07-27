using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//Global class responsible for changing scenes
public class SceneLoader : MonoBehaviour
{
    
    private void Awake()
    {
        if (GameManager.sceneLoader == null)
        {
            GameManager.sceneLoader = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else {
            Destroy(this.gameObject);
        }
        
    }

    public void QuitGame()
    {
        Application.Quit();

    }

    public void LoadScene(string str) {
        SceneManager.LoadScene(str);
    }
    public void LoadStartGame(int i) {
        GameManager.difficultyLevel = (Difficulty)i;
        LoadShopScene(GameManager.gameData.levels[0]);
    }
    public void LoadShopScene(LevelDescription levelDescription) {
        GameManager.levelDescription = levelDescription;
        SceneManager.LoadScene("Shop Scene");
    
    }

    public void ReturnToStartPage() {
        LoadScene("StartPage");
    }




   
  

}