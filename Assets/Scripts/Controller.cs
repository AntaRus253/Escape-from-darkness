using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] private Transform orientation;

    [Header("Keybinds")] 
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    
    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Physics")] 
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;
    [SerializeField] private float startGravity;
    [SerializeField] private float groundDistance=0.3f;
    
    
    
    private bool grounded;
    private CharacterController characterController;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private float horizontalInput;
    private float verticalInput;

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
        
        MyInput();
        MovePlayer();
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
        characterController.Move(moveDirection*speed*Time.deltaTime);
            
            velocity.y -= gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);

    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

    }
}
