using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossHealthBar : HealthBar
{
    [SerializeField] TextMeshProUGUI numberOfLifeText;
    public BossHealth bosshealth;
    private void Awake()
    {
        GameManager.bossHealthBar = this;
    }

    public override void Update()
    {
        base.Update();
        numberOfLifeText.text = "Lives: " + bosshealth.numberOfLifesLeft.ToString();
    }
}
