using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public GameObject HitEffect;
    public GameObject BulletPrebafs;

    public float AttackRate;
    public float AttackRange;
    public int MelleeDamage;

    public bool IsVisible;

    public LayerMask Enemy;

    public TimeControl timecontroller;

    float NextAttacktime = 0;

    Transform FirePoint;
    CombatSystem System;
    Camera cam;
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        FirePoint = transform.Find("Aim");
        System = GetComponent<CombatSystem>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= NextAttacktime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack();
            }
        }
        else
        {
            animator.SetBool("Punch", false);
        }
        Vector3 Dir = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        FirePoint.eulerAngles = new Vector3(0, 0, angle);
        if (Input.GetMouseButtonDown(1))
        {
            timecontroller.TimeShift();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision!= null)
        {
            if(collision.gameObject.tag == "StealthZone")
            {
                IsVisible = false;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.gameObject.tag == "StealthZone")
            {
                IsVisible = true;
            }
        }
    }
    void Attack()
    {
        bool effectGenerate = false;
        System.MeleeAttack(MelleeDamage, AttackRange, Enemy);
        animator.SetBool("Punch", true);
        NextAttacktime = Time.time + 1 / AttackRate;
        Collider2D[] HitsEnemy = Physics2D.OverlapCircleAll(transform.position, AttackRange, Enemy);
        if (HitsEnemy != null)
        {
            if (timecontroller.IsTimeShift)
            {
                timecontroller.TimeShift();
            }
            FindObjectOfType<SoundManager>().Play("Sword Air");
            foreach (Collider2D checker in HitsEnemy)
            {
                if (checker.transform.tag == "Door")
                {
                    FindObjectOfType<SoundManager>().Play("Door Kick");
                    timecontroller.SlowMo();
                    checker.GetComponent<doorScript>().Open();
                }
                if (checker.transform.tag == "Bullet")
                {
                    FindObjectOfType<SoundManager>().Play("Sword Hit");
                    timecontroller.SlowMo();
                    checker.GetComponent<BulletScript>().DestroyBullet();
                }
            }
            foreach (Collider2D enemy in HitsEnemy)
            {
                if(enemy.transform.tag == "Door")
                {
                    FindObjectOfType<SoundManager>().Play("Door Kick");
                    timecontroller.SlowMo();
                    enemy.GetComponent<doorScript>().Open();
                }
                
                if(enemy.gameObject.tag != "Door")
                {

                    if (enemy.gameObject.tag != "Bullet" && !enemy.GetComponent<HealthSystem>().IsDead)
                    {
                        FindObjectOfType<SoundManager>().Play("Sword Hit");
                        timecontroller.SlowMo();
                        Vector3 dir = enemy.transform.position - transform.position;
                        float goc = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        GameObject Swing = Instantiate(HitEffect, transform.position, transform.rotation);
                        effectGenerate = true;
                        Transform Tran = Swing.GetComponent<Transform>();
                        if (goc > 90 || goc < -90)
                        {
                            Tran.localScale = new Vector3(Tran.localScale.x, Tran.localScale.y * -1, Tran.localScale.z);
                        }
                        else if (goc < 90 || goc > -90)
                        {
                            Tran.localScale = new Vector3(Tran.localScale.x, Tran.localScale.y, Tran.localScale.z);
                        }
                        Tran.eulerAngles = new Vector3(0, 0, goc);
                        return;
                    }
                }
            }
        }
        if (!effectGenerate)
        {
            Instantiate(HitEffect, transform.position, transform.rotation);
        }
    }
    public void DeadActive()
    {
        FindObjectOfType<SoundManager>().Play("Explosion");
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
}
