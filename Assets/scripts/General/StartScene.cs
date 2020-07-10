using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        PlayerStats.Reset();
        GameManager.Reset();
    }

    public void SelectDifficulty(Difficulty difficulty) {
        GameManager.difficultyLevel = difficulty;
    }
    public void SelectDifficulty(int i) {
        SelectDifficulty((Difficulty)i);
    }
}
