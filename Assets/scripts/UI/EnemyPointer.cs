using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this controls the boss indicator pointer at the bottom of the screen during boss fight
public class EnemyPointer : MonoBehaviour
{
    SpriteRenderer sprite;
    static EnemyPointer pointer;

    // Start is called before the first frame update
    private void Awake()
    {
        if (pointer == null)
        {
            pointer = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else {
            Destroy(this.gameObject);
        }
    }
    void Start()

    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.currentBoss)
        {
            sprite.enabled = true;
            transform.position = new Vector2(GameManager.currentBoss.transform.position.x, transform.position.y);
        }
        else {
            sprite.enabled = false;
        }
    }
}
