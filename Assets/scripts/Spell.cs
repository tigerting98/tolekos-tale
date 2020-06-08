using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
 
    [SerializeField] SFX sfx;
    public float invulTimer;
    void Start()
    {
        sfx.PlayClip();
        Invoke("DestroyBomb", invulTimer + 2f);
    }

    void DestroyBomb() {
       
        Destroy(gameObject);
    
    }
}
