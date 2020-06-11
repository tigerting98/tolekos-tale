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

    // Update is called once per frame
    
}
