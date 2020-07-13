using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
[System.Serializable]
public struct Stage5Wave2SubWave {
    public int number;
    public float minY;
    public float maxY;
    public float spawnrate;
    public float stopX;
    public float delaybeforemoving;
    public float moveSpeed;
    public float delay;
    public bool left;
    public bool up;
    public DamageType type;
}
public class Stage5Wave2 : EnemyWave
{
    [SerializeField] EnemyStats stats;
    public List<Stage5Wave2SubWave> subwaves;
     Enemy water, earth, fire;
    public float speed, speeddiff;
    public int shotnumber;
    public float dmg;
    // Start is called before the first frame update
    public override void SpawnWave()
    {
        water = GameManager.gameData.watercandle;
        earth = GameManager.gameData.pottedplant;
        fire = GameManager.gameData.candelabra;
        foreach (Stage5Wave2SubWave subwave in subwaves) {
            StartCoroutine(SpawnSubWave(subwave));
        }
    }
    public IEnumerator SpawnSubWave(Stage5Wave2SubWave subwave) {
        yield return new WaitForSeconds(subwave.delay);
        for (int i = 0; i < subwave.number; i ++)
        {
            Enemy en = subwave.type == DamageType.Earth ? earth : subwave.type == DamageType.Water ? water : fire;
            Bullet bul = subwave.type == DamageType.Earth ? GameManager.gameData.leafBullet2 : 
                subwave.type == DamageType.Water ? GameManager.gameData.icicle  : GameManager.gameData.fireBall;
            StartCoroutine(SpawnEnemy(en, bul, subwave.minY + (subwave.up? subwave.number - 1-i: i)*(subwave.maxY - subwave.minY)/(subwave.number-1), subwave));
            yield return new WaitForSeconds(subwave.spawnrate);

        }
    }
    public IEnumerator SpawnEnemy(Enemy enemy, Bullet shootingBullet, float y, Stage5Wave2SubWave subwave) {
       Enemy en = Instantiate(enemy, new Vector2(subwave.left ? -4.3f : 4.3f, y), Quaternion.identity);
        en.SetEnemy(stats, false);
       float time = en.movement.MoveTo(new Vector2(subwave.stopX, y), subwave.moveSpeed);
        yield return new WaitForSeconds(time);
        if (en) {
            Patterns.BulletSpreadingOut(shootingBullet, dmg, en.transform.position, speed, speeddiff, Functions.AimAtPlayer(en.transform), shotnumber, null);
        }
        yield return new WaitForSeconds(subwave.delaybeforemoving);
        if (en) {
            float time2 =en.movement.MoveTo(new Vector2(subwave.left ? 4.3f : -4.3f, y), subwave.moveSpeed);
            Destroy(en.gameObject, time2);
        }

    
    }
}
