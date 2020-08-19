using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DifficultyUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI stagetxt;
    void Start()
    {
        TextMeshProUGUI txt = GetComponent<TextMeshProUGUI>();
        txt.text = Functions.GetDifficultyString(GameManager.difficultyLevel);
        txt.color = Functions.GetDifficultyColor(GameManager.difficultyLevel);
        stagetxt.text = "Stage " + GameManager.stageLevel.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
