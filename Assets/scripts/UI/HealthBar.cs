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
    float currentHP = 0;
 
    // Start is called before the first frame update
    public void Start()
    {
        if (!visible) {
            gameObject.SetActive(false);
        }
   

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
        currentHP = hp.GetCurrentHP();
    }
    void Update()
    {
        int max = health == null ? 0 : (int)health.maxHP;
        currentHP = health == null ? 0 : health.GetCurrentHP();
        text.text = "Health : " + ((int)currentHP).ToString() + "/" + max.ToString();
        slider.value = health.maxHP == 0 ? 0 : currentHP / health.maxHP;
    }

    
    
}
