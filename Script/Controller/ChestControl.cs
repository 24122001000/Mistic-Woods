using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestControl : MonoBehaviour
{
    Animator ani;
    bool open = false;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }
    private void Update()
    {
        ani.SetBool("Open", open);
    }
    public void Touch(bool touch)
    {
        open = touch;
    }
}
