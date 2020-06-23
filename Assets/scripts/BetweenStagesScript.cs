using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BetweenStagesScript : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] Button nextStageButton;
    [SerializeField] Button shopButton;
    private GameObject lastSelected;

    private void Awake() {
        EventSystem.current.SetSelectedGameObject(shopButton.gameObject);
        lastSelected = shopButton.gameObject;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.levelDescription != null) {
            background.sprite = GameManager.levelDescription.backgroundImage;
            text.text = GameManager.levelDescription.levelDescription;
            nextStageButton.onClick.AddListener(
                () => GameManager.sceneLoader.LoadScene(GameManager.levelDescription.nextLevelSceneString));
        
        }

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
