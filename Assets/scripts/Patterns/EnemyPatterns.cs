using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyPatterns : MonoBehaviour {

    //shoots constant speed bullet at a target angle that is fast at first then slow in a constant rate
    public static IEnumerator ExplodingLineAtPlayer(Bullet bullet, float dmg, Transform enemy, float intialSpeed, float finalSpeed, int number, float minTime, float maxTime, float shotRate, SFX sfx)
    {
        return Functions.RepeatAction(() => Patterns.ExplodingLine(bullet, dmg, enemy.position, 
            Functions.AimAtPlayer(enemy), intialSpeed, finalSpeed, number, minTime, maxTime, sfx), shotRate);

    }

    //shoots bullets at players in a constant rate
    public static IEnumerator ShootAtPlayer(Bullet bullet, float dmg, 
        Transform enemy, float speed, float shotRate, SFX sfx)

    {
        return ShootAtPlayerWithLines(bullet, dmg, enemy, speed, shotRate, 0, 0, sfx);

    }

    //shoots multiple bullets at player, with the centre aimed directly at him
    public static IEnumerator ShootAtPlayerWithLines(Bullet bullet, float dmg, Transform enemy, float speed, float shotRate, float spreadAngle, int lines, SFX sfx)
    {


        return Functions.RepeatAction(() =>
            Patterns.ShootMultipleStraightBullet(bullet, dmg, enemy.position, speed,
                Functions.AimAtPlayer(enemy), spreadAngle, lines, sfx), shotRate);

    }
   
    // shoots a stream of certain number of bullets, resulting in a line of bullets
    public static IEnumerator PulsingLine(Bullet bullet, float dmg, Transform enemy, float speed, float angle, float shotRate, int number, SFX sfx) {

        return Functions.RepeatActionXTimes(() => Patterns.ShootStraight(bullet, dmg, enemy.position, angle, speed, sfx), shotRate, number);
    }

    //takes a subpattern and continue it repetitvely with an interval
    public static IEnumerator RepeatSubPatternWithInterval(Func<IEnumerator> subpattern, Shooting enemy, float interval) {
        return Functions.RepeatAction(() => enemy.StartCoroutine(subpattern()), interval);
    }
    //shoots a lines of bullets in a circle
    public static IEnumerator PulsingLines(Bullet bullet, float dmg, Transform enemy, float speed, float angle, float shotRate, int lines, int number, SFX sfx) {
        return Functions.RepeatActionXTimes(() => Patterns.RingOfBullets(bullet, dmg, enemy.position, lines, angle, speed,sfx ), shotRate, number);
    } 
    //shoots circles of bullets, centered at the player angle
    public static IEnumerator PulsingBullets(Bullet bullet, float dmg, Transform enemy, float speed, float shotRate, int lines, SFX sfx)
    {


        return Functions.RepeatAction(() => Patterns.RingOfBullets(bullet, dmg, enemy.position, lines, Functions.AimAtPlayer(enemy), speed, sfx),
            shotRate);


    }

    //shoots circles of bullets, each with different random offset angle
    public static IEnumerator PulsingBulletsRandomAngle(Bullet bullet, float dmg, Transform enemy, float speed, float shotRate, int lines, SFX sfx) {
        return Functions.RepeatAction(() => Patterns.RingOfBullets(bullet, dmg, enemy.position, lines, UnityEngine.Random.Range(0f, 360f), speed,sfx),
            shotRate);
    }


    //shoots circles of random bullets at the player
    public static IEnumerator PulsingBulletsRandom(List<Bullet> bullets, float dmg, Transform enemy, float speed, float shotRate, int lines,SFX sfx)
    {

        return Functions.RepeatAction(() => Patterns.RingOfBullets(bullets[UnityEngine.Random.Range(0, bullets.Count)], dmg, enemy.position, lines, 
           Functions.AimAtPlayer(enemy), speed,sfx),
            shotRate);
    }

    //shoots bullets of custom behavior with custom spacing at a custom angular velocity
    public static IEnumerator CustomSpinningCustomBulletsCustomSpacing(Func<float,Bullet> bulFunction, Func<int, float> spacingFunction, Func<float, float> spinningFunction, int lines, float shotRate)
    {
        return Functions.RepeatCustomAction(
            i => Patterns.CustomRingWithCustomSpacing(angle => bulFunction(angle),
            spacingFunction, spinningFunction(i * shotRate), lines), shotRate);


    }
    //shoots striaght bullets of custom spacing at a custom angular velocity
    public static IEnumerator CustomSpinningStraightBulletsCustomSpacing(Bullet bullet, float dmg, Transform enemy, float speed, Func<int,float> spacingFunction, Func<float, float> spinningFunction, int lines, float shotRate,SFX sfx) {
        return CustomSpinningCustomBulletsCustomSpacing(angle => Patterns.ShootStraight(bullet, dmg, enemy.position, angle, speed,sfx),
            spacingFunction, spinningFunction, lines, shotRate);

    
    }

    //shoots straight bullets that is spaced apart evenly with a custom angular velocity
    public static IEnumerator CustomSpinningStraightBullets(Bullet bullet, float dmg, Transform enemy, float speed, Func<float, float> spinningFunction, int lines, float shotRate, SFX sfx) {
        return CustomSpinningStraightBulletsCustomSpacing(bullet, dmg, enemy, speed, i => 360f * i / lines, spinningFunction, lines, shotRate, sfx);
    }


    //shoots straight bullets that is spaced apart evenly with a constant angular velocity
    public static IEnumerator ConstantSpinningStraightBullets(Bullet bullet, float dmg, Transform enemy, float speed, float angularVel, float start, int lines, float shotRate,SFX sfx) {
        return CustomSpinningStraightBullets(bullet, dmg, enemy, speed, time => start + angularVel * time, lines, shotRate,sfx);
    }

    //a pattern that uses a sin wave angular velocity
    public static IEnumerator BorderOfWaveAndParticle(Bullet bullet, float dmg, Transform enemy, float speed, float shotRate, int lines, float angularVel,SFX sfx)
    {

        return CustomSpinningStraightBullets(bullet, dmg, enemy, speed, time => (float)(180 * Math.Sin(time * angularVel)), lines, shotRate,sfx);

    }

    //a pattern that looks like a fanning effect
    public static void StartFanningPattern(Bullet bullet, float dmg, Shooting enemy, float speed, float angularVel, float start, int lines, float shotRate, int number, float speedDiff,SFX sfx) {
        Functions.StartMultipleCustomCoroutines(enemy, i => ConstantSpinningStraightBullets(bullet, dmg, enemy.transform, speed + i * speedDiff, angularVel, start, lines, shotRate,sfx), number);
    }

    //summons magic circle that rotate around the enemy
    public static Bullet SummonMagicCircle(Bullet magicCircle, float dmg, Transform enemy, float timeToRadius, float angle, float radius, float rotationSpeed,SFX sfx) {
        Bullet bul = Patterns.ShootCustomBullet(magicCircle, dmg, enemy.position, Movement.RotatePath(
             angle, t => new Polar(t > timeToRadius ? radius : radius * t / timeToRadius, rotationSpeed * t).rect), MovementMode.Position,sfx);
        bul.transform.SetParent(enemy);
        return bul;
    }

    //summons a bullet that shoots out then rotate outwards
    public static Bullet OutAndSpinBullet(Bullet bul, float dmg, Transform origin, float initialSpeed, float radius, float radialSpeed2, float angularVel, float delay, float initialAngle,SFX sfx) {
        return Patterns.ShootCustomBullet(bul, dmg, origin.position,
            t => new Polar(t < radius / initialSpeed ? initialSpeed * t : t < radius / initialSpeed + delay ? radius : radius + radialSpeed2 * (t - delay - radius / initialSpeed),
            initialAngle + (t < radius / initialSpeed + delay ? 0 : angularVel * (t - radius / initialSpeed - delay))).rect, MovementMode.Position,sfx);
    }
    // summons a ring of bullet that shoots out and then rotate outwards
    public static List<Bullet> OutAndSpinRingOfBullets(Bullet bul, float dmg, Transform origin, float intialSpeed, float radius, float radialSpeed2, float angularVel, float delay, float offset, int lines,SFX sfx) {
        return Patterns.CustomRing(angle => OutAndSpinBullet(bul, dmg, origin, intialSpeed, radius, radialSpeed2, angularVel, delay, angle,sfx), offset, lines);
    }

    //shoot bullets that follow a sine pattern
    public static IEnumerator ShootSine(Bullet bullet, float dmg, Transform enemy, float angle, float speed, float shotRate, float angularVel, float amp,SFX sfx)
    {
        
         return Functions.RepeatAction(()=>Patterns.ShootSinTrajectory(bullet, dmg, enemy.position, angle, speed, angularVel, amp,sfx), shotRate);
        
    }
    //shoot bullet that seem to follow drag and gravity
    public static Bullet FallingBullet(Bullet bullet, float dmg, Vector2 origin, float initialAngle, float downwardsAcceleration, float timeOfAcceleration, float initialSpeed,SFX sfx)
    {
        Bullet bul = GameManager.bulletpools.SpawnBullet(bullet, origin);
        AudioManager.current.PlaySFX(sfx);
        bul.SetDamage(dmg);
        bul.movement.destroyBoundary = 6f;
        bul.movement.SetAcceleration(Quaternion.Euler(0, 0, initialAngle) * new Vector2(initialSpeed, 0), t => new Vector2(0, t < timeOfAcceleration ? -downwardsAcceleration : 0));
        return bul;


    }


    //shoot a conelike pattern
    public static IEnumerator ConePattern(Bullet bullet, float dmg, Transform spawner, float angle, float speed, float spawnRate, int number, float spacing,SFX sfx)
    {
       
        return Functions.RepeatCustomActionXTimes(i => {
            Vector2 start = spawner.position - Quaternion.Euler(0, 0, angle) * new Vector2(0, (spacing * i) / 2);
            for (int j = 0; j < i + 1; j++)
            {
                
                Bullet bul = Patterns.ShootStraight(bullet, dmg, start + new Polar(spacing * j, angle).rect, angle, speed, sfx);
            }

        }, spawnRate, number);


      




    }
}
