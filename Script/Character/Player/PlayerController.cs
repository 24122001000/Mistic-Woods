using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour,ICharacterHealth
{
    public float speed;
    float currentSpeed;
    float playerSpeed = 0.5f;
    float timeInvincible = 1f;
    int maxHealth = 5;
    public int currentHealth;

    SwordHitBox sword;

    Collider2D col;
    Rigidbody2D rb;
    float horizontal;
    float vertical;
    bool isAlive = true;
    bool attack = false;
    public bool isInvincible = false;
    float invincibleTimer;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sword = GetComponentInChildren<SwordHitBox>();
        col = GetComponent<Collider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        currentSpeed = playerSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        Attack();
        Move();
        Dead();
        IsHited();
        animator.SetBool("isAlive", isAlive);
    }
    private void FixedUpdate()
    {
        Movement();
        PlayerAttack();
    }
    private void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0f) || !Mathf.Approximately(move.y, 0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("LookX", lookDirection.x);
        animator.SetFloat("LookY", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);
    }
    private void Movement()
    {
        Vector2 position = rb.position;
        speed = currentSpeed;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rb.MovePosition(position);
    }
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            SoundController.instance.PlayThisSound("attack", "Player", 0.5f);
            attack = true;
            animator.SetTrigger("Hit");
        }
    }
    private void PlayerAttack()
    {
        sword.HitBoxChange(lookDirection.x, lookDirection.y, attack);
        attack = false;
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            SoundController.instance.PlayThisSound("hit", "Player", 0.5f);
            if (isInvincible) return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            col.enabled = false;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
    }
    public void IsHited()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                col.enabled = true;
            }
        }
    }
    public void IsDead()
    {
            isAlive = false;
            rb.simulated = false;
            if(GamePlayController.instance != null)
            {
                GamePlayController.instance.ActiveCanvas();
            }
    }
    private void Dead()
    {
        if (currentHealth <= 0)
        {
            Invoke("IsDead", 2);
        }
    }
    public void Sound()
    {
        SoundController.instance.PlayThisSound("walk", "Player", 0.5f);
    }
}    
