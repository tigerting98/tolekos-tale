using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHP = 1000;
    float currentHP;
    public event Action OnDeath;
    public bool canDie = true;

    
    // Start is called before the first frame update
    public virtual void Start()
    {
        currentHP = maxHP;
    }

    // Update is called once per frame
    public void TakeDamage(float dmg) {
        currentHP -= dmg;
        CheckDeath();
    }

    public virtual void CheckDeath() {
        if (ZeroHP()&&canDie) {
            InvokeDeath();
        }
    }

    public void InvokeDeath() {

        OnDeath?.Invoke();
    }

    public void Heal(float dmg)

    {
        currentHP += dmg;
    }
    public float GetCurrentHP()
    {
        return currentHP;
    }

   
    public void IncreaseMaxHP(float hp) {
        maxHP += hp;
        currentHP += hp;
    }
    public void ResetHP() {
        currentHP = maxHP;
    }

    public bool ZeroHP() {
        return currentHP <= 0;
    }

   
}
