using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private void Awake()
    {       
        GameManager.sceneLoader = this;       
    }
    public void QuitGame()
    {
        Application.Quit();

    }

    public void LoadScene(string str) {
        SceneManager.LoadScene(str);
    }

    

    public void ReturnToStartPage() {
        LoadScene("StartPage");
    }
    public void EndScene()
    {
        StartCoroutine(WaitAndLoad("EndScene"));

    }

  
    IEnumerator WaitAndLoad(string str) {
        yield return new WaitForSeconds(2f);
        LoadScene(str);
    }

   
  

}