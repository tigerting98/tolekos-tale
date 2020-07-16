using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;
[System.Serializable]
public struct Stage5Wave3SubWave {
    public Vector2 centre;
    public float spawnRate;
    public int number;
    public float radius;
    public float radialvel;
    public float angularvel;
    public bool clockwise;
    public float offset;
    public float delay;
    public bool summonBoss;
}
public class Stage5Wave3 : EnemyWave
{
    [SerializeField] bool harder;
    [SerializeField] float hardershotrate, harderspeed, harderdmg;
    [SerializeField] float pulseRate, shotSpeed;
    [SerializeField] int bulletsperPulse;
    [SerializeField] float shotdmg;
    [SerializeField] float shootingDuration;
    [SerializeField] EnemyStats stats;
    [SerializeField] List<Stage5Wave3SubWave> subwaves;
    [SerializeField] float bossdelay;
    
    public override void SpawnWave()
    {
        foreach (Stage5Wave3SubWave wave in subwaves) {
            StartCoroutine(SpawnEnemy(wave));
        }
    }

    IEnumerator SpawnEnemy(Stage5Wave3SubWave wave) {
        yield return new WaitForSeconds(wave.delay);
        for (int i = 0; i < wave.number; i++)
        {
            float startangle = (wave.clockwise? -1:1)*i * 360f / wave.number + wave.offset;
            Enemy en = Instantiate(GameManager.gameData.flyingBook, wave.centre + new Polar(wave.radius,startangle).rect , Quaternion.identity);
            en.SetEnemy(stats, false);
            en.shooting.StartShootingFor(EnemyPatterns.PulsingBulletsRandomAngle(GameManager.gameData.pageBullet, shotdmg, en.transform, shotSpeed, 
                pulseRate * (wave.summonBoss?0.76f:1), bulletsperPulse, null), 0.6f, shootingDuration);
            if (harder) {
                en.shooting.StartShootingFor(EnemyPatterns.ShootAtPlayer(GameManager.gameData.ellipseBullet.GetItem(DamageType.Pure), harderdmg, en.transform, harderspeed, hardershotrate, null)
                    ,0.6f, shootingDuration);
            }
            en.movement.destroyBoundary = 5f;
            en.movement.SetPolarPath(t => new Polar(wave.radius + t * wave.radialvel, startangle + (wave.clockwise ? 1 : -1) * wave.angularvel*t));
            yield return new WaitForSeconds(wave.spawnRate);
        }
        if (wave.summonBoss)
        { yield return new WaitForSeconds(bossdelay);
            GameManager.DestoryAllEnemyBullets();
            GameManager.DestroyAllNonBossEnemy(false);
            GameManager.SummonMidBoss();
        }
     }
}
