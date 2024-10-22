using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameManeger Gm;
    private void Start()
    {
        Gm = FindObjectOfType<GameManeger>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "CheckPoint")
        {
            if (collision.CompareTag("Player"))
            {
                Gm.LastCheckPoint = collision.transform.position;
            }
        }
        if (gameObject.CompareTag("EndPoint"))
        {
            if (collision.CompareTag("Player"))
            {
                Gm.LastPointChecked = true;
            }
        }
    }
}
