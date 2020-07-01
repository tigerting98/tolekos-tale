using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;

[RequireComponent(typeof(TextMeshProUGUI))]
public class BombText : MonoBehaviour
{
    [SerializeField] int maxBombs;
    [SerializeField] GameObject bombSprite;
    [SerializeField] GameObject bombLocation;
    List<GameObject> bombSprites;

    void Start()
    {
        bombSprites = new List<GameObject>();
        for (int i = 0; i < maxBombs; i++)
        {
            GameObject bomb = Instantiate(bombSprite, bombLocation.transform);
            bombSprites.Add(bomb);
        }
        UpdateBomb();
        PlayerStats.OnUseBomb += UpdateBomb;
    }

    // Update is called once per frame
    void UpdateBomb()
    {
       
        for (int i = 0; i < maxBombs; i++) {
            bombSprites[i].SetActive(i < PlayerStats.bombCount);
        }

    }

    private void OnDestroy()
    {
        PlayerStats.OnUseBomb -= UpdateBomb;
    }
}
