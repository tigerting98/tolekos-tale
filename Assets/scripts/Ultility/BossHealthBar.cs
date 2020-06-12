using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthBar : HealthBar
{
    [SerializeField] GameObject lifeLocation;
    [SerializeField] GameObject lifeImage;
    public BossHealth bosshealth;
    List<GameObject> lifeImages = new List<GameObject>();
    int numberOfLives = 0;
    private void Awake()
    {
        GameManager.bossHealthBar = this;
    }

    public override void Update()
    {
        base.Update();
        int newLifeCount = bosshealth.numberOfLifesLeft;
        if (newLifeCount != numberOfLives) {
            numberOfLives = newLifeCount;
            DrawLives();
        }
    }

    public void DrawLives() {
        for (int i = 0; i < lifeImages.Count; i++) {
            lifeImages[i].SetActive(false);
        }

        for (int i = 0; i < numberOfLives; i++) {
            if (i < lifeImages.Count)
            {
                lifeImages[i].SetActive(true);
            }
            else {
                GameObject newLifeImage = Instantiate(lifeImage, lifeLocation.transform);
                lifeImages.Add(newLifeImage);
            }
        
        }
    
    }
}
