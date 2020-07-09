

using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class Patterns : MonoBehaviour
{
    public static Bullet ShootStraight(Bullet bullet, float dmg, Vector2 origin, float angle, float speed, SFX sfx)
    {
        return ShootCustomBullet(bullet, dmg, origin, Movement.RotatePath(angle, t => new Vector2(speed, 0)), MovementMode.Velocity, sfx); 

    }
    public static Bullet BurstShoot(Bullet bullet, float dmg, Vector2 origin, float angle, float initialSpeed, float finalSpeed, float time, SFX sfx)
    {
        return ShootCustomBullet(bullet,dmg, origin, t => Quaternion.Euler(0, 0, angle) * new Vector2(t > time ? finalSpeed : initialSpeed, 0), MovementMode.Velocity, sfx);
    }

    public static Bullet ShootCustomBullet(Bullet bullet, float dmg, Vector2 origin, Func<float, Vector2> function, MovementMode mode, SFX sfx) {
        Bullet bul = GameManager.bulletpools.SpawnBullet(bullet, origin);
        if (sfx) {
            AudioManager.current.PlaySFX(sfx);
        }
        bul.SetDamage(dmg);
        bul.movement.SetCustomGraph(function, mode);
        return bul;
    }
    public static List<T> CustomRing<T>(Func<float, T> bulletFunction, float offset, int lines)
    {
        return CustomRingWithCustomSpacing<T>(bulletFunction, i => i * 360f / lines, offset, lines);
    }

    public static List<T> CustomRingWithCustomSpacing<T>(Func<float, T> bulletFunction, Func<int, float> spacingFunction, float offset, int lines) {
        List<T> bullets = new List<T>();
        for (int i = 0; i < lines; i++)
        {
            bullets.Add(bulletFunction(offset + spacingFunction(i)));
        }
        return bullets;


    }
    public static List<Bullet> ExplodingLine(Bullet bullet, float dmg, Vector2 origin, float angle, float intialSpeed, float finalSpeed, int number, float minTime, float maxTime, SFX sfx) {
        List<Bullet> bullets = new List<Bullet>();
        float diffTime = (maxTime - minTime) / (number - 1);
        for (int i = 0; i < number; i++) {
            bullets.Add(BurstShoot(bullet, dmg, origin, angle, intialSpeed, finalSpeed, minTime + diffTime * i, sfx));
        }
        return bullets;
        
    }

    public static List<Bullet> RingAroundBossAimAtPlayer(Bullet bullet, float dmg, Vector2 centre, float radius, float speed, int number, SFX sfx) {
        List<Bullet> bullets = new List<Bullet>();
        for (int i = 0; i < number; i++) {
            Vector2 pos = centre + new Polar(radius, i * 360 / number).rect;
            float angle = Functions.AimAt(pos, GameManager.playerPosition);
            bullets.Add(ShootStraight(bullet, dmg, pos, angle, speed, sfx));
        }
        return bullets;
    }

    public static List<Bullet> BulletSpreadingOut(Bullet bullet, float dmg, Vector2 origin, float initialSpeed, float speedDiff, float angle, int number, SFX sfx) {
        List<Bullet> bullets = new List<Bullet>();
        for (int i = 0; i < number; i++)
        {

            bullets.Add(ShootStraight(bullet, dmg, origin, angle, initialSpeed + speedDiff*i, sfx));
        }
        return bullets;
    }
    public static List<Bullet> SpirallingOutwardsRing(Bullet bul, float dmg, Vector2 origin, float radialvel, float angularvel, int number, float offset, SFX sfx)
    {
        return Patterns.CustomRing(angle => Patterns.ShootCustomBullet(
                      bul, dmg,  origin,
                      t => new Polar(radialvel * t, angle + angularvel * t).rect, MovementMode.Position, sfx
                      ), offset, number);
    }
    /*
    public static float AimAt(Vector2 shooter, Vector2 target)
    {

        Vector2 diff = target - shooter;
        return Mathf.Rad2Deg * Mathf.Atan2(diff.y, diff.x);
    }
    */
    public static List<Bullet> RingOfBullets(Bullet bullet, float dmg, Vector2 origin, int number, float offset, float speed, SFX sfx) {
        
        return CustomRing(theta => ShootStraight(bullet, dmg, origin, theta, speed, sfx), offset, number);  
        
    }
    public static List<Bullet> ExplodingRingOfBullets(Bullet bullet, float dmg, Vector2 origin, int number, float offset, float initialSpeed, float finalSpeed, float time, SFX sfx)
    {
        return CustomRing(theta => BurstShoot(bullet, dmg, origin, theta, initialSpeed, finalSpeed, time, sfx), offset, number);

    }

    public static Bullet ShootSinTrajectory(Bullet bullet, float dmg, Vector2 origin, float angle, float speed, float angularVelocity, float amp, SFX sfx) {
        
        return ShootCustomBullet(bullet, dmg, origin, Movement.RotatePath(angle, time => new Vector2(speed * time, (float)(amp * Math.Sin(time * angularVelocity)))), MovementMode.Position,sfx);
        
    }
    public static List<T> ShootMultipleCustomBullet<T>(Func<float, T> bulletFunction, float mainAngle, float spreadAngle, int sideLines) {
        return CustomRingWithCustomSpacing(bulletFunction, x => (x - sideLines) * spreadAngle, mainAngle, sideLines * 2 + 1);
    
    }

    public static List<Bullet> ShootMultipleStraightBullet(Bullet bullet, float dmg, Vector2 origin, float speed, float mainAngle, float spreadAngle, int lines, SFX sfx) {
        return ShootMultipleCustomBullet(angle => ShootStraight(bullet, dmg, origin, angle, speed,sfx), mainAngle, spreadAngle, lines);
    }
   

    public static Bullet HomeNearestEnemy(Bullet bul, float dmg, Vector2 origin, Vector2 defaultVelocity,SFX sfx) {
        Bullet bullet = GameManager.bulletpools.SpawnBullet(bul, origin);
        AudioManager.current.PlaySFX(sfx);
        bullet.SetDamage(dmg);
        GameObject target = Functions.GetNearestEnemy(origin);
        if (target == null)
        {
            bullet.movement.SetSpeed(defaultVelocity);
        }
        else {
            bullet.movement.Homing(target, defaultVelocity.magnitude);
        
        }
        return bullet;
    }

    
}
