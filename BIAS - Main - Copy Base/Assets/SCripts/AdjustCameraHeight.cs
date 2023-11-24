using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustCameraHeight : MonoBehaviour
{
    // ------ This script adjusts the height of the iso cam

    public Camera isoCam; // declare camera for iso view to zoom
    public float cameraZoomScrollSpeed; // declare scroll speed for the camera
    public bool cameraAtMaxZoom; // declare bool to check is camera is at max zoom
    public bool cameraAtMinZoom; // declare bool to check if camera is an min zoom

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0f) // if = key is pressed
        {
            if (!cameraAtMaxZoom) // if camera not at max zoon
            {
                cameraAtMinZoom = false; //  camera is not at minimum zoom and can be adjusted
                isoCam.orthographicSize--; // shrink the orthrographic size of the camera
                if (isoCam.orthographicSize <= 3) // if the iso cam is below or equal to 3
                {
                    cameraAtMaxZoom = true; // camera at max zoom = true
                    cameraAtMinZoom = false; //  camera at min zoom = false
                }
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f) // if - key is pressed
        {
            if (!cameraAtMinZoom) // if camera not at min zoon
            {
                cameraAtMaxZoom = false;  //  camera is not at maximum zoom and can be adjusted
                isoCam.orthographicSize++;  // shrink the orthrographic size of the camera
                if (isoCam.orthographicSize >= 9) // if the iso cam is above or equal to 9
                {
                    cameraAtMinZoom = true; // camera at min zoom true
                    cameraAtMaxZoom = false; // camera at max zoom false;
                }
            }
           
        }

        
    }
}
