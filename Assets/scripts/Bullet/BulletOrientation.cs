using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 oldPosition;
    Quaternion orientation;
    bool custom = false;
    Func<float, Quaternion> orientationOverTime;
    float timer = 0;
    void Start()
    {
        oldPosition = transform.position;
        orientation = Quaternion.identity;

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
        Vector2 diff = (Vector2)transform.position - oldPosition;
        return Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x));
    }
    // Update is called once per frame
    void Update()
    {
        if (custom) {
            orientation = orientationOverTime(timer);
            timer += Time.deltaTime;
        }
        else {
            orientation = FindRotation();
        }
        
        transform.rotation = orientation;
        oldPosition = transform.position;
    }
}
