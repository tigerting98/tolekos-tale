using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    [SerializeField] Text text = default;
    [SerializeField] Slider slider = default;
    [SerializeField] Text leveltext = default;


    // Update is called once per frame
    private void Start()
    {
        UpdateExperience();
        PlayerStats.OnGainExp += UpdateExperience;
    }
    private void OnDestroy()
    {
        PlayerStats.OnGainExp -= UpdateExperience;
    }
    void UpdateExperience() {
        leveltext.text = "Level : " + PlayerStats.playerLevel;
        slider.value = (float)PlayerStats.exp / (float)PlayerStats.expToLevelUp;
        text.text = "EXP: " + PlayerStats.exp + " / " + PlayerStats.expToLevelUp;
    }
}
