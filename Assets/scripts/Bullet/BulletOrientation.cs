using System;

using UnityEngine;
public enum OrientationMode {velocity, position }
[RequireComponent(typeof(Movement))]

public class BulletOrientation : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Movement movement;
    public float angle = 0;
    public bool custom = false;
    public OrientationMode mode = OrientationMode.position;
     Func<float, float> orientationOverTime;
    Func<float, float> angularVelOverTime;
    float timer = 0;
    void Start()
    {
        if (!movement) {
            movement = GetComponent<Movement>();
        }
        



    }
  
    private void OnEnable()
    {
        angle = transform.rotation.z;
    }
    private void OnDisable()
    {
        Reset();
    }

    public void StartRotating(float angularVel, float startAngle) {
        angle = startAngle;
        SetCustomAngularVel(t => angularVel);
    }
    public void StartRotating(float angularVel)
    {
        SetCustomAngularVel(t => angularVel);
    }
    public void SetFixedOrientation(float angle) {
        SetCustomOrientation(t => angle);
    }


    public void SetCustomOrientation(Func<float, float> fun) {
        custom = true;
        timer = 0;
        orientationOverTime = fun;
        mode = OrientationMode.position;
    }

    public void SetCustomAngularVel(Func<float, float> fun) {
        custom = true;
        timer = 0;
        angularVelOverTime = fun;
        mode = OrientationMode.velocity;
    }
    public void Reset()
    {
        custom = false;
        orientationOverTime = null;
        timer = 0;
        angle = 0;
        angularVelOverTime = null;
        mode = OrientationMode.position;
    }


    public float FindRotation() {
        
        return  Mathf.Rad2Deg * Mathf.Atan2(movement.currentVelocity.y, movement.currentVelocity.x);
    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (custom)
        {
            if (mode == OrientationMode.position)
            {
                angle = orientationOverTime(timer);
                timer += Time.deltaTime;
                

            }
            else
            {
                angle += angularVelOverTime(timer)*Time.deltaTime;
                timer += Time.deltaTime;
            }
        }
        else
        {
            angle = FindRotation();
            

        }
        if (transform.parent) {
            angle +=transform.parent.rotation.eulerAngles.z;
            }
        angle = Functions.modulo(angle, 360);
        transform.rotation = Quaternion.Euler(0,0,angle);
    }


}
