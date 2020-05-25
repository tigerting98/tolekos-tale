using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyPatterns : MonoBehaviour
{
    public static IEnumerator ShootAtPlayer(Bullet bullet, Transform enemy, float speed, float shotRate)
    {
        while (true)
        {


            Patterns.ShootStraight(bullet, enemy.position, Patterns.AimAt(enemy.position, GameManager.playerPosition), speed);
            yield return new WaitForSeconds(shotRate);
        }

    }

    public static IEnumerator ShootAtPlayerWithLines(Bullet bullet, Transform enemy, float speed, float shotRate, float spreadAngle, int lines)
    {
        Assert.IsTrue(lines >= 1);
        while (true)
        {
            float angle = Patterns.AimAt(enemy.position, GameManager.playerPosition);

            Patterns.ShootStraight(bullet, enemy.position, angle, speed);
            for (int i = 1; i < lines+1; i++)
            {
                Patterns.ShootStraight(bullet, enemy.position, angle - spreadAngle * i, speed);
                Patterns.ShootStraight(bullet, enemy.position, angle + spreadAngle * i, speed);
            }
            yield return new WaitForSeconds(shotRate);
        }

    }

    public static IEnumerator PulsingBullets(Bullet bullet, Transform enemy, float speed, float shotRate, int lines)
    {

        while (true)
        {
            float angle = Patterns.AimAt(enemy.position, GameManager.playerPosition);

            Patterns.RingOfBullets(bullet, enemy.position, lines, angle, speed);

            yield return new WaitForSeconds(shotRate);



        }


    }

    public static IEnumerator BorderOfWaveAndParticle(Bullet bullet, Transform enemy, float speed, float shotRate, int lines, float angularVel)
    {

       
        float timer = 0;
        while (true)
        {
            float angle = (float)(180 * Math.Sin(timer * angularVel));

            Patterns.RingOfBullets(bullet, enemy.position, lines, angle, speed);

            timer += shotRate;
            yield return new WaitForSeconds(shotRate);
        }
    }

    public static IEnumerator ShootSineAtPlayer(Bullet bullet, Transform enemy, float speed, float shotRate, float angularVel, float amp)
    {
        while (true)
        {
            Patterns.ShootSinTrajectory(bullet, enemy.position, Patterns.AimAt(enemy.position, GameManager.playerPosition), speed, angularVel, amp);
            yield return new WaitForSeconds(shotRate);
        }
    }
    public static IEnumerator ArchimedesSpiral(Bullet bullet, Transform enemy, float ratio, float speed, float shotRate, float angle)
    {
        while (true)
        {
            Patterns.ArchimedesSpiral(bullet, enemy.position, ratio, speed, angle);
            yield return new WaitForSeconds(shotRate);
        }
    }
}
