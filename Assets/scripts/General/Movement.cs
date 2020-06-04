using System;
using System.Transactions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
public enum MovementMode { Position, Velocity, Acceleration, Homing }
public class Movement : MonoBehaviour
{
 

    Func<float, Vector2> graph = time => new Vector2(0,0);
    [HideInInspector]public Vector2 currentVelocity;
    GameObject target;
    float homingSpeed;
    float time = 0;
    bool destroyWhenOut = true;
    bool moving = true;
    MovementMode mode = MovementMode.Velocity;


    // Start is called before the first frame update
 

    public void SetSpeed(Vector2 vel) {
        mode = MovementMode.Velocity;
        graph = time => vel;
        ResetTimer();
    }
    public void SetAcceleration(Vector2 initialVel, Func<float, Vector2> graph) {
        mode = MovementMode.Acceleration;
        currentVelocity = initialVel;
        this.graph = graph;
        ResetTimer();

    }

    public void AccelerateTowards(float acceleration, Vector2 end, float endSpeed) {
        Vector2 direction = (end - (Vector2)transform.position).normalized;
        SetAcceleration(new Vector2(0, 0), t => t < endSpeed / acceleration ? direction * acceleration : new Vector2(0, 0));
    }
    public void Homing(GameObject tar, float spd) {
        mode = MovementMode.Homing;
        target = tar;
        homingSpeed = spd;
        currentVelocity = ((Vector2)(target.transform.position - transform.position)).normalized * homingSpeed;
    
    }

    public void ResetTimer() {
        time = 0;
    }

    public float MoveTo(Vector2 end, float speed) {
        StartMoving();
        Vector2 diff = end - (Vector2)transform.position;
        float timeTaken = diff.magnitude / speed;
        SetSpeed(diff / timeTaken);
        StopMovingAfter(timeTaken);

        return timeTaken;

    }

    public void SetCustomGraph(Func<float, Vector2> traj, MovementMode movementMode) {
        mode = movementMode;
        graph = traj;
    
    }

    public void SetVelocityAndChangeAfter(Vector2 directionVector, float initialSpeed, float finalSpeed, float time) {
        mode = MovementMode.Velocity;
        ResetTimer();
        Vector2 normalized = directionVector.normalized;
        graph = t => t < time ? initialSpeed * normalized : finalSpeed * directionVector; 
    }

    public void MoveAndStopAfter(Vector2 velocity, float time) {
        mode = MovementMode.Velocity;
        ResetTimer();
        graph = t => velocity;
        StopMovingAfter(time);
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
        mode = MovementMode.Position;
        graph = traj;
       
    }
    public void SetDestroyWhenOut(bool bl) {
        destroyWhenOut = bl;
    }

    public void RotateTrajectory(float angle) {
        graph = RotatePath(angle, graph);

    }
    public void SetPolarPath(Func<float, Polar> traj) {
        mode = MovementMode.Position;
        graph = t => traj(t).rect;
    }
    public Func<float, Vector2> RotatePath(float angle, Func<float, Vector2> oldTraj) {
        return t => Quaternion.Euler(0, 0, angle) * oldTraj(t);       
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (moving)
        {
            if (mode == MovementMode.Position)
            {
                currentVelocity = (graph(time + Time.deltaTime) - graph(time)) / Time.deltaTime;

            }
            else if (mode == MovementMode.Velocity)
            {
                currentVelocity = graph(time);

            }
            else if (mode == MovementMode.Acceleration){
                currentVelocity += graph(time) * Time.deltaTime;
            
            }
            else{
                if (!target)
                {
                    SetSpeed(currentVelocity);
                }
                else {
                    currentVelocity = ((Vector2)(target.transform.position - transform.position)).normalized * homingSpeed;
                
                }
            
            }

            
            time += Time.deltaTime;
            transform.position += (Vector3)currentVelocity * Time.deltaTime;
            if (destroyWhenOut)
            { OutOfBound(); }
        }
        

    }

    

    public void OutOfBound()
    {


        if (transform.position.x < -4.5 || transform.position.x > 4.5 || transform.position.y < -4.5 || transform.position.y > 4.5)
        {
            Destroy(gameObject);
        }
    }


}
