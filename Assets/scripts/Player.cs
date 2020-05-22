using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] float shotRate;
    [SerializeField] float bulletSpeed;
    [Range(0,10)][SerializeField] float speed;
    [SerializeField] float focusRatio = 0.2f;
    [SerializeField] float xPadding= 0.4f, yPadding=0.4f;
    [SerializeField] GameObject hitbox;
    float xMin, xMax, yMin, yMax;
    [SerializeField] List<Bullet> bullets;
    Coroutine firing;
    List<Func<IEnumerator>> firePatterns;
    [SerializeField] SceneLoader loader;
     Health health;
    Death deathEffects;
 
    int fireMode = 0;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        hitbox.SetActive(false);
        deathEffects = GetComponent<Death>();
        firePatterns = new List<Func<IEnumerator>>();
        firePatterns.Add(ShootPattern1);
        firePatterns.Add(ShootPattern2);
        firePatterns.Add(ShootPattern3);
        hitbox.GetComponent<SpriteRenderer>().color = getColor(bullets[0].gameObject);
        SetUpBoundary();
    }

   
        void SetUpBoundary() {
        Camera cam = Camera.main;
        xMin = -4 + xPadding;
        xMax = 4 - xPadding;
        yMin = -4 + yPadding;
        yMax = 4 - yPadding;
        
    }
     Color getColor(GameObject obj) {
        return obj.GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        DamageDealer dmg = obj.GetComponent<DamageDealer>();
        if (dmg != null)
        {
            health.TakeDamage(dmg.GetDamage());
            if (obj.GetComponent<Bullet>()!= null)
            { Destroy(obj); }
        }

        

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        DamageDealer dmg = collision.GetComponent<DamageDealer>();
        if (dmg!= null) {
            health.TakeDamage((int)Mathf.Ceil(dmg.GetDamage() * Time.deltaTime));
        }
    }

    void CheckFocus() {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * focusRatio;
            hitbox.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = speed / focusRatio;
            hitbox.SetActive(false);
        }

    }
    void CheckFiring() {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            firing = StartCoroutine(firePatterns[fireMode]());
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            StopCoroutine(firing);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            fireMode = (fireMode + 1) % firePatterns.Count;
            hitbox.GetComponent<SpriteRenderer>().color = getColor(bullets[fireMode].gameObject);
            if (firing != null)
            {
                StopCoroutine(firing);
            }
            if (Input.GetKey(KeyCode.Z))
            {
                firing = StartCoroutine(firePatterns[fireMode]());

            }

        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckFocus();
        Move();


        CheckFiring();
        CheckDeath();
    }

    public void CheckDeath() {
        if (health.ZeroHP())

        {

            deathEffects.die();
            loader.GameOver();

        }

    }
    
    public IEnumerator ShootPattern1() {
        while (true)
        {
           
            Bullet bul1 = Instantiate(bullets[0], transform.position, Quaternion.identity);
            Bullet bul2 = Instantiate(bullets[0], transform.position, Quaternion.identity);
            Bullet bul3 = Instantiate(bullets[0], transform.position, Quaternion.identity);
            bul1.setSpeed(new Vector2(0, bulletSpeed));
            bul2.setSpeed(bulletSpeed* new Vector2(0.2f, 0.8f));
            bul3.setSpeed(bulletSpeed * new Vector2(-0.2f, 0.8f));
            yield return new WaitForSeconds(1 / shotRate);
        }
    }
    public IEnumerator ShootPattern2()
    {
        while (true)
        {

            Bullet bul1 = Instantiate(bullets[1], transform.position, Quaternion.identity);
            Bullet bul2 = Instantiate(bullets[1], transform.position + new Vector3(1,0,0), Quaternion.identity);
            Bullet bul3 = Instantiate(bullets[1], transform.position - new Vector3(1, 0, 0), Quaternion.identity);
            bul1.setSpeed(new Vector2(0, bulletSpeed));
            bul2.setSpeed(new Vector2(0, bulletSpeed));
            bul3.setSpeed(new Vector2(0, bulletSpeed));
           
            yield return new WaitForSeconds(1 / shotRate);
        }
    }
    public IEnumerator ShootPattern3()
    {
        while (true)
        {

            Bullet bul1 = Instantiate(bullets[2], transform.position, Quaternion.identity);
            
            bul1.setSpeed(new Vector2(0, bulletSpeed));
            
            yield return new WaitForSeconds(1 / shotRate /3);
        }
    }
    public void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + deltaX, xMin, xMax), Mathf.Clamp(transform.position.y + deltaY, yMin, yMax));
    }
}
