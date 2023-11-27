using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RotateCameraLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{


    public Button lButton;
    public bool lButtonHeld;
    public GameObject player; // declare player gameobject
    private Vector3 spinCam; // declare vector 3 for the camera
    public float camSpinSpeed; // declare float to hold the speed of the camera rotation
    public float camSpinSpeedTEst;// declare float to hold the speed of the camera rotation
    public GameObject camera;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        // spinCam is a new vector 3, taking the axis from the unity input settings on the X and Y axis and 0 on the Z axis
        //spinCam = new Vector3(Input.GetAxis("VerticalIso"), Input.GetAxis("HorizontalIso"), 0.0f);
        spinCam = new Vector3(0, camSpinSpeedTEst, 0.0f);

        // rotate the camera using the vector 3 from the spin cam, by the spinCamSpeed float by time.delta time. 
        camera.transform.Rotate(spinCam * camSpinSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.A))
        {
            lButtonHeld = true;

        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            lButtonHeld = false;

        }

        if (lButtonHeld)
        {
            camSpinSpeedTEst = 1;
        }

        else
        {
            lButtonHeld = false;
            camSpinSpeedTEst = 0;
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        camSpinSpeedTEst = 1;
        lButtonHeld = true;
        Debug.Log("This button is pressed");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        camSpinSpeedTEst = 0;
        lButtonHeld = false;
        Debug.Log("This button is not pressed");
    }


}
