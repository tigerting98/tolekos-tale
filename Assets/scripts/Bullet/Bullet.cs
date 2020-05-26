using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEditor;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Movement movement;
    public BulletOrientation orientation;

    public void setSpeed(Vector2 vel) {
       
        if (movement != null) {
            movement.SetStraightPath(vel);
            movement.SetStartingPoint(transform.position);
        }
    
    }
    
    

}
