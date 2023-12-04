using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CamFollowPlayerScript : MonoBehaviour
{
    //----------- This script rotates the cam in iso veiw

    
    public GameObject player; // declare player gameobject
                              //  private Vector3 offset; // create declare vector 3 for the offset betwee the camera and the player
    private Vector3 spinCam; // declare vector 3 for the camera
    public float camSpinSpeed; // declare float to hold the speed of the camera rotation
    public bool isCameraFollwingMouse;
    public Cursor mouseCurson;

    float mDelta = 10; // Pixels. The width border at the edge in which the movement work
    float mSpeed = 3.0f; // Scale. Speed of the movement

    private Vector3 mRightDirection = Vector3.right; // Direction the camera should move when on the right edge
    private Vector3 mleftDirection = Vector3.left; // Direction the camera should move when on the right edge
    private Vector3 mUpDirection = Vector3.up; // Direction the camera should move when on the right edge
    private Vector3 mDownDirection = Vector3.down; // Direction the camera should move when on the right edge

    private void Update()
    {
        // spinCam is a new vector 3, taking the axis from the unity input settings on the X and Y axis and 0 on the Z axis
        spinCam = new Vector3(Input.GetAxis("VerticalIso"), Input.GetAxis("HorizontalIso"), 0.0f);
        // rotate the camera using the vector 3 from the spin cam, by the spinCamSpeed float by time.delta time. 
        this.transform.Rotate(spinCam * camSpinSpeed * Time.deltaTime);


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
                transform.position += mRightDirection * Time.deltaTime * mSpeed;
            }

            if (Input.mousePosition.x <= 0)
            {
             // mm  // Move the camera
               transform.position += mleftDirection * Time.deltaTime * mSpeed;
            }

           
        }
       
    }


}

