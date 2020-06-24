using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Windows.Speech;
public enum OrientationMode {velocity, position }
[RequireComponent(typeof(Movement))]

public class BulletOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Movement movement;
    public Quaternion orientation;
    bool custom = false;
    public bool absolute = false;
    public OrientationMode mode = OrientationMode.position;
    Vector2 prev = new Vector2(0, 0);
    Func<float, Quaternion> orientationOverTime;
    Func<float, float> angularVelOverTime;
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
        mode = OrientationMode.position;
    }

    public void SetCustomAngularVel(Func<float, float> fun) {
        custom = true;
        timer = 0;
        angularVelOverTime = fun;
        mode = OrientationMode.velocity;
    }
    public void Reset()
    {
        custom = false;
        absolute = false;
        prev = new Vector2(0, 0);
        orientationOverTime = null;
        timer = 0;
        orientation = Quaternion.identity;
        angularVelOverTime = null;
        mode = OrientationMode.position;
    }


    public Quaternion FindRotation() {
        Vector2 diff;
        if (absolute)
        {
            diff = (Vector2)transform.position - prev;
        }
        else
        {
             diff = movement.currentVelocity;
        }
        return Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x));
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
            if (custom)
            {
                if (mode == OrientationMode.position)
                {
                    orientation = orientationOverTime(timer);
                    timer += Time.deltaTime;
                transform.rotation = orientation;
        
            }
             else {
                transform.Rotate(0, 0, angularVelOverTime(timer) * Time.deltaTime);
                timer += Time.deltaTime;
            }
            }
            else
            {
                orientation = FindRotation();
            transform.rotation = orientation;
          
        }
        prev = transform.position;
    }
}
