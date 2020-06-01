using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject explosion = default;
    [SerializeField] AudioClip deathSFX = default;
    [SerializeField] Health hp = default;
    [SerializeField] bool canDie = true;
    [SerializeField] int experience = 10;

  

    [Range(0,1)][SerializeField] float deathVolume = 0.7f;
    public event Action OnDeath;

    
    private void Update()
    {
        if (hp.ZeroHP() && canDie)
            {
                Die();
            }
    }
    public virtual void Die()
    {
        OnDeath?.Invoke();
        PlayerStats.GainEXP(this.experience);
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, GameManager.mainCamera.transform.position, deathVolume);
        
        Destroy(exp, 1f);

         Destroy(gameObject);
        
     

    }
}
