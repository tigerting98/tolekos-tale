using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPattern : MonoBehaviour
{
    public static void EarthUnfocusedMode(Bullet bullet, Player player)
    {
        float spreadOut = 10;
        float spreadIn = 5;
        float dmg = bullet.damageDealer.damage;
         Patterns.ShootStraight(bullet, dmg, player.transform.position, 90, player.bulletSpeed,null);
         Patterns.ShootStraight(bullet, dmg, player.transform.position, 90 + spreadOut, player.bulletSpeed,null);
         Patterns.ShootStraight(bullet, dmg, player.transform.position, 90 - spreadOut, player.bulletSpeed,null);
        Patterns.ShootStraight(bullet, dmg, player.transform.position, 90 + spreadIn, player.bulletSpeed,null);
        Patterns.ShootStraight(bullet, dmg, player.transform.position, 90 - spreadIn, player.bulletSpeed,null);


    }
    public static void EarthFocusedMode(Bullet bullet, Player player)
    {
      
         Patterns.ShootStraight(bullet, bullet.damageDealer.damage, player.transform.position, 90, player.bulletSpeed/1.5f,null);
  



    }
    public static void WaterUnfocusedMode(Bullet bullet, Player player)
    {
        Vector3 spreadOut =  new Vector2(0.4f, 0);
        Vector3 spreadIn = new Vector2(0.2f, 0);
        float dmg = bullet.damageDealer.damage;
        Patterns.ShootStraight(bullet, dmg, player.transform.position, 90, player.bulletSpeed,null);
        Patterns.ShootStraight(bullet, dmg, player.transform.position + spreadOut, 90, player.bulletSpeed,null);
        Patterns.ShootStraight(bullet, dmg, player.transform.position- spreadOut, 90, player.bulletSpeed,null);
        Patterns.ShootStraight(bullet, dmg, player.transform.position + spreadIn, 90, player.bulletSpeed,null);
        Patterns.ShootStraight(bullet, dmg, player.transform.position - spreadIn, 90, player.bulletSpeed,null);


    }
    public static void FireMode(Bullet bullet, Player player)
    {
        Patterns.ShootStraight(bullet, bullet.damageDealer.damage, player.transform.position, 90, player.bulletSpeed,null);
    }

    public static void WaterFocusedMode(Bullet bullet, Player player) {
        Vector3 spread = new Vector2(0.5f, 0);
        float dmg = bullet.damageDealer.damage;
        Patterns.ShootStraight(bullet, dmg, player.transform.position, 90, player.bulletSpeed,null);
        Patterns.HomeNearestEnemy(bullet, dmg, player.transform.position - spread, new Vector2(0, player.bulletSpeed),null);
        Patterns.HomeNearestEnemy(bullet, dmg, player.transform.position + spread, new Vector2(0, player.bulletSpeed),null);
        
    } 

}
