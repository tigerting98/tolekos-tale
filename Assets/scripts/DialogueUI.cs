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

    private void Awake()
    {
        GameManager.dialogueUI = this;
        
    }
    void Start()
    {
        SetInactive();
    }
    void AcceptSprite(Image image, Sprite sprite) {
        float ratio = sprite.rect.size.y / sprite.rect.size.x;
        image.transform.localScale = new Vector2(1/ratio, 1);
        image.sprite = sprite;
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

        speakerName.text = line.name;
        content.text = line.text;
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
