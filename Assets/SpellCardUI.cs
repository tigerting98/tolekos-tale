using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellCardUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI spellCardText;

    public void SetImage(Sprite sprite) {
        image.sprite = sprite;
    }

    public void SetText(string text) {
        spellCardText.text = text;
    }
}
