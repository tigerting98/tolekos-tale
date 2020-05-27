using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] Health hp;
    [SerializeField] bool canDie = true;
    [SerializeField] int experience = 10;

  

    [Range(0,1)][SerializeField] float deathVolume = 0.7f;


    private void Update()
    {
        if (hp.ZeroHP() && canDie)
            {
                Die();
            }
    }
    public virtual void Die()
    {
        PlayerStats.GainEXP(this.experience);
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, GameManager.mainCamera.transform.position, deathVolume);
        
        Destroy(exp, 1f);

         Destroy(gameObject);
        
     

    }
}
