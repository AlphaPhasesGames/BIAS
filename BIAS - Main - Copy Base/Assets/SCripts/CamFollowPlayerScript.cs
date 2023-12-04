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
    private void Update()
    {
        // spinCam is a new vector 3, taking the axis from the unity input settings on the X and Y axis and 0 on the Z axis
        spinCam = new Vector3(Input.GetAxis("VerticalIso"), Input.GetAxis("HorizontalIso"), 0.0f);
        // rotate the camera using the vector 3 from the spin cam, by the spinCamSpeed float by time.delta time. 
        this.transform.Rotate(spinCam * camSpinSpeed * Time.deltaTime);
    }

    void LateUpdate() // called after all update() methods have been called
    {
        transform.position = player.transform.position;// 
    }
   

}

