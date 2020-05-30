using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
[RequireComponent(typeof(Movement))]
public class BulletOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Movement movement;
    Quaternion orientation;
    bool custom = false;
    Func<float, Quaternion> orientationOverTime;
    float timer = 0;
    void Start()
    {
        if (!movement) {
            movement = GetComponent<Movement>();
        }

    }

    public void SetFixedOrientation(Quaternion quad) {
        custom = true;
        orientationOverTime = t => quad;
        
    }

    public void SetCustomOrientaion(Func<float, Quaternion> fun) {
        custom = true;
        timer = 0;
        orientationOverTime = fun;
    }



    public Quaternion FindRotation() {
        Vector2 diff = movement.currentVelocity;
        return Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
            if (custom)
            {
                orientation = orientationOverTime(timer);
                timer += Time.deltaTime;
            }
            else
            {
                orientation = FindRotation();
            }

            transform.rotation = orientation;
        
    }
}
