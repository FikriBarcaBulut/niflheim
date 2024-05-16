using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float baseSpeed = 5f;
    public float sprintSpeed = 10f;
    public float crouchSpeed = 1f; // Adjusted crouch speed
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float crouchHeight = 0.5f;

    private bool isCrouching = false;
    private Vector3 velocity;

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleCrouch();
    }

    void HandleMovement()
    {
        float currentSpeed = baseSpeed;

        if (Input.GetButton("Fire3"))
            currentSpeed = sprintSpeed;
        else if (isCrouching)
            currentSpeed = crouchSpeed;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    void HandleJump()
    {
        if (controller.isGrounded && velocity.y < 0)
            velocity.y = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (!isCrouching)
            {
                controller.height = crouchHeight;
                controller.center = new Vector3(0f, crouchHeight / 1f, 0f);
                isCrouching = true;
            }
            else
            {
                controller.height = 2f;
                controller.center = Vector3.zero;
                isCrouching = false;
            }
        }
    }
}
