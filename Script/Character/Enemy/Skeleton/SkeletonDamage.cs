using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDamage : MonoBehaviour
{
    int skeDamage = 3;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(collision.tag == "Player")
        {
            player.ChangeHealth(-skeDamage);
        }
    }
}
