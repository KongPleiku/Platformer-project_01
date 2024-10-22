using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    MovementControler Controler;

    public float Speed;
    public float JumpForce;
    public float DashSpeed;
    public float JumpTime;

    float Movement;

    Animator animator;

    private void Awake()
    {
        Controler = GetComponent<MovementControler>();
    }
    private void Start()
    {
        FindObjectOfType<SoundManager>().Play("Moving Sound");
    }
    // Update is called once per frame
    void Update()
    {
        
        Movement = Input.GetAxisRaw("Horizontal") * Speed;
        Controler.Move(Movement);
        if (Controler.IsOnGround)
        {
            if (Movement == 0)
            {
                FindObjectOfType<SoundManager>().Mute("Moving Sound", true);
            }
            else
            {
                FindObjectOfType<SoundManager>().Mute("Moving Sound", false);
            }
        }
        else
        {
            FindObjectOfType<SoundManager>().Mute("Moving Sound", true);
        }
        if(Input.GetButtonDown("Jump"))
        {
            Controler.Jump(JumpForce, Movement);
        }
    }
}
