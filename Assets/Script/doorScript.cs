using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour
{
    public void Open()
    {
        GetComponent<Collider2D>().enabled = false;
        Animator ani = GetComponent<Animator>();
        ani.SetTrigger("Dead");
    }
}
