using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHP = 1000;
    protected float currentHP;
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
    }

    public virtual void CheckDeath() {
        if (ZeroHP()&&canDie) {
            InvokeDeath();
        }
    }

    public virtual void InvokeDeath() {

        OnDeath?.Invoke();
    }

    public virtual void Heal(float dmg)

    {
        currentHP += dmg;
    }
    public virtual float GetCurrentHP()
    {
        return currentHP;
    }

   
    public virtual void IncreaseMaxHP(float hp) {
        maxHP += hp;
        currentHP += hp;
    }
    public virtual void ResetHP() {
        currentHP = maxHP;
    }

    public virtual bool ZeroHP() {
        return currentHP <= 0;
    }

    private void Update()
    {
        CheckDeath();
    }

}
