using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    float time = 0;
    public float angularvel;
    public float maxy, maxx, maxz;
    public float startangle;
    public float tiltangle;
    public float tiltAngular;
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, tiltangle);
    }

    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime; 
        float angle = tiltangle * Mathf.Cos(tiltAngular * time);
        transform.rotation = Quaternion.Euler(0, 0, angle);
        Polar xy = Polar.RotateBy(angle, new Polar(new Vector2(maxx * Mathf.Cos(angularvel * time + startangle),
            maxy * Mathf.Sin(angularvel * time + startangle))));
        
        transform.localPosition = new Vector3(xy.rect.x,
            xy.rect.y, maxz * Mathf.Sin(angularvel * time + startangle));

    }
}
