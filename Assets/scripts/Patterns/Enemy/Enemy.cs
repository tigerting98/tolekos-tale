using System;
using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Death))]
[RequireComponent(typeof(Movement))]
public class Enemy : MonoBehaviour
{


    public Health health;
    public Death deathEffects;
    public Movement movement;
    public Shooting shooting;
    public DamageTaker damagetaker;
    public Collider2D collider;

    // Start is called before the first frame update

    //set the auto destory boundaries to 10.
    public virtual void Awake()
    {

        movement.destroyBoundary = 10f;
    
    }

    //changes enemy stats given paramters, if the value is <0, it is considered to not change the original
    public Enemy SetEnemyStats(float hp, float dmg, float goldchance, int maxGold, int minGold, int exp) {
        if (health && hp > -1) {
            health.maxHP = hp;

        }
        if (GetComponent<DamageDealer>() && dmg > -1)
        {

            GetComponent<DamageDealer>().damage = dmg;
        }
        if (TryGetComponent(out BasicDroppable droppable)) {
            if (goldchance > -1) {
                droppable.chanceToDropCoins = goldchance;
            }
            if (maxGold > -1)
            {
                droppable.maxGold = maxGold;
            }
            if (goldchance > -1)
            {
                droppable.minGold = minGold;
            }

        }
        if (deathEffects && exp > -1) {
            deathEffects.experience = exp;
        }
        return this;

    }
    public Enemy SetEnemyResistances(float water, float earth, float fire, float pure) {
        if (damagetaker) {
            if (water >= 0) {
                damagetaker.WaterMultiplier = water;
            }
            if (earth >= 0)
            {
                damagetaker.EarthMultiplier = earth;
            }
            if (fire >= 0)
            {
                damagetaker.FireMultiplier = fire;
            }

            if (pure >= 0)
            {
                damagetaker.PureMultiplier = pure;
            }

        }
        return this;
    
    }

    public Enemy SetEnemy(EnemyStats stats, bool changeResist) {
        SetEnemyStats(stats.hp, stats.dmg, stats.goldchance, stats.minGold, stats.maxGold, stats.exp);
        if (changeResist)
        {
            SetEnemyResistances(stats.GetWaterMultiplier(), stats.GetEarthMultiplier(), stats.GetFireMultiplier(), stats.GetPureMultiplier());
        }
        return this;
    }

    public Enemy ChangeEnemySize(float newSize) {
        transform.localScale = newSize * new Vector3(transform.localScale.x, transform.localScale.y);
        return this;
    }

    public virtual void OnDisable()

    {
        GameManager.enemies.Remove(gameObject.GetInstanceID());
    }
    public virtual void OnEnable()
    {
        GameManager.enemies.Add(gameObject.GetInstanceID(), gameObject);
      
    }


    public virtual void Start()
    {
        
        if (!collider) { 
            collider = GetComponent<Collider2D>(); 
        }
        collider.enabled = false;
        Hittable();
    }


    public void DestroyAfter(float seconds) {
        StartCoroutine(DestoryAt(seconds));
    }

    IEnumerator DestoryAt(float seconds) {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Hittable();
        
    }

    void Hittable() {
        if (transform.position.x < 4.2 && transform.position.x > -4.2 && transform.position.y < 4.2 && transform.position.y > -4.2) {
            collider.enabled = true;
        }
    
    }
}
