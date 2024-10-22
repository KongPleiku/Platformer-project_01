using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPLayer : StateMachineBehaviour
{

    Transform PLayer;
    Transform AI;
    Rigidbody2D Rb;

    float Speed;
    bool IsFacingRight;

    EnemyScript Core;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AI = animator.GetComponent<Transform>();
        PLayer = GameObject.FindGameObjectWithTag("Player").transform;
        Rb = animator.GetComponent<Rigidbody2D>();
        Core = animator.GetComponent<EnemyScript>();
        if(AI.localScale.x > 0)
        {
            IsFacingRight = true;
        }
        else if (AI.localScale.x < 0)
        {
            IsFacingRight = false;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rb.velocity = Vector2.zero;
        Vector3 Dir = PLayer.position - Rb.transform.position;
        float Angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg;
        if(Angle > 90 || Angle < - 90)
        {
            Speed = -5;
        }
        else if(Angle <90 || Angle > -90)
        {
            Speed = 5;
        }
        if(!IsFacingRight && Speed > 0)
        {
            Flip();
        }
        if(IsFacingRight && Speed < 0)
        {
            Flip();
        }
        if (Vector2.Distance(Rb.position, PLayer.position) > Core.AttackRange)
        {
            Rb.velocity = new Vector2(Speed,Rb.velocity.y);
        }
        else
        {
            animator.SetTrigger("Attack");
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 Scaler = AI.localScale;
        Scaler.x *= -1;
        AI.localScale = Scaler;
    }
}
