using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//This controls the continue page
public class DeathScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI deathText;
    [SerializeField] Button continueButton, quitButton;
    void Awake()
    {
        GameManager.deathMenu = this;
        quitButton.onClick.AddListener(ReturnToMain);
        continueButton.onClick.AddListener(Revive);
        gameObject.SetActive(false);
        
    }
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(null);
        StartCoroutine(Enable());
    }


    private IEnumerator Enable()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        continueButton.GetComponent<Selectable>().Select();
    }
    // Update is called once per frame
    void Update()
    {
        deathText.text = "Number of Deaths: " + PlayerStats.deathCount.ToString();
    }
    private void ReturnToMain()
    {
        
        Revive();
        GameManager.sceneLoader.ReturnToStartPage();
 
    }
    //Remvoe all bullets and return the player back to the game
    private void Revive() {
        AudioManager.current.music.source.Play();
        GameManager.player.gameObject.SetActive(true);
        GameManager.player.transform.position = new Vector2(0, -3.5f);
        GameManager.player.health.ResetHP();
        GameManager.player.enabled = true;
        GameManager.gameInput.enabled = true;
        PlayerStats.playerCurrentHP = PlayerStats.playerMaxHP;
        GameManager.DestroyAllEnemyBulletsOnDeath();
        Time.timeScale = 1f;
        GameManager.death = false;
        gameObject.SetActive(false);
    }
   
}
