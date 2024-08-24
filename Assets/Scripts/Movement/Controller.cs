using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [Header("Keybinds")] 
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode accelerationKey = KeyCode.LeftShift;
    
    [Header("Movement")]
    [SerializeField] private float speed=7f;
    [SerializeField] private float accelerationBoost=1.5f;
    [SerializeField] private float jumpCooldown=0.3f;
    [SerializeField] private float jumpHeight=4f;
    [SerializeField] private int maxNumberOfJumps=1;
    [SerializeField] private float airMultiplier=0.3f;

    [Header("Physics")] 
    [SerializeField] private float mass=3f;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity=10f;
    [SerializeField] private float startGravity=1f;
    [SerializeField] private float groundDistance=0.33f;


    private float numberOfJumps;
    private bool acceleration;
    private bool jumpProcess;
    private bool grounded;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private float horizontalInput;
    private float verticalInput;
    private bool readyToJump=true;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        grounded = Physics.CheckSphere(orientation.position, groundDistance, groundMask);
        if (grounded && velocity.y < 0)
        {
            velocity.y = -startGravity;
        }
        if (grounded)
        {
            if (readyToJump)
            {
                jumpProcess = false;
                numberOfJumps = maxNumberOfJumps;
            }
        }
        
        MyInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        
        if (jumpProcess)
        {
            verticalInput = Math.Clamp(verticalInput, verticalInput * airMultiplier, 1);
            horizontalInput *= airMultiplier;
        }
        
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (acceleration)
        {
            moveDirection *= accelerationBoost;
        }
        
        
        velocity.y -= mass * gravity * Time.deltaTime;
        
        characterController.Move((moveDirection * speed + velocity) * Time.deltaTime);

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(jumpKey))
        {
            jumpProcess = true;
            if (grounded)
            {
                jump();
            }
            else
            {
                if (maxNumberOfJumps > numberOfJumps)
                {
                    jump();
                }
            }
        }
        if (Input.GetKey(accelerationKey))
        {
            acceleration = true;
        }
        else
        {
            acceleration = false;
        }
    }

    private void jump()
    {
        if (numberOfJumps > 0&&readyToJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            numberOfJumps--;
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }
    private void ResetJump()
    {
        readyToJump = true;
    }
}
