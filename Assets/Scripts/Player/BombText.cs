using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(TextMeshProUGUI))]
//This class displays the number of bombs a player have in the UI
public class BombText : MonoBehaviour
{
    [SerializeField] int maxBombs;
    [SerializeField] GameObject bombSprite;
    [SerializeField] GameObject bombLocation;
    List<GameObject> bombSprites;
    //Initialise all the bombs sprites and deactivate the currently unused ones
    void Start()
    {
        bombSprites = new List<GameObject>();
        for (int i = 0; i < maxBombs; i++)
        {
            GameObject bomb = Instantiate(bombSprite, bombLocation.transform);
            bombSprites.Add(bomb);
        }
        UpdateBomb();
        PlayerStats.OnUpdateBomb += UpdateBomb;
    }

    //Checks to see which bomb sprite need to be set visible
    void UpdateBomb()
    {
       
        for (int i = 0; i < maxBombs; i++) {
            bombSprites[i].SetActive(i < PlayerStats.bombCount);
        }

    }

    private void OnDestroy()
    {
        PlayerStats.OnUpdateBomb -= UpdateBomb;
    }
}
