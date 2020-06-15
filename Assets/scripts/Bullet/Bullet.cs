
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Movement movement;
    public BulletOrientation orientation;
    public DamageDealer damageDealer;
    public GameObject explosion;

    private void Awake()
    {
        if (tag == "Enemy Bullet") {
            GameManager.enemyBullets.Add(gameObject.GetInstanceID(), gameObject);
        }
    }
    public void DestroyBullet() {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        
        GameManager.enemyBullets.Remove(gameObject.GetInstanceID());
    }



}
