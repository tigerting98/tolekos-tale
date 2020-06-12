

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patterns : MonoBehaviour
{
    public static Bullet ShootStraight(Bullet bullet, Vector3 origin, float angle, float speed)
    {
        return ShootCustomBullet(bullet, origin, Movement.RotatePath(angle, t => new Vector3(speed, 0, 0)), MovementMode.Velocity); 

    }
    public static Bullet BurstShoot(Bullet bullet, Vector2 origin, float angle, float initialSpeed, float finalSpeed, float time)
    {
        return ShootCustomBullet(bullet, origin, t => Quaternion.Euler(0, 0, angle) * new Vector2(t > time ? finalSpeed : initialSpeed, 0), MovementMode.Velocity);
    }

    public static Bullet ShootCustomBullet(Bullet bullet, Vector2 origin, Func<float, Vector2> function, MovementMode mode) {
        Bullet bul = Instantiate(bullet, origin, Quaternion.identity);
        bul.movement.SetCustomGraph(function, mode);
        return bul;
    }
    public static List<Bullet> RingOfCustomBullets(Func<float, Bullet> bulletFunction, float offset, int lines)
    {
        return RingOfCustomBulletsWithCustomSpacing(bulletFunction, i => i * 360f / lines, offset, lines);
    }

    public static List<Bullet> RingOfCustomBulletsWithCustomSpacing(Func<float, Bullet> bulletFunction, Func<int, float> spacingFunction, float offset, int lines) {
        List<Bullet> bullets = new List<Bullet>();
        for (int i = 0; i < lines; i++)
        {
            bullets.Add(bulletFunction(offset + spacingFunction(i)));
        }
        return bullets;


    }
    public static List<Bullet> ExplodingLine(Bullet bullet, Vector2 origin, float angle, float intialSpeed, float finalSpeed, int number, float minTime, float maxTime) {
        List<Bullet> bullets = new List<Bullet>();
        float diffTime = (maxTime - minTime) / (number - 1);
        for (int i = 0; i < number; i++) {
            bullets.Add(BurstShoot(bullet, origin, angle, intialSpeed, finalSpeed, minTime + diffTime * i));
        }
        return bullets;
        
    }



    public static float AimAt(Vector2 shooter, Vector2 target)
    {

        Vector2 diff = target - shooter;
        return Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);
    }
    
    public static List<Bullet> RingOfBullets(Bullet bullet, Vector2 origin, int number, float offset, float speed) {
        
        return RingOfCustomBullets(theta => ShootStraight(bullet, origin, theta, speed), offset, number);  
        
    }
    public static List<Bullet> ExplodingRingOfBullets(Bullet bullet, Vector2 origin, int number, float offset, float initialSpeed, float finalSpeed, float time)
    {
        return RingOfCustomBullets(theta => BurstShoot(bullet, origin, theta, initialSpeed, finalSpeed, time), offset, number);

    }

    public static Bullet ShootSinTrajectory(Bullet bullet, Vector2 origin, float angle, float speed, float angularVelocity, float amp) {
        
        return ShootCustomBullet(bullet, origin, Movement.RotatePath(angle, time => new Vector2(speed * time, (float)(amp * Math.Sin(time * angularVelocity)))), MovementMode.Position);
        
    }
    public static List<Bullet> ShootMultipleCustomBullet(Func<float, Bullet> bulletFunction, float mainAngle, float spreadAngle, int sideLines) {
        return RingOfCustomBulletsWithCustomSpacing(bulletFunction, x => (x - sideLines) * spreadAngle, mainAngle, sideLines * 2 + 1);
    
    }

    public static List<Bullet> ShootMultipleStraightBullet(Bullet bullet, Vector2 origin, float speed, float mainAngle, float spreadAngle, int lines) {
        return ShootMultipleCustomBullet(angle => ShootStraight(bullet, origin, angle, speed), mainAngle, spreadAngle, lines);
    }
    public static Bullet ArchimedesSpiral(Bullet bul, Vector2 origin, float spirallingRatio, float speed, float angle)
    {
        Bullet bull = Instantiate(bul, origin, Quaternion.identity);
        Movement move = bull.movement;
        move.SetPolarPath(t=> new Polar(speed*t, spirallingRatio*speed * t));
        move.RotateTrajectory(angle);
        return bull;
    }

    public static Bullet HomeNearestEnemy(Bullet bul, Vector2 origin, Vector2 defaultVelocity) {
        Bullet bullet = Instantiate(bul, origin, Quaternion.identity);
        GameObject target = GetNearestEnemy(origin);
        if (target == null)
        {
            bullet.movement.SetSpeed(defaultVelocity);
        }
        else {
            bullet.movement.Homing(target, defaultVelocity.magnitude);
        
        }
        return bullet;
    }

    public static GameObject GetNearestEnemy(Vector2 origin) {
        GameObject obj = null;
        float distance = Mathf.Infinity;
        foreach (GameObject item in GameManager.enemies.Values) {
            Vector2 pos = item.transform.position;
            if (pos.x < 4.1 && pos.x > -4.1 && pos.y < 4.1 && pos.y > -4.1)
            {
                float dist = (pos - origin).magnitude;

                if (dist < distance)
                {
                    distance = dist;
                    obj = item;

                }
            }
            
        }

        return obj;
    }
}
