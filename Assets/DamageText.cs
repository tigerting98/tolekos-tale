using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField] Color pure, water, earth, fire;
    [SerializeField] TextMeshProUGUI text;

    public void SetColor(DamageType type) { 
        if (type == DamageType.Water) {
            text.color = water;
        }
        else if (type == DamageType.Earth)
        {
            text.color = earth;
        }
        else if (type == DamageType.Fire)
        {
            text.color = fire;
        }
        else {
            text.color = pure;
        }

    }
    public void SetText(float dmg) {
        if (dmg > 0)
        {
            text.text = (int)(-1 * dmg) + " HP";
        }
        else {
            text.text = "+"+(int)(-1 * dmg) + " HP";
        }
        
    }
}
