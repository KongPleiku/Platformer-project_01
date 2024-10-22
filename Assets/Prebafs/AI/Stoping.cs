using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stoping : StateMachineBehaviour
{
    Rigidbody2D Rb;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rb = animator.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Rb.velocity = Vector2.zero;
    }
}
