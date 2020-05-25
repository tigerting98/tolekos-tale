using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] GameObject hitbox;
    float xMin, xMax, yMin, yMax;
    [SerializeField] List<Bullet> bullets;
    Coroutine firing;
    List<Func<IEnumerator>> firePatterns;
    [SerializeField] Health health;
    [SerializeField] PlayerDeath deathEffects;
 
    int fireMode = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        GameManager.player = this;
    }
    void Start()
    {
        
        hitbox.SetActive(false);
       
        firePatterns = new List<Func<IEnumerator>>();
        firePatterns.Add(() => PlayerPattern.Mode1(bullets[0], this));
        firePatterns.Add(() => PlayerPattern.Mode2(bullets[1], this));
        firePatterns.Add(() => PlayerPattern.Mode3(bullets[2], this));
        hitbox.GetComponent<SpriteRenderer>().color = getColor(bullets[0].gameObject);
        SetUpBoundary();
     
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
       
    }

  
    
   
    public void Move()
    {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * speed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * speed;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x + deltaX, xMin, xMax), Mathf.Clamp(transform.position.y + deltaY, yMin, yMax));
    }
}
