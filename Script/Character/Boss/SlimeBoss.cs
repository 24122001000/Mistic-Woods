using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeBoss : MonoBehaviour,ICharacterHealth
{
    public float speed;
    private float distance;
    public float distanceBetween = 4;
    public float distanceAffter = 0.4f;
    public float timeRangeAttack = 4;
    float timeDelay = 0;
    float timeToAttack = 0;
    private int maxBossHealth = 10;
    public int currentHealth;

    public GameObject rangeAttackPrefab;
    Animator ani;
    SpriteRenderer sp;
    Rigidbody2D rb;
    GameObject player;

    Vector2 director;
    int bodyDamage = 3;
    public bool canMove = true;
    bool isAlive = true;
    bool isHited = false;
    bool isBossAttack = false;
    public bool isAttack = false;
    public bool isRoll = false;
    private void Awake()
    {
        ani = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxBossHealth;
    }

    // Update is called once per frame
    void Update()
    {
        SetAni();
        IsDead();
    }
    private void FixedUpdate()
    {
        BossController();
    }
    private void SetAni()
    {
        ani.SetFloat("Speed", speed);
        ani.SetBool("isAlive", isAlive);
        ani.SetBool("isHited", isHited);
        ani.SetBool("isRoll", isRoll);
        ani.SetBool("isAttack",isAttack);
    }
    void BossController()
    {
        Direction();
        if (currentHealth < (maxBossHealth / 2))
        {
            isRoll = true;
        }
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (isBossAttack)
        {
            RangeAttack();
        }
        if (canMove)
        {
            Movement();
        }
    }
    void Direction()
    {
        director = player.transform.position - transform.position;
        director.Normalize();
        if (director.x < 0)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
    }
    void Movement()
    {
        if (distance < distanceBetween)
        {
            if (isRoll)
            {
                speed = 0.7f;
            }
            else if (distance <= distanceAffter)
            {
                speed = 0f;
            }
            else
            {
                speed = 0.5f;
                timeToAttack += Time.deltaTime;
                if (timeToAttack > timeRangeAttack)
                {
                    isBossAttack = true;
                    timeToAttack = 0;
                }
            }
        }
        else
        {
            speed = 0;
        }
        transform.localPosition = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    void RangeAttack()
    {
        timeDelay -= Time.deltaTime;
        if (timeDelay < 0)
        {
            SoundController.instance.PlayThisSound("BossAttack", "Enemy", 0.5f);
            isAttack = true;
            canMove = false;
            GameObject bulletObject = Instantiate(rangeAttackPrefab, rb.position + new Vector2(0,0.1f), Quaternion.identity);
            RangeAttack rangeAttack = bulletObject.GetComponent<RangeAttack>();
            rangeAttack.Attack(director, 2500f);
            timeDelay = timeRangeAttack;
        }
    }
    
    public void IsHited()
    {
        isHited = false;
    }
    public void IsDead()
    {
        if (currentHealth <= 0)
        {
            if (isRoll)
            {
                StartCoroutine(DiedAfterRoll());
            }
            else
            {
                isAlive = false;
            }
            rb.simulated = false;
            StartCoroutine(BossDied());
        }
    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isRoll)
            {
                Debug.Log("Roll");
            }
            else
            {
                isHited = true;
            }
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxBossHealth);
    }
    public void EndAttack()
    {
        canMove = true;
        isAttack = false;
        isBossAttack = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (collision.gameObject.tag == "Player")
        {
            player.ChangeHealth(-bodyDamage);
        }
    }
    IEnumerator BossDied()
    {
        yield return new WaitForSeconds(3);
        if(GamePlayController.instance != null)
        {
            GamePlayController.instance.BossDefeat();
        }
    }
    IEnumerator DiedAfterRoll()
    {
        isRoll = false;
        yield return new WaitForSeconds(1);
        isAlive = false;
    }
    
}
