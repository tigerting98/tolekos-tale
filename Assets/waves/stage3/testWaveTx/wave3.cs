using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave3 : EnemyWave
{
    [SerializeField] float y1, y2;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float delay;
    [SerializeField] int number = 5;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] EnemyStats stats;
    [SerializeField] float shootingDelay = 0.3f;
    [SerializeField] float bulletSpeed = 0.5f;
    [SerializeField] float shotRate = 0.2f;
    [SerializeField] float shotAngle = 20f;
    [SerializeField] int lines = 1;
    [SerializeField] int bulletsPerLine = 10;
    [SerializeField] float damage = 10f;
    
    Enemy enemy;

    public override void SpawnWave()
    {
        Functions.StartMultipleCustomCoroutines(this, i => SingleEnemy(moveSpeed, 0, new Vector2(-4.1f, 3f), new Vector2(0f, 1.5f), new Vector2(-4.1f, 0f)), 5, spawnRate);
        Functions.StartMultipleCustomCoroutines(this, i => SingleEnemy(moveSpeed, 0, new Vector2(4.1f, 3f), new Vector2(0f, 1.5f), new Vector2(4.1f, 0f)), 5, spawnRate);
        // StartCoroutine(SingleEnemy(true, moveSpeed, 0, new Vector2(-4.1f, 3f), new Vector2(0f, 1.5f), new Vector2(-4.1f, 0f)));
    }

    public IEnumerator SingleEnemy(float speed, float firingDelay, Vector2 start, Vector2 mid, Vector2 end)
    {
        enemy = GameManager.gameData.linemonster.GetItem(DamageType.Earth);
        Enemy en = Instantiate(enemy, start, Quaternion.identity);
        en.SetEnemy(stats, false);
        
        // en.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayerWithLines(GameManager.gameData.arrowBullet.GetItem(DamageType.Earth), 10f, en.transform, bulletSpeed, shotRate, shotAngle, lines), shootingDelay, bulletsPerLine * shotRate);
        float timeToMid = en.movement.MoveTo(mid, speed);

        yield return new WaitForSeconds(shootingDelay);
        if(en) {
            float bulletSpeed = this.bulletSpeed;
            float angle = Patterns.AimAt(en.transform.position, GameManager.playerPosition);
            for (int i = 0; i < bulletsPerLine; i++) {
                Patterns.ShootMultipleStraightBullet(GameManager.gameData.leafBullet1, damage, en.transform.position, bulletSpeed, angle, shotAngle, 1);
                bulletSpeed += 0.5f; 
            }
        } 

        yield return new WaitForSeconds(timeToMid - shootingDelay);

        if (en) 
        {
            float timeToEnd = en.movement.MoveTo(end, speed);
    
            yield return new WaitForSeconds(timeToEnd);

            if(en)
            {
            Destroy(en.gameObject);
            }
        }
    }
}
