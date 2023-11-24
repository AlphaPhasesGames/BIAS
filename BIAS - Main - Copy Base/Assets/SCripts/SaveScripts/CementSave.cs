using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CementSave : MonoBehaviour
{
    [Header("BrickDetails")] // header for the  Rotated bricks details
    public string brickName; // string to store the Rotated brick name, players can edit
    public Rigidbody rB;
    private void Awake() // on awake
    {
        // SaveSystem.cementBrick.Add(this); // add this otated brick to the list of saved objects
        SaveSystem.cementBrick.Add(this); // add this otated brick to the list of saved objects
    }

    private void OnMouseDown() // if mouse is over this object and clicked
    {
        if (PlayerController.instance.deleteBrickActive) // if deleteBrickActive is enabled in the player controller
        {
            SaveSystem.cementBrick.Remove(this); // remove it from the list of rotated bricks, so it doesnt load back in next time we start the game.
            Destroy(this.gameObject); // destroy this game object
            gameObject.SetActive(false); //  set this gameobject to inactive and hide it 
            Destroy(transform.parent.gameObject); //  destroy parent object so we arnt left with loads of invisible bricks after deleting
        }

    }

   // public IEnumerator MakeCementRigid()
   // {
       
       
       // yield return new WaitForSeconds(5);
       // rB.isKinematic = true;
   // }
}
