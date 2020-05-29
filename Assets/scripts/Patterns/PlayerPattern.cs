using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPattern : MonoBehaviour
{
    public static void EarthMode(Bullet bullet, Player player)
    {
        float spread = player.isFocus ? 5 : 15;
         Patterns.ShootStraight(bullet, player.transform.position, 90, player.bulletSpeed);
         Patterns.ShootStraight(bullet, player.transform.position, 90 + spread, player.bulletSpeed);
         Patterns.ShootStraight(bullet, player.transform.position, 90 - spread, player.bulletSpeed);


        
    }

    public static void WaterMode(Bullet bullet, Player player)
    {
        Vector3 spread = player.isFocus ? new Vector2(0.3f, 0) : new Vector2(1, 0);
        Patterns.ShootStraight(bullet, player.transform.position, 90, player.bulletSpeed);
        Patterns.ShootStraight(bullet, player.transform.position + spread, 90, player.bulletSpeed);
        Patterns.ShootStraight(bullet, player.transform.position- spread, 90, player.bulletSpeed);



    }
    public static void FireMode(Bullet bullet, Player player)
    {
        Patterns.ShootStraight(bullet, player.transform.position, 90, player.bulletSpeed);
    }

}
