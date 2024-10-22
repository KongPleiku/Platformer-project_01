using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<HealthSystem>().TakeDamage(2);
        }
        else if (collision.gameObject.layer == 8)
        {
            FindObjectOfType<SoundManager>().Play("Bullet Impact");
            Destroy(gameObject);
        }
    }
    public void  DestroyBullet()
    {
        FindObjectOfType<SoundManager>().Play("Bullet Impact");
        Destroy(gameObject);
    }
}
