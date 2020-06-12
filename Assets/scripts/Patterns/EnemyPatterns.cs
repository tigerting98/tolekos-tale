using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyPatterns : MonoBehaviour { 

  public static IEnumerator ShootAt(Bullet bullet, Transform enemy, float angle, float speed, float shotRate)
    { 
        return ShootPattern(() => Patterns.ShootStraight(bullet, enemy.position, angle, speed), shotRate);

 }

    public static IEnumerator ShootPattern(Action shoot, float shotRate) {
        while (true) {
            shoot();
            
            yield return new WaitForSeconds(shotRate);
        }

    }
   





    public static IEnumerator ExplodingLineAtPlayer(Bullet bullet, Transform enemy, float intialSpeed, float finalSpeed, int number, float minTime, float maxTime, float shotRate)
    {
        return ShootPattern(() => Patterns.ExplodingLine(bullet, enemy.position, Patterns.AimAt(enemy.transform.position, GameManager.playerPosition), intialSpeed, finalSpeed, number, minTime, maxTime), shotRate); 
           
    }
    public static IEnumerator ShootAtPlayer(Bullet bullet, Transform enemy, float speed, float shotRate)

    {
        return ShootAtPlayerWithLines(bullet, enemy, speed, shotRate, 0, 0);

    }

    public static IEnumerator ShootAtPlayerWithLines(Bullet bullet, Transform enemy, float speed, float shotRate, float spreadAngle, int lines)
    {

        
        return ShootPattern(() =>
            Patterns.ShootMultipleStraightBullet(bullet, enemy.position, speed, 
                Patterns.AimAt(enemy.position, GameManager.playerPosition), spreadAngle, lines), shotRate);

    }

    public static IEnumerator PulsingBullets(Bullet bullet, Transform enemy, float speed, float shotRate, int lines)
    {


        return ShootPattern(() => Patterns.RingOfBullets(bullet, enemy.position, lines, Patterns.AimAt(enemy.position, GameManager.playerPosition), speed),
            shotRate);


    }
    public static IEnumerator PulsingBulletsRandomAngle(Bullet bullet, Transform enemy, float speed, float shotRate, int lines) {
        return ShootPattern(() => Patterns.RingOfBullets(bullet, enemy.position, lines, UnityEngine.Random.Range(0f, 360f), speed),
            shotRate);
    }

    public static IEnumerator PulsingBulletsRandom(List<Bullet> bullets, Transform enemy, float speed, float shotRate, int lines)
    {

        return ShootPattern(() => Patterns.RingOfBullets(bullets[UnityEngine.Random.Range(0, bullets.Count)], enemy.position, lines, 
            Patterns.AimAt(enemy.position, GameManager.playerPosition), speed),
            shotRate);
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

    public static IEnumerator BorderOfWaveAndParticleRandom(List<Bullet> bullets, Transform enemy, float speed, float shotRate, int lines, float angularVel)
    {


        float timer = 0;
        while (true)
        {
            float angle = (float)(180 * Math.Sin(timer * angularVel));

            Patterns.RingOfBullets(bullets[UnityEngine.Random.Range(0, bullets.Count)], enemy.position, lines, angle, speed);

            timer += shotRate;
            yield return new WaitForSeconds(shotRate);
        }
    }

    public static IEnumerator ShootSine(Bullet bullet, Transform enemy, float angle, float speed, float shotRate, float angularVel, float amp)
    {
        
         return ShootPattern(()=>Patterns.ShootSinTrajectory(bullet, enemy.position, angle, speed, angularVel, amp), shotRate);
        
    }
    public static IEnumerator ArchimedesSpiral(Bullet bullet, Transform enemy, float ratio, float speed, float shotRate, float angle)
    {
        
        return ShootPattern(()=>Patterns.ArchimedesSpiral(bullet, enemy.position, ratio, speed, angle), shotRate);
        
    }

    public static IEnumerator ShootLaserBeam(Bullet actualLaser, Bullet warningLaser, Transform enemy, float angle, float timeWarning, float duration) {
        Bullet warning = Instantiate(warningLaser, enemy.position, Quaternion.Euler(0, 0, angle), enemy);
        warning.orientation.SetFixedOrientation(Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(timeWarning);
        Destroy(warning.gameObject);
        Bullet actual = Instantiate(actualLaser, enemy.position, Quaternion.Euler(0, 0, angle), enemy);
        actual.orientation.SetFixedOrientation(Quaternion.Euler(0, 0, angle));
        yield return new WaitForSeconds(duration);
        
        Destroy(actual.gameObject);


    }
    public static IEnumerator ConePattern(Bullet bullet, Transform spawner, float angle, float speed, float spawnRate, float number, float spacing)
    {
        for (int i = 0; i < number; i++)
        {
            Vector2 start = spawner.position - Quaternion.Euler(0, 0, angle) * new Vector2(0, (spacing * i) / 2);
            for (int j = 0; j < i + 1; j++)
            {
                Bullet bul = Instantiate(bullet, start + (Vector2)(Quaternion.Euler(0, 0, angle) * new Vector2(0, spacing * j)), Quaternion.identity);
                bul.movement.SetSpeed(Quaternion.Euler(0, 0, angle) * new Vector2(speed, 0));
            }
            yield return new WaitForSeconds(spawnRate);
        }


    }
}
