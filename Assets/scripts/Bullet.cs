using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  
    [SerializeField] int damage;
    
    // Start is called before the first frame update
    void Start()
    {
      
        
    }

    public int TakeDamage() {

        return this.damage;
    }

    public void setSpeed(Vector2 vel) {
        BulletMovement movement = gameObject.GetComponent<BulletMovement>();
        if (movement != null) {
            movement.SetStraightPath(vel);
            movement.SetStartingPoint(transform.position);
        }
    
    }
   
    // Update is called once per frame
    void Update()
    {   
       
        
    }
}
