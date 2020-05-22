using System;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;

using System.Runtime.InteropServices.WindowsRuntime;

using System.Threading;
using UnityEditor.UIElements;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    Camera cam;

    Vector2 startingPoint;
    Func<float, Vector2> trajectory;
  
    float rotationAngle = 0;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        time = 0;
      
    }


    public void SetStraightPath(Vector2 vel) {
        ResetTimer();
        trajectory = time => time * vel;


    }

    public void SetStartingPoint(Vector2 initialPos) {
        ResetTimer();
        startingPoint = initialPos;

    }
    public void ResetTimer() {
        time = 0;
    }

    public void RemoveRotation() {
        rotationAngle = 0;
       
    }
   
    public void SetCustomPath(Func<float, Vector2> traj) {
        ResetTimer();

        trajectory = traj;
       
    }

    public void RotateTrajectory(float angle) {
        rotationAngle = angle;
      
    }
    // Update is called once per frame
    void Update()
    {

        time += Time.deltaTime;
        transform.position = Quaternion.Euler(0,0,rotationAngle) * (Vector3)(trajectory(time)) + (Vector3)(startingPoint);

   

        OutOfBound();

    }

    void OutOfBound() {

        UnityEngine.Vector3 pos = cam.WorldToViewportPoint(transform.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
