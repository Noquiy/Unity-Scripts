using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public float walkSpeed = 5f;
    public float sprintSpeed = 9f;
    public float jumpForce = 5f;
    public float dashForce = 10f;
    public float toGroundDistance = 0.5f;

    private bool doubleJumpAble = false;


    void Awake()
    {
        rb.GetComponent<Rigidbody>();
    }


    void FixedUpdate()
    {
        walk();
        sprint();
    }

    private void Update()
    {
        jump();
        dash();
        doubleJump();
    }



    void walk()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 walking = new Vector3(x, 0, y) * (walkSpeed * Time.fixedDeltaTime);
        Vector3 movePosition = rb.position + rb.transform.TransformDirection(walking);
        rb.MovePosition(movePosition);
    }

    void sprint()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");
            Vector3 sprinting = new Vector3(x, 0, y) * (sprintSpeed * Time.fixedDeltaTime);
            Vector3 movePosition = rb.position + rb.transform.TransformDirection(sprinting);
            rb.MovePosition(movePosition);
        }
    }

    bool isGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, toGroundDistance);
    }

    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            doubleJumpAble = true;
        }
        else
        {
            doubleJumpAble = false;
        }
    }

    void doubleJump()
    {
        if (doubleJumpAble == true && Input.GetKeyDown(KeyCode.Space) && !isGrounded())
        {
            rb.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
            doubleJumpAble = false;
        }
    }

    void dash()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            rb.AddForce(Camera.main.transform.forward * dashForce, ForceMode.Impulse);
        }
    }


}
