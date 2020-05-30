using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class EnemyWave3 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] float startY = 4.5f;
    [SerializeField] float endY = 3f;
    [SerializeField] int number = 10;
    [SerializeField] float width = 3.5f;
    [SerializeField] float moveInSpeed = 3f;
    [SerializeField] float moveOutSpeed = 1f;
    [SerializeField] float shotRate = 0.05f;
    [SerializeField] float pulseTime = 1f;
    [SerializeField] float pause = 1f;
    [SerializeField] float pulses = default;
    [SerializeField] float angularVel = 1f;
    [SerializeField] float amp = 0.2f;
    [SerializeField] float bulletSpeed = 2f;
    [SerializeField] float spread = 5f;



    public override void SpawnWave() {

        for (int i = 0; i < number; i++)
        {
            float X = -width + i * width * 2 / (number - 1);
            Enemy enemy = Instantiate(enemies[0], new Vector2(X, startY), Quaternion.identity);
            StartCoroutine(EnemyActions(enemy, X));
            

        }
    }

    IEnumerator EnemyActions(Enemy enemy, float X)
    {
        float time = enemy.movement.MoveTo(new Vector2(X, endY), moveInSpeed);
        yield return new WaitForSeconds(time);
        
        for (int i = 0; i < pulses; i++) {
            if (enemy)
            {
                float angle = Patterns.AimAt(enemy.transform.position, GameManager.playerPosition);
                enemy.shooting.StartShootingFor(
                    EnemyPatterns.ShootSine(bullets[0], enemy.transform, angle, bulletSpeed, shotRate, angularVel, amp)
                    , 0, pulseTime);
                enemy.shooting.StartShootingFor(
                    EnemyPatterns.ShootSine(bullets[0], enemy.transform, angle - spread, bulletSpeed, shotRate, angularVel, amp)
                    , 0, pulseTime);

                enemy.shooting.StartShootingFor(
                    EnemyPatterns.ShootSine(bullets[0], enemy.transform, angle + spread, bulletSpeed, shotRate, angularVel, amp)
                    , 0, pulseTime);

                AudioSource.PlayClipAtPoint(bulletSpawnSound, GameManager.mainCamera.transform.position, audioVolume);
                yield return new WaitForSeconds(pulseTime + pause);
            }
        }
        if (enemy) {

            StartCoroutine(MoveAwayAfter(enemy, new Vector2(X, startY), moveOutSpeed, 0));
        
         }
    }

    
    IEnumerator MoveAwayAfter(Enemy enemy, Vector2 end, float speed, float time) {
        yield return new WaitForSeconds(time);
        if (enemy)
        {
            float sec = enemy.movement.MoveTo(end, speed);

            yield return new WaitForSeconds(sec);
        }
            if (enemy)
            { Destroy(enemy.gameObject); }
        

        
    }
}
