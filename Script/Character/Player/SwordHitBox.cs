using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public Collider2D col;
    int damage = 1;
    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    public void HitBoxChange(float x,float y,bool attack)
    {
        if (attack)
        {
            col.enabled = true;
            if (x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90f);
                transform.localPosition = new Vector3(0.1f, 0.1f, 0);
            }
            if (x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90f);
                transform.localPosition = new Vector3(-0.15f, 0.1f, 0);
            }
            if (y > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localPosition = new Vector3(0, 0.2f, 0);
            }
            if (y < 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                transform.localPosition = new Vector3(0, 0, 0);
            }
        }
        else col.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.SendMessage("ChangeHealth",-damage);
        }
    }
}
