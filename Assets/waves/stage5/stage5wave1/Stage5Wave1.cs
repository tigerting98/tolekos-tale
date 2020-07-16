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
            i => ArcShape(round, smallrounddmg, enemy.transform.position, i % 2 == 0,
            startSpeed, endSpeed, minTime, maxTime, numberPerArcs, numberOfArcs, Functions.AimAtPlayer(enemy.transform), null), shotRate
            ), time);
        if (harder) {
            enemy.shooting.StartShootingAfter(Functions.RepeatCustomAction(
                i => ArcShape(round, smallrounddmg, enemy.transform.position, i % 2 == 1,
                startSpeed, endSpeed, minTime, maxTime, numberPerArcs, numberOfArcs, Functions.AimAtPlayer(enemy.transform), null), shotRate
                ), time);
        }
        enemy.shooting.StartShootingFor(Functions.RepeatAction(
            () =>
            {
                for (int j = 0; j < starsPerShot; j++)
                {
                    Bullet bul = Patterns.ShootStraight(star, stardmg, Functions.RandomLocation(enemy.transform.position, 0.3f), Random.Range(0f, 360f), Random.Range(minSpeed, maxSpeed), null);
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
    List<Bullet> ArcShape(Bullet bul, float dmg, Vector2 origin, bool clockwise, float initialSpeed, float finalSpeed, float minTime, float maxTime, int numberPerArc, int numberOfArcs, float offset, SFX sfx)
    {
        
        List<Bullet> buls = new List<Bullet>();
        for (int i = 0; i < numberPerArc; i++) 
        {
            buls.AddRange(
                Patterns.ExplodingRingOfBullets(bul, dmg, origin, numberOfArcs,
                offset + (clockwise ? 1 : -1) * i * 360f / (numberOfArcs * numberPerArc), initialSpeed, finalSpeed, minTime + i * (maxTime - minTime) / (numberPerArc - 1),
                null));
        }
        Functions.Scale(buls, ballSize);
        return buls;
    }
}
