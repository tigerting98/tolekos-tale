using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] Health hp;
    [SerializeField] bool canDie = true;

  

    [Range(0,1)][SerializeField] float deathVolume = 0.7f;


    private void Update()
    {
        //if (hp)
       // {
            if (hp.ZeroHP() && canDie)
            {
                Die();
            }
       // }
    }
    public virtual void Die()
    {

        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, GameManager.mainCamera.transform.position, deathVolume);
        Destroy(exp, 1f);

         Destroy(gameObject);
        
     

    }
}
