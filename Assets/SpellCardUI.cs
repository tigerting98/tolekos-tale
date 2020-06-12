﻿using System.Collections;
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
        image.sprite = sprite;
    }

    public void SetText(string text) {
        spellCardText.text = text;
    }

    public void SetSFX(SFX sfx) {
        spellCardSFX = sfx;
    }

    public void PlaySFX() {
        spellCardSFX.PlayClip();
    }
}
