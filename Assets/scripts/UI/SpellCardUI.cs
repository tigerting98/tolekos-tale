using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem.LowLevel;

public class SpellCardUI : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI spellCardText;
    [SerializeField] SFX spellCardSFX;

    public void SetImage(Sprite sprite) {
        float ratio = sprite.rect.size.y / sprite.rect.size.x;
        image.transform.localScale = new Vector2(1 / ratio, 1);
        image.sprite = sprite;
    }

    public void SetText(string text) {
        spellCardText.text = text;
    }

    public void SetSFX(SFX sfx) {
        spellCardSFX = sfx;
    }

    public void PlaySFX() {
        AudioManager.current.PlaySFX(spellCardSFX);
        
    }
}
