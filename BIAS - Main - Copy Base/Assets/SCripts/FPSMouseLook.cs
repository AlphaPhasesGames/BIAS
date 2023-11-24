using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSMouseLook : MonoBehaviour
{
    public float mouseSensitivity; //Sensitivity for mouse look
    public float xRotation = 0f; // decalre xRotation float and set it to 0f;
    public float mouseX; // declare float for mouse X rotation
    public float mouseY; // declare float for mouse Y rotation
    public Transform playerBody; // transform for the Playerbody to be attached too;

    // Update is called once per frame
    void Update()
    {
       if(PlayerController.instance.fPSCam == true)
        {
            // we do this to assign the look controls to the mouse not the sticks
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; // set mouseX to the axis of Mouse X in input manager pointing to the value of mouseSensitivity
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity; // set mouseY to the axis of Mouse Y in input manager pointing to the value of mouseSensitivity



            xRotation -= mouseY; //Decrease our x rotation based on mouse y. We use -= because using + the rotation is flipped.
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX); // Rotate around the Y Axis by the mouseX positon 

        }

    }
}
