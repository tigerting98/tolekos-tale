using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using TMPro;
public class ExperienceBar : MonoBehaviour
{
    [SerializeField] Text text = default;
    [SerializeField] Slider slider = default;
    [SerializeField] Text leveltext = default;
    [SerializeField] TextMeshProUGUI updatetext;
    int i = -1;
    // Update is called once per frame
    private void Start()
    {
        UpdateExperience(0);
        PlayerStats.OnGainExp += UpdateExperience;
        PlayerStats.OnGainExp += UpdateExperienceText;
    }
    private void OnDestroy()
    {
        PlayerStats.OnGainExp -= UpdateExperience;
        PlayerStats.OnGainExp -= UpdateExperienceText;
    }
    void UpdateExperience(int exp) {
        leveltext.text = "Level : " + PlayerStats.playerLevel;
        slider.value = (float)PlayerStats.exp / (float)PlayerStats.expToLevelUp;
        text.text = "EXP: " + PlayerStats.exp + " / " + PlayerStats.expToLevelUp;
    }
    void UpdateExperienceText(int exp) {
        i = (i + 1) % 8;
        TextMeshProUGUI text = Instantiate(updatetext, (Vector2)transform.position + new Vector2(-200 + i * 70, 25f), Quaternion.identity, transform);
        text.text = "+ " + exp.ToString() + "xp";

        Movement movement = text.GetComponent<Movement>();
        movement.SetSpeed(50f, 90);

        Destroy(text.gameObject, 2.1f);
    }
}
