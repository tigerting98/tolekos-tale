﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave1 : EnemyWave
{
    // Start is called before the first frame update
    [SerializeField] Vector2 startPosition = default;
    [SerializeField] Vector2 endPosition = default;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float shotRate = 0.5f;
    [SerializeField] int number = 5;
    [SerializeField] float spawnRate = 0.5f;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float spreadAngle = 5f;
    [SerializeField] int shotLines = 1;


    public override void SpawnWave() {

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < number; i++)
        {
            Enemy enemy = Instantiate(enemies[0], startPosition, Quaternion.identity);
            float time = enemy.movement.MoveTo(endPosition, moveSpeed);
            enemy.DestroyAfter(time);
            enemy.shooting.StartShooting(EnemyPatterns.ShootAtPlayerWithLines(bullets[0], enemy.transform, bulletSpeed, shotRate, spreadAngle, shotLines));
            yield return new WaitForSeconds(spawnRate);
        }
        Destroy(gameObject);
    }
}
