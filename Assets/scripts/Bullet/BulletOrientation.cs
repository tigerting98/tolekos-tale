using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 oldPosition;
    Quaternion orientation;
    bool fix = false;
    void Start()
    {
        oldPosition = transform.position;
        orientation = Quaternion.identity;

    }

    public void SetFixedOrientation(Quaternion quad) {
        fix = true;
        orientation = quad;
        
    }

    public Quaternion FindRotation() {
        Vector2 diff = (Vector2)transform.position - oldPosition;
        return Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x));
    }
    // Update is called once per frame
    void Update()
    {

        if (!fix) {
            orientation = FindRotation();
        }
        transform.rotation = orientation;
        oldPosition = transform.position;
    }
}
