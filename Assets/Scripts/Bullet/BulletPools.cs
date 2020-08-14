

using System.Collections.Generic;

using UnityEngine;
//this is a class which stores all the various bullet pools
public class BulletPools : MonoBehaviour
{
    public Dictionary<string, BulletPool> bulletpools = new Dictionary<string, BulletPool>();
    [SerializeField] BulletPool bulletPool;
    private void Awake()
    {
        if (GameManager.bulletpools == null)
        {
            GameManager.bulletpools = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }
    //spawn a bullet given a position or rotation
    public Bullet SpawnBullet(Bullet bullet, Vector3 position, Quaternion rotation) {
        Bullet bul;
        if (bulletpools.ContainsKey(bullet.bulletPoolID))
        {
            BulletPool pool = bulletpools[bullet.bulletPoolID];
            if (!pool.pooledObject) {
                pool.pooledObject = bullet;
            }
            bul = pool.SpawnFromPool(position, rotation);
            
        }
        else {
            BulletPool newBulletPool = Instantiate(bulletPool);
            newBulletPool.pooledObject = bullet;
            bul = newBulletPool.SpawnFromPool(position, rotation);
            bulletpools.Add(bullet.bulletPoolID, newBulletPool);
        }
        bul.transform.position = position;
        bul.transform.rotation = rotation;
        return bul;
    }

    public Bullet SpawnBullet(Bullet bullet, Vector3 position) {
        return SpawnBullet(bullet, position, Quaternion.identity);

    }
    public Bullet SpawnBullet(Bullet bullet) {
        return SpawnBullet(bullet, bullet.transform.position);
    }

    public void PreWarmBullets(Bullet bullet, int quantity) {
        BulletPool pool;
        if (bulletpools.ContainsKey(bullet.bulletPoolID))
        {
            pool = bulletpools[bullet.bulletPoolID];
            if (!pool.pooledObject)
            {
                pool.pooledObject = bullet;
            }

        }
        else
        {
            pool = Instantiate(bulletPool);
            pool.pooledObject = bullet;
            bulletpools.Add(bullet.bulletPoolID, pool);
        }
        pool.PreWarmPool(quantity);
    }
    //place a bullet back to the pool
    public void DeactivateBullet(Bullet bullet) {
        if (bullet)
        {
            if (bulletpools.ContainsKey(bullet.bulletPoolID))
            {
                bulletpools[bullet.bulletPoolID].ReturnToPool(bullet);
            }
            else
            {
                Destroy(bullet.gameObject);
            }
        }
    }
}
