using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickSave : MonoBehaviour
{
    [Header("BrickDetails")] // header for the bricks details
    public string brickName; // string to store the brick name, players can edit
    public Rigidbody rb;
  //  public bool isKinematic = true;
    private void Awake()  // on awake
    {
        SaveSystem.bricks.Add(this);  // add this brick to the list of saved objects
        
       
    }
         
       
    private void OnMouseDown() // if mouse is over this object and clicked
    {
        if (PlayerController.instance.deleteBrickActive)  // if deleteBrickActive is enabled in the player controller
        {
            SaveSystem.bricks.Remove(this); // remove it from the list of rotated bricks, so it doesnt load back in next time we start the game.
            gameObject.SetActive(false); //  set this gameobject to inactive and hide it 
            Destroy(this.gameObject); // destroy this gameobject
            Destroy(transform.parent.gameObject); // destroy parent object of this object
          //  Builder.instance.Add1ToBrickAmount();
        }//

    }
    /*
    private void OnTriggerEnter(Collider brick, bool isBrickStck)
    {
        Debug.Log("This new code triggers");
        if (brick.CompareTag("Cement"))
        {
            isBrickStck = true;
            rb.isKinematic = isBrickStck;
            Debug.Log("Brick At");
        }
    }

     */
    private void OnTriggerEnter(Collider other)
           {
               if (other.CompareTag("Cement"))
               {
                   rb.isKinematic = true;
               }
           }

           private void OnTriggerStay(Collider other)
           {
               if (other.CompareTag("Cement"))
               {
                   rb.isKinematic = true;
               }
           }

           private void OnTriggerExit(Collider other)
           {
               if (other.CompareTag("Cement"))
               {
                   rb.isKinematic = false;
               }
           }
     
}
