using System;
using System.Transactions;
using UnityEngine;

public class Movement : MonoBehaviour
{


    Vector2 startingPoint;
    Func<float, Vector2> trajectory = time => new Vector2(0,0);
    float time = 0;
    bool destroyWhenOut = true;
    bool moving = true;



    // Start is called before the first frame update
    public void Start()
    {
        startingPoint = transform.position;
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

    public float MoveTo(Vector2 end, float speed) {
        return MoveToFrom(transform.position, end, speed);
        
    }

    public float MoveToFrom(Vector2 start, Vector2 end, float speed) {
        StartMoving();
        Vector2 diff = end - start;
        float timeTaken = diff.magnitude / speed;
        SetStartingPoint(start);
        SetStraightPath(diff / timeTaken);
        StopMovingAfter(timeTaken);

        return timeTaken;
    }
    public bool IsMoving() {
        return moving;
    }
    public void StartMoving() {
        moving = true;
    }

    public void StopMoving() {
        moving = false;
    }
    public void StopMovingAfter(float sec) {
        Invoke("StopMoving", sec);
        
    }
    public void StartMovingAfter(float sec) {
        Invoke("StartMoving", sec);
    }
    public void SetCustomPath(Func<float, Vector2> traj) {
        ResetTimer();

        trajectory = traj;
       
    }
    public void SetDestroyWhenOut(bool bl) {
        destroyWhenOut = bl;
    }

    public void RotateTrajectory(float angle) {
        trajectory = RotatePath(angle, trajectory);
      
    }
    public void SetPolarPath(Func<float, Polar> traj) {
        trajectory = t => traj(t).rect;
    }
    public Func<float, Vector2> RotatePath(float angle, Func<float, Vector2> oldTraj) {
        return t => Quaternion.Euler(0, 0, angle) * oldTraj(t);       
    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            time += Time.deltaTime;
        }
        transform.position = trajectory(time) +startingPoint;

        if (destroyWhenOut)
        { OutOfBound(); }



    }
    public void OutOfBound()
    {


        if (transform.position.x < -6 || transform.position.x > 6 || transform.position.y < -6 || transform.position.y > 6)
        {
            Destroy(gameObject);
        }
    }


}
