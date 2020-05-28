using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.UIElements;
using UnityEngine;

public class something : MonoBehaviour
{
    float angle = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        angle += 720 * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
