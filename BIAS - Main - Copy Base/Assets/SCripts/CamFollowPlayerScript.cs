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
   

 
    void LateUpdate() // called after all update() methods have been called
    {
        transform.position = player.transform.position;// 
    }
   

}

