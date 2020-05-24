using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Slider slider;
    [SerializeField] Health health;
    [SerializeField] bool visible = true;
    [SerializeField] Text title;
    int maxHP = 0;
    int currentHP = 0;
 
    // Start is called before the first frame update
    void Start()
    {
        if (!visible) {
            gameObject.SetActive(false);
        }
        if (health!=null)
        { maxHP = health.GetMaxHP(); }

    }

    public void SetVisible() {
        visible = true;
        gameObject.SetActive(true);
    }

    public void SetTitle(string str) {
        title.text = str;
    }
    public void SetInvisible() {
        visible = false;
        gameObject.SetActive(false);
    }
    // Update is called once per frame

    public void SetHealth(Health hp) {
        health = hp;
        maxHP = hp.GetMaxHP();
        currentHP = hp.GetCurrentHP();
    }
    void Update()
    {
        currentHP = health == null ? 0 : health.GetCurrentHP();
        text.text = "Health : " + currentHP.ToString() + "/" + maxHP.ToString();
        slider.value = maxHP == 0 ? 0 : (float)currentHP / (float)maxHP;
    }

    
    
}
