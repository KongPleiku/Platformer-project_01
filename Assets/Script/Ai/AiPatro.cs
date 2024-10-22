using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiPatro : StateMachineBehaviour
{
    Rigidbody2D Rb;
    float Speed;
    Transform AI;
    EnemyScript Core;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Physics2D.queriesStartInColliders = false;
        AI = animator.GetComponent<Transform>();
        Core = animator.GetComponent<EnemyScript>();
        Rb = animator.GetComponent<Rigidbody2D>();
        if (AI.localScale.x > 0)
        {
            Speed = 2;
        }
        else if (AI.localScale.x < 0)
        {
            Speed = -2;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Collider2D[] Checker = Physics2D.OverlapCircleAll(Core.Checker.position, 0.1f);
        if(Checker != null)
        {
            foreach(Collider2D col in Checker)
            {
                if (col.transform.tag == "Wall" || col.transform.tag == "Door")
                {
                    ChangeDir();
                    animator.SetTrigger("PatroIdel");
                }
            }
        }
        Rb.velocity = new Vector2(Speed, Rb.velocity.y);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("PatroIdel");
    }
    void ChangeDir()
    {
        AI.localScale = new Vector3(AI.localScale.x * -1, AI.localScale.y, AI.localScale.z);
        Speed *= -1;
    }
}
