﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject explosion = default;
    [SerializeField] SFX deathSound = default;
    [SerializeField] Health hp = default;
    public bool canDie = true;
    [SerializeField] int experience = 10;
    public event Action OnDeath;
    public event Action OnLifeDepleted;

    private void Awake()
    {
        hp = hp ? hp : GetComponent<Health>();
        hp.OnDeath += Die;

    }

    private void Update()
    {
        
    }
    public virtual void Die()
    {
        OnDeath?.Invoke();
        PlayerStats.GainEXP(this.experience);
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        deathSound.PlayClip();
        Destroy(exp, 1f);

         Destroy(gameObject);
        
     

    }
}
