using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBarUI : MonoBehaviour
{
    [SerializeField] BossHealth health;
    [SerializeField] Image hpbar;
    private void Awake()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameManager.mainCamera;
        if (!health) {
            health = transform.parent.GetComponent<BossHealth>();
        }
    }

    private void Update()
    {
        if (health) {
            hpbar.fillAmount = health.GetCurrentHP() / health.maxHP;
       }
        transform.rotation = Quaternion.identity;
    }
}

