using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
//using System.Numerics;

using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Death))]
public class Player : MonoBehaviour
{
    
    public float bulletSpeed;
    [Range(0,10)] public float speed= 5f;
    [SerializeField] float focusRatio = 0.2f;
    [SerializeField] float xPadding= 0.4f, yPadding=0.4f;
    public GameObject hitbox = default;
    public DamageTaker damageTaker = default;
    float xMin, xMax, yMin, yMax;
    bool isFireFocus = false, isFireUnfocus = false;
    public bool isFocus = false;
    bool isFiring = false;
    float firingCoolDown = 0;
    SpriteRenderer hitsprite = default;
    public Health health;
    [SerializeField] PlayerDeath deathEffects;
    public float currentSpeed = 5f;
    public Color water, earth, fire;
    public Bullet waterBullet, earthFocusBullet, earthUnfocusBullet;
    [SerializeField] Bullet fireFocus = default, fireUnfocus = default;
    public event Action ActivateSpell;
    public event Action ChangeMode;
    public DamageType mode;
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.player = this;
        PlayerStats.player = this;
    }
    void Start()
    {
        GameManager.playerPosition = transform.position;
        hitbox.SetActive(false);
        fireFocus.gameObject.SetActive(false);
        fireUnfocus.gameObject.SetActive(false);
        hitsprite = hitbox.GetComponent<SpriteRenderer>();
        SetUp();
        mode = DamageType.Water;

    }

    void SetUp() {
 

        health.maxHP = PlayerStats.playerMaxHP;
        health.ResetHP();
        SetDamageType();
        SetUpBoundary();

    }

    public void Level()
    {
        SetPlayerBulletDamage(PlayerStats.damage);
        health.IncreaseMaxHP(PlayerStats.playerMaxHP - health.maxHP);
    }

    public void SetPlayerBulletDamage(float dmg) {
        waterBullet.damageDealer.damage = dmg;

        earthUnfocusBullet.damageDealer.damage = dmg;
        earthFocusBullet.damageDealer.damage = dmg*PlayerStats.earthFocusDaamgeRatio;
        fireFocus.damageDealer.damage = dmg * PlayerStats.fireFocusDamageRatio;
        fireUnfocus.damageDealer.damage = dmg * PlayerStats.fireUnfocusDamageRatio;
    }
   
        void SetUpBoundary() {
       
        xMin = -4 + xPadding;
        xMax = 4 - xPadding;
        yMin = -4 + yPadding;
        yMax = 4 - yPadding;
        
    }

    void CheckInputs() {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isFocus = true;
        }
        else {
            isFocus = false;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            isFiring = true;
        }
        else {
            isFiring = false;
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            if (mode == DamageType.Water) {
                mode = DamageType.Earth;
            }
            else if (mode == DamageType.Earth)
            {
                mode = DamageType.Fire;
            }
            else
            {
                mode = DamageType.Water;
            }
            SetDamageType();
            ChangeMode?.Invoke();

        }
        if (Input.GetKeyDown(KeyCode.C)) {
            ActivateSpell?.Invoke();
        }
    }

    void SetDamageType() {
        if (mode == DamageType.Water) {
            hitsprite.color = water;
            damageTaker.WaterMultiplier = 1;
            damageTaker.EarthMultiplier = 2;
            damageTaker.FireMultiplier = 0.5f;
        }
        else if (mode == DamageType.Earth)
        {
            hitsprite.color = earth;
            damageTaker.WaterMultiplier = 0.5f;
            damageTaker.EarthMultiplier = 1;
            damageTaker.FireMultiplier = 2;
        }
        if (mode == DamageType.Fire)
        {
            hitsprite.color = fire;
            damageTaker.WaterMultiplier = 2;
            damageTaker.EarthMultiplier = 0.5f;
            damageTaker.FireMultiplier = 1;
        }
    }
    void CheckFocus() {
        if (isFocus) {
            currentSpeed = speed * focusRatio;
            hitbox.SetActive(true);
        } else {
            currentSpeed = speed;
            hitbox.SetActive(false);
        }

    }
    void CheckFiring() {
        bool onFireFocus = false, onFireUnfocus = false;
        if (firingCoolDown > 0)
        {
            firingCoolDown -= Time.deltaTime;

        }
        else {
            if (isFiring) {
                if (mode == DamageType.Water)
                {
                    if (isFocus)
                    {
                        PlayerPattern.WaterFocusedMode(waterBullet, this);
                        firingCoolDown += PlayerStats.baseShotRate;
                    }
                    else
                    {
                        PlayerPattern.WaterUnfocusedMode(waterBullet, this);
                        firingCoolDown += PlayerStats.baseShotRate;
                    }
                    
                }
                else if (mode == DamageType.Earth)
                {
                    if (isFocus)
                    {
                        PlayerPattern.EarthFocusedMode(earthFocusBullet, this);
                        firingCoolDown += PlayerStats.baseShotRate * PlayerStats.earthFocusedShotRateRatio;
                    }
                    else
                    {
                        PlayerPattern.EarthUnfocusedMode(earthUnfocusBullet, this);
                        firingCoolDown += PlayerStats.baseShotRate;
                    }
                }
                else {
                    if (!isFocus)
                    {
                        onFireUnfocus = true;
                    }
                    else {
                        onFireFocus= true;
                    }
                }
            }
        }
        if (isFireFocus != onFireFocus) {

            isFireFocus = !isFireFocus;
            fireFocus.gameObject.SetActive(isFireFocus);
        }
        if (isFireUnfocus != onFireUnfocus)
        {

            isFireUnfocus = !isFireUnfocus;
            fireUnfocus.gameObject.SetActive(isFireUnfocus);
        }
    }

    
   
    // Update is called once per frame
    void Update()
    {
        
        CheckInputs();
        CheckFocus();
        Move();
        CheckFiring();
        CheckPointOfCollection();
        GameManager.playerPosition = transform.position;

       
    }

    public void CheckPointOfCollection() {
        if (transform.position.y > 2) {
            GameManager.CollectEverything();
        }
    
    }
  
    
   
    public void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * currentSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + deltaX, xMin, xMax), Mathf.Clamp(transform.position.y + deltaY, yMin, yMax));
    }
}
