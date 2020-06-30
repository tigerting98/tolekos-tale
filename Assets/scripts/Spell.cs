using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{

    [SerializeField] List<Bomb> bombparts;
    [SerializeField] SFX sfx;
    public float invulTimer;
    void Start()
    {
        AudioManager.current.PlaySFX(sfx);
        Invoke("DestroyBomb", invulTimer + 2f);
    }

    void DestroyBomb() {
       
        Destroy(gameObject);
    
    }

    public void SetDamage(float dmg) {
        foreach (Bomb bomb in bombparts) {
            bomb.SetDamage(dmg);
        }
    }
}
