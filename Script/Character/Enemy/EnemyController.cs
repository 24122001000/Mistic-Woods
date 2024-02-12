using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour,ICharacterHealth
{
    private float speed = 0f;
    private int maxHealth = 5;
    private float backForce = 300f;
    private int currentHealth;

    Animator ani;
    SpriteRenderer sr;
    Rigidbody2D rb;
    DetectedZone dt;

    Vector2 direction = new Vector2(1,0);
    public int damage = 1;
    bool isAlive = true;
    bool isHited = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        ani = GetComponent<Animator>();
        dt = GetComponentInChildren<DetectedZone>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        SetAni();
        IsDead();
    }

    void FixedUpdate()
    {
        EnemyMove();
        EnemyBack();
    }
    private void SetAni()
    {
        ani.SetFloat("Speed", speed);
        ani.SetBool("isAlive", isAlive);
        ani.SetBool("isHited", isHited);
    }
    private void EnemyMove()
    {
        if (dt.enemies.Count > 0)
        {
            speed = 70f;
            direction = (dt.enemies[0].transform.position - transform.position).normalized;
            EnemyDirection();
        }
        else
        {
            speed = 0f;
        }
        rb.AddForce(direction * speed * Time.deltaTime, ForceMode2D.Force);

    }
    private void EnemyDirection()
    {
        if (direction.x < 0)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
    private void EnemyBack()
    {
        if (isHited)
        {
            rb.AddForce(-direction * backForce * Time.deltaTime);
        }  
    }
    public void IsHited()
    {
        isHited = false;
    }
    public void IsDead()
    {
        if(currentHealth <= 0)
        {
            isAlive = false;
            rb.simulated = false;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            isHited = true;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if(collision.gameObject.tag == "Player")
        {
            player.ChangeHealth(-damage);
        }
    }
}
