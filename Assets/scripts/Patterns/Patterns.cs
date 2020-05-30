
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patterns : MonoBehaviour
{
    public static Bullet ShootStraight(Bullet bullet, Vector3 pos, float angle, float speed)
    {


        Bullet bul = Instantiate(bullet, pos, Quaternion.Euler(0, 0, angle));


        bul.setSpeed(Quaternion.Euler(0, 0, angle) * new Vector3(speed, 0, 0));
        return bul;

    }
   

    public static float AimAt(Vector2 shooter, Vector2 target)
    {

        Vector2 diff = target - shooter;
        return Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);
    }
    
    public static List<Bullet> RingOfBullets(Bullet bullet, Vector2 origin, int number, float offset, float speed) {
        List<Bullet> bullets = new List<Bullet>();
        for (int i=0; i < number; i++) {
            bullets.Add(ShootStraight(bullet, origin, offset + i * 360 / number, speed));
        }
        return bullets;
        
    }

    public static Bullet ShootSinTrajectory(Bullet bullet, Vector2 origin, float angle, float speed, float angularVelocity, float amp) {
        Bullet bull = Instantiate(bullet, origin, Quaternion.identity);
        Movement move = bull.movement;
        move.SetCustomPath(time => new Vector2(speed * time, (float)(amp * Math.Sin(time * angularVelocity))));
        move.RotateTrajectory(angle);
        return bull;
    }

    public static Bullet ArchimedesSpiral(Bullet bul, Vector2 origin, float spirallingRatio, float speed, float angle)
    {
        Bullet bull = Instantiate(bul, origin, Quaternion.identity);
        Movement move = bull.movement;
        move.SetPolarPath(t=> new Polar(speed*t, spirallingRatio*speed * t));
        move.RotateTrajectory(angle);
        return bull;
    }
}
