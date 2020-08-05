using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5Wave1 : EnemyWave
{
    [SerializeField] bool harder = false;
    [SerializeField] EnemyStats stats;
    [SerializeField] float speed = 7f;
    [SerializeField] float moveSpeed;
    [SerializeField] float smallrounddmg;
    [Header("Bullet Behavior")]
    [SerializeField] float endSpeed = 0.7f;
    [SerializeField] float startSpeed = 3f;
    [SerializeField] float ballSize = 0.5f;
    [SerializeField] float minTime = 0.1f, maxTime = 0.5f, shotRate = 1f;
    [SerializeField] int numberOfArcs = 5, numberPerArcs = 3;
    [SerializeField] float starsize = 0.5f;
    [SerializeField] int starsPerShot;
    [SerializeField] float shotRateStar, minSpeed, maxSpeed, stardmg;
    [SerializeField] Vector2 water, earth, fire;
    [SerializeField] float shootingDuration = 13f;
    public override void SpawnWave()
    {
        StartCoroutine(SpawnPixie(DamageType.Water, water));
        StartCoroutine(SpawnPixie(DamageType.Earth, earth));
        StartCoroutine(SpawnPixie(DamageType.Fire, fire));
    }
    IEnumerator SpawnPixie(DamageType type, Vector2 location) {
        yield return new WaitForSeconds(1f);
        Bullet star = GameManager.gameData.starBullet.GetItem(type);
        Bullet round = GameManager.gameData.smallRoundBullet.GetItem(type);
        Enemy enemy = Instantiate(GameManager.gameData.pixies.GetItem(type), location,Quaternion.identity);
        enemy.SetEnemy(stats, false);
        float time = 2;
        enemy.shooting.StartShootingAfter(Functions.RepeatCustomAction(
            i => enemy.shooting.StartShooting(ArcShape(round, smallrounddmg, enemy.transform.position, i % 2 == 0,
            startSpeed, endSpeed, minTime, maxTime, numberPerArcs, numberOfArcs, Functions.AimAtPlayer(enemy.transform))), shotRate
            ), time);
        if (harder) {
            enemy.shooting.StartShootingAfter(Functions.RepeatCustomAction(
                i => enemy.shooting.StartShooting(ArcShape(round, smallrounddmg, enemy.transform.position, i % 2 == 1,
                startSpeed, endSpeed, minTime, maxTime, numberPerArcs, numberOfArcs, Functions.AimAtPlayer(enemy.transform))), shotRate
                ), time);
        }
        enemy.shooting.StartShootingFor(Functions.RepeatAction(
            () =>
            {
                for (int j = 0; j < starsPerShot; j++)
                {
                    Bullet bul = Patterns.ShootStraight(star, stardmg, Functions.RandomLocation(enemy.transform.position, 0.3f, false), Random.Range(0f, 360f), Random.Range(minSpeed, maxSpeed), GameManager.gameData.clickSFX);
                    bul.transform.localScale *= starsize;
                }
            }, shotRateStar
            ), time, shootingDuration);
        yield return new WaitForSeconds(shootingDuration+time);
        if (enemy) {
           float time2=  enemy.movement.MoveTo(new Vector2(location.y, 4.3f), moveSpeed/3);
            Destroy(enemy.gameObject, time2);

        }

    }
    IEnumerator ArcShape(Bullet bul, float dmg, Vector2 origin, bool clockwise, float initialSpeed, float finalSpeed, float minTime, float maxTime, int numberPerArc, int numberOfArcs, float offset)
    {
        AudioManager.current.PlaySFX(GameManager.gameData.magicPulse1SFX);
        float angle = 360f / (numberOfArcs * numberPerArc);
        float time = (maxTime - minTime) / (numberPerArc - 1);
        for (int i = 0; i < numberPerArc; i++) 
        {

              Functions.Scale(  Patterns.ExplodingRingOfBullets(bul, dmg, origin, numberOfArcs,
                offset + (clockwise ? 1 : -1) * i * angle, initialSpeed, finalSpeed, minTime + i * time ,
                null), ballSize);
            yield return null;
        }


    }
}
