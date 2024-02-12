using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    Animator ani;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public GameObject skeHitBox;
    Collider2D colHitBox;
    float attackDelay;
    float skeDelay = 2f;
    public bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        attackDelay = skeDelay;
        ani = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        colHitBox = skeHitBox.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sr.flipX == true)
        {
            skeHitBox.transform.localPosition = new Vector3(-0.22f, 0, 0);
        }
        else
        {
            skeHitBox.transform.localPosition = new Vector3(0, 0, 0);
        }
        if (attack)
        {
            attackDelay -= Time.deltaTime;
            if(attackDelay < 0)
            {
                colHitBox.enabled = true;
                rb.mass = 100;
                ani.SetTrigger("Attack");
                attack = false;
            }
            
        }
        
    }
    public void SkeletonDelayAttack()
    {
        attackDelay = skeDelay;
        colHitBox.enabled = false;
        rb.mass = 2;
    }
    
}
