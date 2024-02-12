using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Object")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                collision.SendMessage("Touch", true);
            }
        }
    }
}
