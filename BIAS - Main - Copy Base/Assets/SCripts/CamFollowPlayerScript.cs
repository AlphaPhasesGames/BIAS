using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayerScript : MonoBehaviour
{
    //----------- This script rotates the cam in isometric veiw

    
    public GameObject player; // declare player gameobject
    public Vector3 spinCam; // declare vector 3 for the camera
    public float camSpinSpeed; // declare float to hold the speed of the camera rotation
    public bool isCameraFollwingMouse;
    public float mSpeed; // Scale. Speed of the movement
    public Camera isoCam;
    public GameObject eastDirectrion;

    private void Start()
    {
        mSpeed = 5;
        camSpinSpeed = 20f;
    }

    private void Update()
    {
        spinCam = new Vector3(Input.GetAxis("VerticalIso"), Input.GetAxis("HorizontalIso"), 0.0f); // spinCam is a new vector 3, taking the axis from the unity input settings on the X and Y axis and 0 on the Z axis
        this.transform.Rotate(spinCam * camSpinSpeed * Time.deltaTime); // rotate the camera using the vector 3 from the spin cam, by the spinCamSpeed float by time.delta time. 
           
        if (Input.GetKeyDown(KeyCode.M))
        {
            isCameraFollwingMouse = !isCameraFollwingMouse;
        }
    }

    void LateUpdate() // called after all update() methods have been called
    {
        if (!isCameraFollwingMouse)
        {
            transform.position = player.transform.position;// 
        }

        if (isCameraFollwingMouse)
        {
            // Check if on the right edge
            if (Input.mousePosition.x >= Screen.width )
            {
                // Move the camera
               transform.position += isoCam.transform.right * Time.deltaTime * mSpeed;
            }

            if (Input.mousePosition.x <= 0)
            {
             // mm  // Move the camera
               transform.position += -isoCam.transform.right * Time.deltaTime * mSpeed;
            }

            if (Input.mousePosition.y >= Screen.height)
            {
                // Move the camera
                transform.position += eastDirectrion.transform.forward * Time.deltaTime * mSpeed;
            }

            if (Input.mousePosition.y <= 0)
            {
                // mm  // Move the camera
                transform.position += -eastDirectrion.transform.forward * Time.deltaTime * mSpeed;
            }
        }
    }
}

