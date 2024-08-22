using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [SerializeField] private float SensX;
    [SerializeField] private float SensY;
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
        float mouseX = Input.GetAxis("Mouse X")*SensX*Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")*SensY*Time.deltaTime;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Math.Clamp(xRotation, -90f, 90f);
        
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }



}