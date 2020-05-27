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
    public float shotRate;
    public float bulletSpeed;
    [Range(0,10)] public float speed= 5f;
    [SerializeField] float focusRatio = 0.2f;
    [SerializeField] float xPadding= 0.4f, yPadding=0.4f;
    [SerializeField] GameObject hitbox = default;
    float xMin, xMax, yMin, yMax;
    [SerializeField] BulletPack bulletPack = default;
    bool isLaser = false;
    public bool isFocus = false;
    bool isFiring = false;
    float firingCoolDown = 0;
    SpriteRenderer hitsprite = default;
    [SerializeField] Health health;
    [SerializeField] PlayerDeath deathEffects;
    public float currentSpeed = 5f;
    Bullet waterBullet, earthBullet, fireBullet;
    [SerializeField] Bullet laser;
    [SerializeField] float laserDamageRatio = 20f;


    int fireMode = 0;
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
        laser.gameObject.SetActive(false);
        SetUpBullets();
        Level();
        hitsprite = hitbox.GetComponent<SpriteRenderer>();
        hitsprite.color = getColor(waterBullet.gameObject);
        SetUpBoundary();
     
    }

    void SetUpBullets() {
        waterBullet = bulletPack.bullets[0];    
        earthBullet = bulletPack.bullets[1];
        fireBullet = bulletPack.bullets[2];

    }

    public void Level()
    {
        SetPlayerBulletDamage(PlayerStats.damage);
        health.IncreaseMaxHP(PlayerStats.playerMaxHP - health.maxHP);
    }

    public void SetPlayerBulletDamage(float dmg) {
        waterBullet.damageDealer.damage = dmg;
        fireBullet.damageDealer.damage = dmg;
        earthBullet.damageDealer.damage = dmg;
        laser.damageDealer.damage = dmg * laserDamageRatio;
    }
   
        void SetUpBoundary() {
       
        xMin = -4 + xPadding;
        xMax = 4 - xPadding;
        yMin = -4 + yPadding;
        yMax = 4 - yPadding;
        
    }
     Color getColor(GameObject obj) {
        return obj.GetComponent<SpriteRenderer>().color;
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
        if (Input.GetKeyDown(KeyCode.C)) {
            fireMode = (fireMode + 1) % 3;
            hitsprite.color = getColor(bulletPack.bullets[fireMode].gameObject);
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
        bool onLaser = false;
        if (firingCoolDown > 0)
        {
            firingCoolDown -= Time.deltaTime;

        }
        else {
            if (isFiring) {
                if (fireMode == 0)
                {
                    PlayerPattern.Mode1(waterBullet, this);
                    firingCoolDown += shotRate;
                }
                else if (fireMode == 1)
                {
                    PlayerPattern.Mode2(earthBullet, this);
                    firingCoolDown += shotRate;
                }
                else {
                    if (!isFocus)
                    {
                        PlayerPattern.Mode3(fireBullet, this);
                        firingCoolDown += shotRate / 3;
                    }
                    else {
                        onLaser = true;
                    }
                }
            }
        }
        if (isLaser != onLaser) {
          
            switchLaser();
        }
    }

    
    void switchLaser() {
        isLaser = !isLaser;
        laser.gameObject.SetActive(isLaser);
    }
    // Update is called once per frame
    void Update()
    {
        
        CheckInputs();
        CheckFocus();
        Move();
        CheckFiring();
        GameManager.playerPosition = transform.position;

       
    }

  
    
   
    public void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * currentSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * currentSpeed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + deltaX, xMin, xMax), Mathf.Clamp(transform.position.y + deltaY, yMin, yMax));
    }
}
