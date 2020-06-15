using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stage1Wave1 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] float y1, y2;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float delay;
    [SerializeField] int number = 5;
    [SerializeField] float spawnRate = 0.5f;


    public override void SpawnWave() {

        StartCoroutine(SpawnEnemy(0, y1, true));
        StartCoroutine(SpawnEnemy(delay, y2, false));
        DestroyAfter(delay + number * spawnRate + 1);
    }

    IEnumerator SpawnEnemy(float delay, float y, bool left)
    {
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < number; i++)
        {
            float initialX = left ? -4.5f : 4.5f;
            Enemy enemy = Instantiate(enemies[0], new Vector2(initialX, y), Quaternion.identity);
            float time = enemy.movement.MoveTo(new Vector2(-initialX, y), moveSpeed);
            enemy.DestroyAfter(time);
            yield return new WaitForSeconds(spawnRate);
        }
        
    }

    
}
