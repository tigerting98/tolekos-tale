using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{



    public void setSpeed(Vector2 vel) {
       Movement movement = gameObject.GetComponent<Movement>();
        if (movement != null) {
            movement.SetStraightPath(vel);
            movement.SetStartingPoint(transform.position);
        }
    
    }
    
    

}
