using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class EndStoryline : MonoBehaviour
{
    [SerializeField] [TextArea(3, 5)] List<string> wonDialogue;
    [SerializeField] [TextArea(3, 5)] List<string> lostDialogue;
    [SerializeField] Color goodendcolor, badendcolor;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject ending;
    [SerializeField] GameObject badendtext;
    bool next = false;
    int number = 0;
    int max = 0;
    public void Start()
    {
        max = PlayerStats.deathCount == 0 ? wonDialogue.Count : lostDialogue.Count;
        SetText(0);
    }
    public void SetText(int i) {
        text.text = PlayerStats.deathCount == 0 ? wonDialogue[i] : lostDialogue[i];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.KeypadEnter)) {
            if (next) {
                GameManager.sceneLoader.LoadScene("CreditScene");
            }
            else { 
            number++;
            if (number == max)
            {
                    text.gameObject.SetActive(false);
                    ShowEnding();
            }
            else {
                SetText(number);
            }


            }
        }
    }
    void ShowEnding() 
    {
        ending.GetComponent<TextMeshProUGUI>().text = PlayerStats.deathCount == 0 ? "Good End" : "Bad End";
        ending.GetComponent<TextMeshProUGUI>().color = PlayerStats.deathCount == 0 ? goodendcolor : badendcolor;
        ending.GetComponent<Animator>().SetTrigger("ShowEnding");
        if (PlayerStats.deathCount >0) {
            Invoke("EnableBadEndText", 0.5f);
        }
        Invoke("EnableCredits", 1f);
    }
    void EnableBadEndText() {
        badendtext.SetActive(true);
    }
    void EnableCredits() {
        next = true;
    }
}
