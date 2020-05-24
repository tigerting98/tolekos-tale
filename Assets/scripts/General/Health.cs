using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHP = 100;
    int currentHP;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    public void TakeDamage(int dmg) {
        currentHP -= dmg;
    }
    public void Heal(int dmg)

    {
        currentHP += dmg;
    }
    public int GetCurrentHP()
    {
        return currentHP;
    }

    public int GetMaxHP() {
        return maxHP;
    }

    public void SetMaxHP(int hp) {
        maxHP = hp;
    }

    public void ResetHP(int hp) {
        currentHP = maxHP;
    }

    public bool ZeroHP() {
        return currentHP <= 0;
    }

   
}
