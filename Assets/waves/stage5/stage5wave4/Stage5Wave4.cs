using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct Stage5Wave4SubWave {
    public float delay;
    public DamageType type;
    public float y;
    public bool left;
}
public class Stage5Wave4 : EnemyWave
{
    [SerializeField] float movespeed, shotrate, delay, bulletspeed, bulletdmg, bulletspread;
    [SerializeField] int lines;
    [SerializeField] EnemyStats stats;
    [SerializeField] List<Stage5Wave4SubWave> subwaves;
    
    public override void SpawnWave() {
        foreach (Stage5Wave4SubWave subwave in subwaves) {
            StartCoroutine(SpawnEnemy(subwave));
        }
        
    }

    IEnumerator SpawnEnemy(Stage5Wave4SubWave subwave) {
        yield return new WaitForSeconds(subwave.delay);
        Enemy en = Instantiate(GameManager.gameData.ghosts.GetItem(subwave.type), new Vector2(subwave.left ? -4.3f : 4.3f, subwave.y), Quaternion.identity);
        float time = en.movement.MoveTo(new Vector2(subwave.left ? 4.1f : -4.1f, subwave.y), movespeed);
        Destroy(en.gameObject, time);
        en.SetEnemy(stats, false);
        en.shooting.StartShooting(Functions.RepeatAction(
            () =>
            {
                Bullet bul = Patterns.ShootStraight(GameManager.gameData.starBullet.GetItem(subwave.type), bulletdmg, en.transform.position, 0, 0, null);
                ActionTrigger<Movement> trigger = new ActionTrigger<Movement>(movement => movement.time > delay);
                trigger.OnTriggerEvent += movement =>
                {
                    Patterns.ShootMultipleStraightBullet(GameManager.gameData.laserBullet.GetItem(subwave.type), bulletdmg,
                    movement.transform.position, bulletspeed, Functions.AimAtPlayer(movement.transform), bulletspread, lines, null);
                    movement.GetComponent<Bullet>().Deactivate();
                };
                bul.movement.triggers.Add(trigger);

            }, shotrate
            ));
            

    }
    
}
