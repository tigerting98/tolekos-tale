using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet Pack")]
public class BulletPack : ElementPack<Bullet>
{
    


    /*public Bullet GetBullet(int i) {
        Bullet bul = bullets[i];
        return bul;
    }


    public Bullet GetBullet(DamageType type) {
        switch (type) {
            case DamageType.Pure:
                return GetBullet(3);
            case DamageType.Water:
                return GetBullet(0);
            case DamageType.Earth:
                return GetBullet(1);
            case DamageType.Fire:
                return GetBullet(2);
            default:
                return GetBullet(0);
        }
    }



    public List<Bullet> GetAllBullets() {
        List<Bullet> buls = new List<Bullet>();
        for (int i = 0; i < bullets.Count; i++) {
            buls.Add(GetBullet(i));
        }
        return buls;
    
    }*/

}
