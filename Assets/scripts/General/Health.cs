using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHP = 100;
    float currentHP;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    public void TakeDamage(float dmg) {
        currentHP -= dmg;
    }
    public void Heal(float dmg)

    {
        currentHP += dmg;
    }
    public float GetCurrentHP()
    {
        return currentHP;
    }

    public float GetMaxHP() {
        return maxHP;
    }

    public void SetMaxHP(float hp) {
        maxHP = hp;
    }

    public void ResetHP() {
        currentHP = maxHP;
    }

    public bool ZeroHP() {
        return currentHP <= 0;
    }

   
}
