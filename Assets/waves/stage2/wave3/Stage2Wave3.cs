
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering.PostProcessing;

public class Stage2Wave3 : EnemyWave 
{
    // Start is called before the first frame update

    [SerializeField] float stopY, stopX, movespeed1, movespeed2, spawnRate;
    [SerializeField] float ballDmg1, pointedDmg1;
    [SerializeField] float pulseRate, pulseDuration;
    [SerializeField] EnemyStats ghostStat, fairyStat;
    [SerializeField] int numberOfBlobs;
    [SerializeField] EnemyWave wave;
    [Header("Bullet Behavior")]
    [SerializeField] float amp, pointedvel, pointedshotRate, pointedangularvel, ballspeed, ballspread;
    [SerializeField] int pulseballnumber, numberOfPulses;
    [SerializeField] float ballSpeed2, arrowSpeed2, angularvelball, angularvelarrow, shotrateball, shotratearrow, delay, shootingduration;
    [SerializeField] float dmgball2, dmgarrow2;
    Bullet ball, arrow, pointed;

    public override void SpawnWave()
    {
        Instantiate(wave).SpawnWave();
        ball = GameManager.gameData.smallRoundBullet.GetItem(DamageType.Water);
        arrow = GameManager.gameData.arrowBullet.GetItem(DamageType.Water);
        pointed = GameManager.gameData.pointedBullet.GetItem(DamageType.Water);
        StartCoroutine(Functions.RepeatCustomActionXTimes(i => StartCoroutine(SpawnBlob(i % 2 == 0)), spawnRate, numberOfBlobs));
        StartCoroutine(summonFairy(delay + (numberOfBlobs - 1) * spawnRate));

    }

    
    IEnumerator summonFairy(float t) {
        yield return new WaitForSeconds(t);
        Enemy fairy = Instantiate(GameManager.gameData.waterFairy, new Vector2(0, 4.1f), Quaternion.identity);
        fairy.transform.localScale = 1.5f * fairy.transform.localScale;
        fairy.SetEnemy(fairyStat, true);
        float time = fairy.movement.MoveTo(new Vector2(0, stopY), movespeed1);
        yield return new WaitForSeconds(time);
        fairy.shooting.StartShootingFor(EnemyPatterns.ConstantSpinningStraightBullets(arrow, dmgarrow2, fairy.transform, arrowSpeed2, angularvelarrow, 180, 3, shotratearrow), 0, shootingduration);
        fairy.shooting.StartShootingFor(EnemyPatterns.ConstantSpinningStraightBullets(arrow, dmgarrow2, fairy.transform, arrowSpeed2, -angularvelarrow, 0, 3, shotratearrow), 0, shootingduration);
        fairy.shooting.StartShootingFor(EnemyPatterns.ConstantSpinningStraightBullets(ball, dmgball2, fairy.transform, ballSpeed2, angularvelball, -135, 3, shotrateball), 0, shootingduration);
        fairy.shooting.StartShootingFor(EnemyPatterns.ConstantSpinningStraightBullets(ball, dmgball2, fairy.transform, ballSpeed2, -angularvelball, -45, 3, shotrateball), 0, shootingduration);
        yield return new WaitForSeconds(shootingduration);
        if (fairy) {
            fairy.movement.SetSpeed(movespeed1, 90);
        }
        yield return new WaitForSeconds(1f);
        GameManager.SummonEndBoss();
        GameManager.DestoryAllEnemyBullets();
    }
    IEnumerator SpawnBlob(bool left) {


        float x = left ? -stopX : stopX;

        Enemy ghost = Instantiate(GameManager.gameData.ghosts.GetItem(DamageType.Water), new Vector2(x, 4.1f), Quaternion.identity);
        ghost.SetEnemy(ghostStat, false);
        float time = ghost.movement.MoveTo(new Vector2(x, stopY), movespeed1);
        yield return new WaitForSeconds(time);
        if (ghost) {
            ghost.shooting.StartCoroutine(Functions.RepeatActionXTimes(
                () =>
                {
                    float angle = Functions.AimAt(ghost.transform.position, GameManager.playerPosition);
                    Patterns.ShootMultipleStraightBullet(ball, ballDmg1, ghost.transform.position, ballspeed, angle, ballspread, pulseballnumber);
                    ghost.shooting.StartShootingFor(EnemyPatterns.ShootSine(pointed, pointedDmg1,ghost.transform, angle, pointedvel, pointedshotRate, pointedangularvel, amp), 0, pulseDuration);
                }, pulseRate, numberOfPulses
                ));
        }
        yield return new WaitForSeconds((numberOfPulses + 1) * pulseRate);
        if (ghost)
        {
            ghost.movement.SetSpeed(movespeed2, Functions.AimAt(ghost.transform.position, GameManager.playerPosition)); 
        }




    }
}
