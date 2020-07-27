using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This class is used for easy testing of a level
public class LevelTester : LevelLoader
{
    [SerializeField] int playerLevel = 1;
    [SerializeField] Difficulty difficulty = Difficulty.Normal;
    bool uped = false;
    public void Update()
    {
        if (!uped)
        {
            for (int i = 1; i < playerLevel; i++)
            {

                PlayerStats.LevelUp();

            }
            PlayerStats.GainEXP(1);
        }
        uped = true;
    }
    
    protected override void Awake()
    {
        GameManager.difficultyLevel = difficulty;
        base.Awake();
    }

}
