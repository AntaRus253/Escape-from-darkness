using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float Sens=400f;
    [SerializeField] private Transform orientation;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X")*Sens*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*Sens*Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }



}