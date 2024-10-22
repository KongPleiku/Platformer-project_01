using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public int Damage;
    public LayerMask Enemy;
    public LayerMask Ground;
    public bool IsMelleeAttack;
    public float AttackRange;
    public Transform Checker;
    public Transform FirePoint;
    public GameObject BulletPrefabs;
    public GameObject MuzzleFlashEffect;

    Animator animator;
    CombatSystem System;

    private void Start()
    {
        System = GetComponent<CombatSystem>();
        Physics2D.queriesStartInColliders = false;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        int Direction = 0;
        if(transform.localScale.x < 0)
        {
            Direction = -1;
        }
        else if(transform.localScale.x > 0)
        {
            Direction = 1;
        }
        RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, transform.right*Direction);
        foreach(RaycastHit2D collision in ray)
        {
            if(collision.transform.gameObject.layer == 8 || collision.transform.tag == "Door")
            {
                return;
            }
            if (collision.transform.tag == "Player")
            {
                if (collision.transform.gameObject.GetComponent<PlayerCombat>().IsVisible)
                {
                    animator.SetTrigger("PlayerDetected");
                    return;
                }
            }
        }
    }
    public void AiAttack()
    {
        if (IsMelleeAttack)
        {
            System.MeleeAttack(Damage, AttackRange, Enemy);
        }
        else if (!IsMelleeAttack)
        {
            float EffectAngle = 0;
            Vector3 dir = transform.right;
            if (transform.localScale.x > 0)
            {
                dir = transform.right;
                EffectAngle = 0;
            }
            else if (transform.localScale.x < 0)
            {
                dir = transform.right * -1;
                EffectAngle = 180;
            }
            FindObjectOfType<SoundManager>().Play("Gun Fire");
            System.RangedAttack(BulletPrefabs , dir  , FirePoint);
            GameObject effect = Instantiate(MuzzleFlashEffect, FirePoint.position, FirePoint.rotation);
            effect.transform.eulerAngles = new Vector3(0, 0, EffectAngle);
        }
    }
}
