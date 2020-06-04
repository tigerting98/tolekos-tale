using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrystal : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Sprite waterCrystal, earthCrystal, fireCrystal;
    [SerializeField] SpriteRenderer spriteRenderer;
    void Start()
    {
        player.ChangeMode += ChangeColor;
    }

    // Update is called once per frame
    private void OnDestroy()
    {
        player.ChangeMode -= ChangeColor;
    }

    void ChangeColor() {
        if (player.mode == DamageType.Water) {
            spriteRenderer.sprite = waterCrystal;
        }
        else if (player.mode == DamageType.Earth)
        {
            spriteRenderer.sprite = earthCrystal;
        }
        else
        {
            spriteRenderer.sprite = fireCrystal;
        }

    }
}
