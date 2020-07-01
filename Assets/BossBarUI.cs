using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarUI : MonoBehaviour
{
    // Start is called before the first frame update

    static BossBarUI bossBar;
    static float originalScaleX = 7.8f;
    static float originalScaleY =  0.05f;
    // Start is called before the first frame update
    private void Awake()
    {
        if (bossBar == null)
        {
            bossBar = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    Vector2 Scaled(float ratio) {
        return new Vector2(originalScaleX * ratio, originalScaleY);
    }
    void Start()

    {
        bossBar.transform.localScale = Scaled(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.currentBoss)
        {
            bossBar.transform.localScale = Scaled(0);
        }
        else
        {
            bossBar.transform.localScale = Scaled(GameManager.currentBoss.bosshealth.GetCurrentHP()/GameManager.currentBoss.bosshealth.maxHP);
        }
    }
}
