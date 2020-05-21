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
    [SerializeField] float xPadding= 0.6f, yPadding=0.6f;
    [SerializeField] GameObject hitbox;
    float xMin, xMax, yMin, yMax;
    [SerializeField] List<Bullet> bullets;
    Coroutine firing;
    List<Func<IEnumerator>> firePatterns;
    [SerializeField] int hp;
    [SerializeField] SceneLoader loader;
 
    int fireMode = 0;
    // Start is called before the first frame update
    void Start()
    {
        
        hitbox.SetActive(false);
        
        firePatterns = new List<Func<IEnumerator>>();
        firePatterns.Add(ShootPattern1);
        firePatterns.Add(ShootPattern2);
        firePatterns.Add(ShootPattern3);
        hitbox.GetComponent<SpriteRenderer>().color = getColor(bullets[0].gameObject);
        SetUpBoundary();
    }

    void SetUpBoundary() {
        Camera cam = Camera.main;
        xMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = cam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = cam.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = cam.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
        
    }
     Color getColor(GameObject obj) {
        return obj.GetComponent<SpriteRenderer>().color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject obj = collision.gameObject;
        Bullet bul = obj.GetComponent<Bullet>();
        if (bul != null)
        {
            hp -= bul.TakeDamage();
            Destroy(obj);
        }

        if (hp <= 0) {
            loader.StartGame("Defeat");
            Destroy(gameObject);
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed / 3;
            hitbox.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            speed = speed * 3;
            hitbox.SetActive(false);
        }
        Move();
       
        if (Input.GetKeyDown(KeyCode.Z)) {
            firing = StartCoroutine(firePatterns[fireMode]());
        }
        if (Input.GetKeyUp(KeyCode.Z)) {
            StopCoroutine(firing);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            fireMode = (fireMode + 1) % firePatterns.Count;
            hitbox.GetComponent<SpriteRenderer>().color = getColor(bullets[fireMode].gameObject);
            if (firing != null)
            { StopCoroutine(firing); 
            }
            if (Input.GetKey(KeyCode.Z)) {
                firing = StartCoroutine(firePatterns[fireMode]()); 
            
            }
        
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
