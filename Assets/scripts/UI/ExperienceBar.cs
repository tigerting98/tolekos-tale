using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Slider slider;
    [SerializeField] Text leveltext;


    // Update is called once per frame
    void Update()
    {
        leveltext.text = "Level : " + PlayerStats.playerLevel;
        slider.value = (float)PlayerStats.exp / (float)PlayerStats.expToLevelUp;
        text.text = "EXP: " + PlayerStats.exp + " / " + PlayerStats.expToLevelUp; 
    }
}
