using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyWave4 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] Vector2 startPosition = default;
    [SerializeField] float outY = 4.2f;
    [SerializeField] int number = 10;
    [SerializeField] float radius = default;
    [SerializeField] float moveCircleSpeed = 3f;
    [SerializeField] float moveOutSpeed = 0.1f;
    [SerializeField] float warningTime = 0.2f;
    [SerializeField] float laserTime = 1f;
    [SerializeField] float pause = 1f;
    [SerializeField] Bullet actualLaser = default;
    [SerializeField] Bullet warningLaser = default;
    [SerializeField] int lines = 10;
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] float shotRate = 1f;



    public override void SpawnWave() {
        

        for (int i = 0; i < number; i++)
        {
            float angle = i * 360 / number;
            Enemy enemy = Instantiate(enemies[0], startPosition, Quaternion.identity);
           
            StartCoroutine(EnemyActions(enemy, angle));

        }
        Destroy(gameObject, 30f);
    }

    IEnumerator EnemyActions(Enemy enemy, float angle)
    {
        float time1 = enemy.movement.MoveTo(startPosition + new Polar(radius, angle).rect, moveCircleSpeed);
        yield return new WaitForSeconds(time1);
        if (enemy) {
            enemy.movement.StartMoving();
            enemy.movement.SetSpeed(new Vector2(0, moveOutSpeed));
            enemy.shooting.StartShooting(EnemyPatterns.PulsingBulletsRandom(bulletPack.bullets, enemy.transform, bulletSpeed, shotRate, lines));
        }
        while (enemy) { 
                if (enemy.transform.position.y > outY)
                {
                    Destroy(enemy.gameObject);
                }
                else
                {
                    float playerAngle = Patterns.AimAt(enemy.transform.position, GameManager.playerPosition);
                enemy.shooting.StartShooting(EnemyPatterns.ShootLaserBeam(actualLaser, warningLaser, enemy.transform, playerAngle, warningTime, laserTime));
                    yield return new WaitForSeconds(warningTime);
                    AudioSource.PlayClipAtPoint(bulletSpawnSound, GameManager.mainCamera.transform.position, audioVolume);
                    
                }
            yield return new WaitForSeconds(laserTime + pause);
        }
        
    }



}
