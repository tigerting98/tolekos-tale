﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//This class is responsible for the scene in between stages
public class BetweenStagesScript : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button nextStageButton;
    [SerializeField] Button shopButton;
    [SerializeField] MusicTrack music;
    private GameObject lastSelected;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.current.music.ChangeTrack(music);
        if (GameManager.levelDescription != null) {
            text.text = GameManager.levelDescription.levelDescription;
            nextStageButton.onClick.AddListener(
                () => GameManager.sceneLoader.LoadScene(GameManager.levelDescription.nextLevelSceneString));
        
        }
        Invoke("SetUp", 0.01f);
        
    }
    //Default selection is the shop button
    void SetUp() {
        EventSystem.current.SetSelectedGameObject(shopButton.gameObject);
        lastSelected = shopButton.gameObject;

    }
    private void Update() {
        GameObject current = EventSystem.current.currentSelectedGameObject;
        if (current != lastSelected && current != null) {
            lastSelected = current;
        }
        if (EventSystem.current.currentSelectedGameObject == null && lastSelected.activeInHierarchy) 
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                EventSystem.current.SetSelectedGameObject(lastSelected);
            }
        }
    }
    
}
