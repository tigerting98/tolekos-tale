using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    public int numberOfLifes = 1;
    int currentLife = 1;
    public List<float> listOfHp;

    public event Action OnLifeDepleted;
    private void Awake()
    {
        canDie = false;
        maxHP = listOfHp[0];
    }

    public override void CheckDeath() {
        if (ZeroHP())
        {
            OnLifeDepleted?.Invoke();
            if (currentLife == numberOfLifes)
            {
                InvokeDeath();
            }
            else {
                currentLife++;
                
                maxHP = listOfHp[currentLife - 1];
                ResetHP();
            }
        }
    }

}
