using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class encompass the behavior for the boss circular hp bar
public class BossBarUI : MonoBehaviour
{
    [SerializeField] BossHealth health;
    [SerializeField] Image hpbar;
    [SerializeField] Image emptyhpbar;
    [SerializeField] float reducedOpacity;
    [SerializeField] float radiusbeforeReducing = 1.5f;
    private void Awake()
    {
        gameObject.GetComponent<Canvas>().worldCamera = GameManager.maincamera.GetComponent<Camera>();
        if (!health) {
            health = transform.parent.GetComponent<BossHealth>();
        }
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
        if (health) {
            hpbar.fillAmount = health.GetCurrentHP() / health.maxHP;
       }
        if ((((Vector2)transform.position) - GameManager.playerPosition).magnitude < radiusbeforeReducing)
        {
            ChangeOpacity(reducedOpacity);
        }
        else {
            ChangeOpacity(1);
        }
    }
    //Changes transparency if the player is near the boss
    void ChangeOpacity(float opacity) {
        Color hp = hpbar.color;
        hp.a = opacity;
        hpbar.color = hp;
        Color empty = emptyhpbar.color;
        empty.a = opacity;
        emptyhpbar.color = empty;
    }
}

