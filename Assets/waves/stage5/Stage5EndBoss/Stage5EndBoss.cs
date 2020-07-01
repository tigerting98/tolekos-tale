using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage5EndBoss : EnemyBossWave
{
    [SerializeField] Stage5EndBossUI ui;
    [SerializeField] Stage5Boss waterprefab, earthprefab, fireprefab;
    [SerializeField] Stage5Boss waterBoss, earthBoss, fireBoss;
    Stage5EndBossUI bossUI;
    public override void SpawnWave() {
        SetUp();
        waterBoss = Instantiate(waterprefab, new Vector2(0, 0), Quaternion.identity);
        waterBoss.SetUpBossHealthbar(bossUI.waterHealthBar);
        earthBoss = Instantiate(earthprefab, new Vector2(-1, 0), Quaternion.identity);
        earthBoss.SetUpBossHealthbar(bossUI.earthHealthBar); 
        fireBoss = Instantiate(fireprefab, new Vector2(1, 0), Quaternion.identity);
        fireBoss.SetUpBossHealthbar(bossUI.fireHealthBar);
        

    }

    public void SetUp() {
        GameObject obj = GameObject.Find("Canvas");
        bossUI = Instantiate(ui, obj.transform);
        bossUI.transform.localPosition = new Vector2(622, 294);
    }
}
