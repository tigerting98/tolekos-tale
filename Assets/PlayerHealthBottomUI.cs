using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBottomUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Image image;
    void Update()
    {
        if (GameManager.player)
        {
            Health hp = GameManager.player.health;
            float percent = Mathf.Clamp(hp.GetCurrentHP() / hp.maxHP, 0, 1);
            image.fillAmount = percent;
        }
        else {
            image.fillAmount = 0;
        }
    }
}
