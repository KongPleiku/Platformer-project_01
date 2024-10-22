using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{

    Collider2D[] EnemeyHits;

    public void MeleeAttack(int MeleeDamage ,float AttackRange , LayerMask Enemy)
    {
        EnemeyHits = Physics2D.OverlapCircleAll(transform.position,AttackRange,Enemy);
        foreach(Collider2D TheEnemy in EnemeyHits)
        {
            if(TheEnemy.gameObject.tag == "Player")
            {
                TheEnemy.GetComponent<HealthSystem>().TakeDamage(MeleeDamage);
            }
            if(TheEnemy.gameObject.tag == "Enemy")
            {
                TheEnemy.GetComponent<HealthSystem>().TakeDamage(MeleeDamage);
            }
        }
    }
    public void RangedAttack(GameObject BulletPreBafs, Vector3 Target , Transform FirePoint)
    {
        float angle = Mathf.Atan2(Target.y, Target.x) * Mathf.Rad2Deg;
        GameObject Bullet = Instantiate(BulletPreBafs, FirePoint.position, FirePoint.rotation);
        Rigidbody2D Rb = Bullet.GetComponent<Rigidbody2D>();
        Rb.AddForce(Target * 10f , ForceMode2D.Impulse);
    }
}
