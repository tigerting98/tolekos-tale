
using System;
using System.Collections.Generic;
using UnityEngine;
//This class is responsible for the movement behavior of a game object
public enum MovementMode { Position, Velocity, Acceleration, Homing }
public class Movement : MonoBehaviour
{

    public Func<float, Vector2> graph = time => new Vector2(0,0);
    [HideInInspector]public Vector2 currentVelocity;
    GameObject target;
    float homingSpeed;
    public float time = 0;
    bool moving = true;
    MovementMode mode = MovementMode.Velocity;
    public float destroyBoundary = 4.5f;
    public List<ActionTrigger<Movement>> triggers = new List<ActionTrigger<Movement>>();

    // Start is called before the first frame update
 

    public void SetSpeed(Vector2 vel) {
        moving = true;
        mode = MovementMode.Velocity;
        graph = time => vel;
        ResetTimer();
    }

    public void SetSpeed(float vel, float angle) {
        SetSpeed(Quaternion.Euler(0, 0, angle) * new Vector2(vel, 0));
    }

    //takes a velocity and acceleration graph
    public void SetAcceleration(Vector2 initialVel, Func<float, Vector2> graph) {
        mode = MovementMode.Acceleration;
        currentVelocity = initialVel;
        this.graph = graph;
        ResetTimer();

    }

    //accelerates towards a location, until its speed reaches the endspeed
    public void AccelerateTowards(float acceleration, Vector2 end, float endSpeed) {
        Vector2 direction = (end - (Vector2)transform.position).normalized;
        SetAcceleration(new Vector2(0, 0), t => t < endSpeed / acceleration ? direction * acceleration : new Vector2(0, 0));
    }
    //homes a target at a certain speed
    public void Homing(GameObject tar, float spd) {
        mode = MovementMode.Homing;
        target = tar;
        homingSpeed = spd;
        currentVelocity = ((Vector2)(target.transform.position - transform.position)).normalized * homingSpeed;
    
    }

    public void ResetTimer() {
        time = 0;
    }

    //move to a location and then stopping afterwards
    public float MoveTo(Vector2 end, float speed) {
        StartMoving();
        CancelInvoke();
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

    //changes speed after some amount of time
    public void SetVelocityAndChangeAfter(Vector2 directionVector, float initialSpeed, float finalSpeed, float time) {
        mode = MovementMode.Velocity;
        ResetTimer();
        Vector2 normalized = directionVector.normalized;
        graph = t => t < time ? initialSpeed * normalized : finalSpeed * directionVector; 
    }

    //moves for a while then stop
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
   
 
    public void RotateTrajectory(float angle) {
        graph = RotatePath(angle, graph);

    }
    public void SetPolarPath(Func<float, Polar> traj) {
        mode = MovementMode.Position;
        graph = t => traj(t).rect;
    }
    public static Func<float, Vector2> RotatePath(float angle, Func<float, Vector2> oldTraj) {
        return t => Quaternion.Euler(0, 0, angle) * oldTraj(t);       
    }

    public static Func<float, Vector2> ReflectPathAboutX(Func<float, Vector2> oldTraj) {
        return t =>
        {
            Vector2 old = oldTraj(t);
            return new Vector2(old.x, -old.y);
        };
        
    
    }
    public static Func<float, Vector2> ReflectPathAboutY(Func<float, Vector2> oldTraj)
    {
        return t =>
        {
            Vector2 old = oldTraj(t);
            return new Vector2(-old.x, old.y);
        };


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
                if (!target || !target.activeInHierarchy)
                {
                    SetSpeed(currentVelocity);
                }
                else {
                    currentVelocity = ((Vector2)(target.transform.position - transform.position)).normalized * homingSpeed;
                
                }
            
            }

            

            
            time += Time.deltaTime;

            transform.position += (Vector3)currentVelocity * Time.deltaTime;

            CheckTriggers();
         
            OutOfBound(); 
           
        }
       

    }

    //check for all possible triggers associated with this component
    private void CheckTriggers() {
        foreach (ActionTrigger<Movement> actionTrigger in triggers) {
            actionTrigger.CheckTrigger(this);
        }
    }
    private void OnDisable()
    {
        Reset();
    }


    public void ResetTriggers() {
        triggers = new List<ActionTrigger<Movement>>();
    }
    //reset to default values
    public void Reset()
    {
        graph = time => new Vector2(0, 0);
        currentVelocity = new Vector2(0,0);
        target = null;
        homingSpeed = 0;
        ResetTimer();
        moving = true;
        CancelInvoke();
        mode = MovementMode.Velocity;
        destroyBoundary = 4.5f;
        ResetTriggers();
    }
    //destory the object or deactivate it if its pooled
    public void RemoveObject() {
        IPooledObject obj = gameObject.GetComponent<IPooledObject>();
        if (obj != null)
        {
            obj.Deactivate();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //check if its within bounds
    public void OutOfBound()
    {


        if (!Functions.WithinBounds(transform.position,destroyBoundary))
        {

            RemoveObject();
        }
    }



}
