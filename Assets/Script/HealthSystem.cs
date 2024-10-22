using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    Animator animator;

    public ParticleSystem Pa;
    public int MaxHealth;
    public bool IsDoor;

    int PlayTime = 1;

    [HideInInspector] public bool IsDead = false;

    [HideInInspector]public int CurHealth;

    void Start()
    {
        Pa.Stop();
        animator = GetComponent<Animator>();
        CurHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurHealth <= 0)
        {
            if (gameObject.layer == 10)
            {
                DestroyObject(gameObject, 2f);
            }
            if (gameObject.tag == "Enemy")
            {
                DestroyObject(gameObject, 4f);
            }
            if (gameObject.tag == "Enemy" && PlayTime > 0 )
            {
                if (Pa.isStopped)
                {
                    Pa.Play();
                }
                PlayTime = 0;
            }
            animator.SetTrigger("Dead");
            IsDead = true;
        }
    }
    public void TakeDamage(int Damage)
    {
        CurHealth -= Damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            CurHealth = 0;
        }
    }
}
