
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Movement movement;
    public BulletOrientation orientation;
    public DamageDealer damageDealer;

    public void setSpeed(Vector2 vel) {

        movement.SetStraightPath(vel);
    }
    
    

}
