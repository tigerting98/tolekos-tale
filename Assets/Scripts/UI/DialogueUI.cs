using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//this controls the UI that displays the dialogue
public class DialogueUI : MonoBehaviour
{
    [SerializeField] Image leftImage, rightImage;
    [SerializeField] TextMeshProUGUI speakerName, content;
    public bool typing = false;
    int currentcharacter=0, textlength=0;
    Line line;

    private void Awake()
    {
        GameManager.dialogueUI = this;
        
    }
    void Start()
    {
        SetInactive();
    }
    void AcceptSprite(Image image, Sprite sprite) {
        try
        {
            float ratio = sprite.rect.size.y / sprite.rect.size.x;
            image.transform.localScale = new Vector2(1 / ratio, 1);
            image.sprite = sprite;
        }
        catch (Exception ex) {
            Debug.Log(ex);
        }
    }
    public void ReceiveNewLine(Line line) {
        if (line.left)
        {
            leftImage.gameObject.SetActive(true);
            rightImage.gameObject.SetActive(false);
            AcceptSprite(leftImage, line.isPlayer ? GameManager.gameData.playerDialogueSprite : line.speaker);

        }
        else {
            leftImage.gameObject.SetActive(false);
            rightImage.gameObject.SetActive(true);
            AcceptSprite(rightImage, line.isPlayer ? GameManager.gameData.playerDialogueSprite : line.speaker);
        }
        this.line = line;
        speakerName.text = line.name;
        typing = true;
        currentcharacter = 0;
        textlength = line.text.Length;
        StartCoroutine(StartTyping());
    }
    public void FinishLine() {
        currentcharacter = textlength;
        content.text = line.text;
        typing = false;
        
    }
    IEnumerator StartTyping() {
        while (typing)
        {
            if (currentcharacter < textlength)
            {
                currentcharacter++;
                content.text = line.text.Substring(0, currentcharacter);
            }
            if (currentcharacter == textlength) {
                typing = false;
            }
            yield return new WaitForSeconds(0.02f);
        }

    }
    public void SetActive() {
        gameObject.SetActive(true);
    }
    public void SetInactive() {

        leftImage.gameObject.SetActive(false);
        rightImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }


}
