using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    [SerializeField] Text text = default;
    [SerializeField] Slider slider = default;
    [SerializeField] Health health = default;
    [SerializeField] bool visible = true;
    [SerializeField] Text title = default;
    float currentHP = 0;
    float lastKnownMax = 0;
 
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
        lastKnownMax = hp.maxHP;
    }
    void Update()
    {
        lastKnownMax = health == null ? lastKnownMax : health.maxHP;
        currentHP = health == null ? 0 : health.GetCurrentHP();
        text.text = "Health : " + ((int)currentHP).ToString() + "/" + lastKnownMax.ToString();
        slider.value = health.maxHP == 0 ? 0 : currentHP / health.maxHP;
    }

    
    
}
