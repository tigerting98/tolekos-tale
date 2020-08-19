using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;
using System;
//This class is responsible for the healthbar UI element
public class HealthBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text = default;
    [SerializeField] Slider slider = default;
    [SerializeField] protected Health health = default;
    [SerializeField] protected DamageTaker taker = default;
    [SerializeField] bool visible = true;
    [SerializeField] TextMeshProUGUI title = default;
    [SerializeField] TextMeshProUGUI waterResistText;
    [SerializeField] TextMeshProUGUI earthResistText;
    [SerializeField] TextMeshProUGUI fireResistText;
    [SerializeField] DamageText dmgTaker;
    float currentHP = 0;
    float lastKnownMax = 0;
 
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (!visible) {
            gameObject.SetActive(false);
        }
   

    }
    public void TakeDPSDmg(DamageType type, float dmg) {
        try
        {
            DamageText text = Instantiate(dmgTaker, (Vector2)transform.position + new Vector2(150f, 25f), Quaternion.identity, transform);
            text.SetColor(type);
            text.SetText(dmg);

            Movement movement = text.GetComponent<Movement>();
            movement.SetSpeed(50f, 90);

            Destroy(text.gameObject, 2.1f);
        }
        catch(Exception ex) {
            UnityEngine.Debug.Log(ex);
        }
    }
    public void TakeNormalDmg(DamageType type, float dmg)
    {
        try
        {
           DamageText text = Instantiate(dmgTaker, (Vector2)transform.position + new Vector2(0f, 25f), Quaternion.identity, transform);
            text.SetColor(type);
            text.SetText(dmg);

            Movement movement = text.GetComponent<Movement>();
            movement.SetSpeed(50f, 90);

            Destroy(text.gameObject, 2.1f);
        }
        catch (Exception ex) {
            UnityEngine.Debug.Log(ex);
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
        hp.OnHeal += (heal) => TakeNormalDmg(DamageType.Pure, -heal);

        

    }
    public void SetTaker(DamageTaker taker) {
        this.taker = taker;
        this.taker.OnNormalDmgTaken += TakeNormalDmg;
        this.taker.OnDPSDmgTaken += TakeDPSDmg;

    }

    public void SetResist() {
        waterResistText.text = ((int)(taker.WaterMultiplier*100)).ToString() + "%";
        earthResistText.text = ((int)(taker.EarthMultiplier * 100)).ToString() + "%";
        fireResistText.text = ((int)(taker.FireMultiplier * 100)).ToString() + "%";
    }
    public virtual void Update()
    {
        lastKnownMax = health == null ? lastKnownMax : health.maxHP;
        currentHP = health == null ? 0 : health.GetCurrentHP();
        currentHP = currentHP < 0 ? 0 : currentHP;
        text.text = "Health : " + ((int)currentHP).ToString() + "/" + lastKnownMax.ToString();
        slider.value = health.maxHP == 0 ? 0 : currentHP / health.maxHP;
        SetResist();
    }
  
    
}
