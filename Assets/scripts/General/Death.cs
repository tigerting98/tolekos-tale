using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] AudioClip deathSFX;
    Camera cam;
    [Range(0,1)][SerializeField] float deathVolume = 0.7f;
    private void Start()
    {
        cam = Camera.main;
    }
    public void die()
    {

        
        GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathSFX, cam.transform.position, deathVolume);
        Destroy(exp, 1f);

         Destroy(gameObject);
        
     

    }
}
