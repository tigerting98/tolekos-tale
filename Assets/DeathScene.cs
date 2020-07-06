using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
        GameManager.player = null; 
        GameManager.sceneLoader.ReturnToStartPage();
 
    }
    private void Revive() {
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
