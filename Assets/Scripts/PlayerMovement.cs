using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    private float speed;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpSpeed;
    public float gravity;
    public float jumpHeight;

    public KeyCode sprintKey = KeyCode.LeftShift;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;
    public Movementstate state;

    Vector3 velocity;
    bool isGrounded;
    
    public enum Movementstate
    {
        walking,
        sprinting,
        air,
    }
    void Start()
    {
        
    }

    
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }
        StateHandeler();
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }


        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void StateHandeler()
    {
        if (isGrounded && Input.GetKey(sprintKey))
        {
            state = Movementstate.sprinting;
            speed = sprintSpeed;
        }

        else if (isGrounded)
        {
            state = Movementstate.walking;
            speed = walkSpeed;

        }

        else
        {
            state = Movementstate.air;
        }
    }
}
