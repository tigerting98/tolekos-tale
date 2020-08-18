using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Ultility class to deal with circular movement
public class Polar 
{
    public float r;
    public float degree;
    public Vector2 rect;
    public Polar(Vector2 vect) {
        this.r = vect.magnitude;
        this.degree = Mathf.Atan2(vect.y, vect.x) * 180/(float)Math.PI;
        rect = vect;
       
    }
    public static Polar RotateBy(float movedegree, Polar pol) {
        return new Polar(pol.r, pol.degree + movedegree);
    }
    public Polar(float r, float degree) {
        this.r = r;
        this.degree = degree;
        rect = r * new Vector2(Mathf.Cos(degree* Mathf.Deg2Rad), Mathf.Sin(degree*Mathf.Deg2Rad));


    }

}
