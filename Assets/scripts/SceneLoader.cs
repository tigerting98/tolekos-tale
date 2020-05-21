﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{   
    
    public void QuitGame()
    {
        Application.Quit();

    }

    public void StartGame(string str) {
        SceneManager.LoadScene(str);
    }
}