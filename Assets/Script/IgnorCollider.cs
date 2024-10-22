using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnorCollider : MonoBehaviour
{
    Collider2D collider;
    private void Awake()
    {
        collider = GetComponent<Collider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(collider, collision.collider, true);
        }
    }
}
