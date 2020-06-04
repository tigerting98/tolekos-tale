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

    public List<Bullet> GetAllBullets() {
        List<Bullet> buls = new List<Bullet>();
        for (int i = 0; i < bullets.Count; i++) {
            buls.Add(GetBullet(i));
        }
        return buls;
    
    }

}
