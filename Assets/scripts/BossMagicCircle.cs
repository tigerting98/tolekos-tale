using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This controls the beahvior of the v boss magic circle around the boss
public class BossMagicCircle : MonoBehaviour
{
    // Start is called before the first frame update
    float time = 0;
    float angle = 0;
    float size = 1;
    public float originalsize =1;
    public float orientationspeed = 180f;
    public float expandtime = 0.3f;
    public float expandamp = 0.5f;
    public float expandfreq = 0.75f;
    void Start()
    {
        time = 0;
        size = 0;
        transform.localScale = new Vector2(0, 0);
    }
    public void ResetTime() {
        time = 0;
        size = 0;
        transform.localScale = new Vector2(0, 0);
    }
    private void OnDisable()
    {
        ResetTime();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        time += Time.deltaTime;
        angle += orientationspeed * Time.deltaTime;
        angle = Functions.modulo(angle, 360f);
        if (time < expandtime)
        {
            size = originalsize / expandtime * time;
        }
        else {
            size = originalsize + expandamp * Mathf.Sin(expandfreq * 2 * (float)Math.PI * (time - expandtime));
        }
        transform.localScale = new Vector2(size, size);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
