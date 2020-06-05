using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    [HideInInspector]public int numberOfLifes = 1;
    [HideInInspector] public int numberOfLifesLeft = 1;
    public List<float> listOfHp;

    public event Action OnLifeDepleted;
    private void Awake()
    {
        canDie = false;
        maxHP = listOfHp[0];
        numberOfLifes = listOfHp.Count;
        numberOfLifesLeft = listOfHp.Count;
    }

    public override void CheckDeath() {
        if (ZeroHP())
        {
            OnLifeDepleted?.Invoke();
            numberOfLifesLeft--;
            if (numberOfLifesLeft == 0)
            {
                InvokeDeath();
            }
            else {

                maxHP = listOfHp[numberOfLifes - numberOfLifesLeft];
                ResetHP();
            }
        }
    }

}
