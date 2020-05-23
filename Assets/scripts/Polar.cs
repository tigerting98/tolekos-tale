using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Polar 
{
    public float r;
    public float rads;
    public Vector2 rect;
    public Polar(Vector2 vect) {
        this.r = vect.magnitude;
        this.rads = Mathf.Atan2(vect.y, vect.x);
        rect = vect;
       
    }

    public Polar(float r, float rads) {
        this.r = r;
        this.rads = rads;
        rect = r * new Vector2(Mathf.Cos(rads), Mathf.Sin(rads));


    }

}
