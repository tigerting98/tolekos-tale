using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPattern : MonoBehaviour
{
    public static IEnumerator Mode1(Bullet bullet, Player player)
    {
        while (true)
        {

            Bullet bul1 = Patterns.ShootStraight(bullet, player.transform.position, 90, player.bulletSpeed);
            Bullet bul2 = Patterns.ShootStraight(bullet, player.transform.position, 100, player.bulletSpeed);
            Bullet bul3 = Patterns.ShootStraight(bullet, player.transform.position, 80, player.bulletSpeed);

            yield return new WaitForSeconds(player.shotRate);
        }
    }

    public static IEnumerator Mode2(Bullet bullet, Player player)
    {
        while (true)
        {

            Bullet bul1 = Patterns.ShootStraight(bullet, player.transform.position, 90, player.bulletSpeed);
            Bullet bul2 = Patterns.ShootStraight(bullet, player.transform.position + new Vector3(1, 0), 90, player.bulletSpeed);
            Bullet bul3 = Patterns.ShootStraight(bullet, player.transform.position - new Vector3(1, 0), 90, player.bulletSpeed);


            yield return new WaitForSeconds(player.shotRate);

        }
    }
    public static IEnumerator Mode3(Bullet bullet, Player player)
    {
        while (true)
        {

            Bullet bul1 = Patterns.ShootStraight(bullet, player.transform.position, 90, player.bulletSpeed);

            yield return new WaitForSeconds(player.shotRate / 3);
        }
    }

}
