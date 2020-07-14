using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Stage5Wave5SubWave
{
    public float delay;
    public DamageType type;
    public int numOfEnemies;
}
public class Stage5Wave5 : EnemyWave
{
    [SerializeField] float delayBeforeMoving;
    [SerializeField] float shotRate, spawnRate, moveSpeed, bulletAccel, bulletSpeed, bulletDmg, jitterFactor;
    [SerializeField] EnemyStats stats;
    [SerializeField] List<Stage5Wave5SubWave> subwaves;

    public override void SpawnWave() {
        foreach (Stage5Wave5SubWave subwave in subwaves) {
            StartCoroutine(SpawnEnemy(subwave));
        }
    }

    IEnumerator SpawnEnemy(Stage5Wave5SubWave subwave) {
        yield return new WaitForSeconds(subwave.delay);
        for (int i = 0; i < subwave.numOfEnemies; i ++) {
        Enemy en = Instantiate(GameManager.gameData.flyingBook, new Vector2(-3.9f + i * 8f/ (subwave.numOfEnemies), 4.2f), Quaternion.identity);
        en.movement.SetAcceleration(new Vector2(0f, -moveSpeed), time => new Vector2(Random.Range(-jitterFactor, jitterFactor), 0f));
        Destroy(en.gameObject, 9f/moveSpeed);
        en.SetEnemy(stats, false);
        en.shooting.StartShooting(Functions.RepeatAction(
            () =>
            {
                Bullet bul = Patterns.ShootStraight(GameManager.gameData.smallRoundBullet.GetItem(subwave.type), bulletDmg, en.transform.position, 0, 0, null);
                ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > delayBeforeMoving);
                trigger.OnTriggerEvent += movement =>
                {
                    bul.movement.AccelerateTowards(bulletAccel, GameManager.playerPosition, bulletSpeed);
                    movement.ResetTriggers();
                };
                bul.movement.triggers.Add(trigger);

            }, shotRate
            ));
            yield return new WaitForSeconds(spawnRate);
        }
            
    }
}
