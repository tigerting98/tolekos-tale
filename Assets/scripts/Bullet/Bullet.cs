
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Movement movement;
    public BulletOrientation orientation;
    public DamageDealer damageDealer;

    public void setSpeed(Vector2 vel) {
       
        if (movement != null) {
            movement.SetStraightPath(vel);
            movement.SetStartingPoint(transform.position);
        }
    
    }
    
    

}
