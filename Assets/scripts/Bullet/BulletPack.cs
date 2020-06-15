using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[CreateAssetMenu(menuName = "Bullet Pack")]
public class BulletPack : ScriptableObject
{
    public List<Bullet> bullets;
    public float dmg = 100;

    public Bullet GetBullet(int i) {
        Bullet bul = bullets[i];
        bul.damageDealer.damage = dmg;
        return bul;
    }

    public Bullet GetBullet(DamageType type) {
        switch (type) {
            case DamageType.Pure:
                return GetBullet(0);
            case DamageType.Water:
                return GetBullet(1);
            case DamageType.Earth:
                return GetBullet(2);
            case DamageType.Fire:
                return GetBullet(3);
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
    
    }

}
