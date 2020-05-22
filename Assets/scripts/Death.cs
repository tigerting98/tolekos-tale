using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    public void die()
    {
        
        if (this.enabled)
        {
            GameObject exp = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(exp, 1f);
            Destroy(gameObject);
        }
     

    }
}
