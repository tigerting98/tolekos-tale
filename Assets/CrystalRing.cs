using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalRing : MonoBehaviour
{
    [SerializeField] List<Crystal> crystals;
    [SerializeField] float angularvel;
    [SerializeField] int numberOfCrystals;
    [SerializeField] float maxX, maxY;
    [SerializeField] float angularvelup, amp;
    float startpos;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < numberOfCrystals; i++) {
            float startangle = 2 * Mathf.PI / numberOfCrystals * i;
            Crystal crystal = Instantiate(crystals[i%3], this.transform.position+new Vector3(maxX * Mathf.Cos(startangle),
            maxY * Mathf.Sin(startangle), 0.5f * Mathf.Sin( startangle)), Quaternion.identity, this.transform);
            crystal.startangle = startangle;
            crystal.angularvel = angularvel;
            crystal.maxx = maxX;
            crystal.maxy = maxY;
            crystal.maxz = 0.5f;
        }
        startpos = this.transform.position.y;
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, startpos + amp* Mathf.Sin(angularvelup*time), this.transform.position.z);
        time += Time.deltaTime;
    }
}
