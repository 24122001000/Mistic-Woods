using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : MonoBehaviour
{
    Rigidbody2D rb;
    Vector2 tf;
    SpriteRenderer sp;
    float damage = 2;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        tf = transform.position;
    }
    private void Update()
    {
        DestroyBullet(5);
    }
    public void Attack(Vector2 director,float force)
    {
        rb.AddForce(director * force*Time.deltaTime);
        if(director.x < 0)
        {
            sp.flipX = true;
        }
        else
        {
            sp.flipX = false;
        }
    }
    void DestroyBullet(float distance)
    {
        float bulletDistance = Vector2.Distance(tf, transform.position);
        if (distance < bulletDistance)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.SendMessage("ChangeHealth",-damage);
            Destroy(gameObject);
        }
    }
}
