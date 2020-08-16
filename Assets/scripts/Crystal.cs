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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(maxx * Mathf.Cos(angularvel * time + startangle),
            maxy * Mathf.Sin(angularvel * time + startangle), maxz * Mathf.Sin(angularvel * time + startangle));
        time += Time.deltaTime;
    }
}
