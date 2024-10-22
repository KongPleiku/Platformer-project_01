using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControler : MonoBehaviour
{
    public LayerMask GroundOn;

    public float Movement;
    public float JumpForce;

    public bool IsFacingRight = true;

    [HideInInspector]public bool IsOnGround;
    bool DoubleJump ;

    public Transform GroundChecker;

    Rigidbody2D m_rigidbody2D;
    Animator animator;
    private void Awake()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        GroundChecker = transform.Find("GroundChecker");
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        IsOnGround = Physics2D.OverlapCircle(GroundChecker.position, 0.1f, GroundOn);
        if (IsOnGround)
        {
            animator.SetBool("OnGround", true);
        }
        else
        {
            animator.SetBool("OnGround", false);

            animator.SetBool("Jump", false);
        }
    }

    public void Move(float Movement)
    {
        animator.SetFloat("PlayerMoving" ,Mathf.Abs(Movement));
        m_rigidbody2D.velocity = new Vector2(Movement, m_rigidbody2D.velocity.y);
        if (!IsFacingRight && Movement > 0)
        {
            Flip();
        }
        if (IsFacingRight && Movement < 0)
        {
            Flip();
        }
    }

    public void Jump(float JumpForce , float movement)
    {
        animator.SetBool("Jump", true);
        if (IsOnGround)
        {
            FindObjectOfType<SoundManager>().Play("Jump");
            m_rigidbody2D.velocity = new Vector2(movement , m_rigidbody2D.velocity.y);
            m_rigidbody2D.velocity += Vector2.up * JumpForce;
            DoubleJump = true;
        }
        else
        {
            if (DoubleJump)
            {
                FindObjectOfType<SoundManager>().Play("Jump");
                m_rigidbody2D.velocity = new Vector2(movement, m_rigidbody2D.velocity.y);
                m_rigidbody2D.velocity += Vector2.up * JumpForce;
                DoubleJump = false;
            }
        }
    }
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(GroundChecker.position, 0.2f);
    }
}
