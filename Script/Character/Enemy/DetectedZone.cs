using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectedZone : MonoBehaviour
{
    public List<Collider2D> enemies = new List<Collider2D>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" )
        {
            enemies.Add(collision);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            enemies.Remove(collision);
        }
    }
}
