using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectArray : MonoBehaviour
{
    public GameObject[] buildingBricks; // declare an array for the game objects were gonna use to build with
    public GameObject currentObject; // declare gameobject this is the current object selected in the buildingBricks Array
    public int arrayPos; // declare int to store the array position
    public int arrayMax; // declare int to set max number in the array
    public int arrayMin = 1; // declare int to set min number in the array - Set as 1 so it wont go below 0


    // Start is called before the first frame update
    void Start()
    {
        arrayPos = 0; // on start set array pos to 0
        currentObject = buildingBricks[arrayPos]; // the current object we have selected is the building brick assigned by the arrayPos
        arrayMax = buildingBricks.Length; // max length of array is the length of the buildingBricks array
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftBracket)) // if left bracket is pressed
        {
            if(arrayPos > arrayMin) // if arrayPos is greated than the minimum size of the array
            {
                arrayPos--; // scroll down through array by reducing the arrayPos
                currentObject = buildingBricks[arrayPos]; // the current object is the object at arrayPos in the building blocks array
                Debug.Log("The chosen brick is " + currentObject + " " + arrayPos); // output message + the current object plus the array number
            }
        }

        if (Input.GetKeyDown(KeyCode.RightBracket)) // if Rightt bracket is pressed
        { 
            if (arrayPos < arrayMax) // if arrayPos is less than the max size of the array
            {
                arrayPos++; // scroll down through array by increasing the arrayPos
                currentObject = buildingBricks[arrayPos]; //  // the current object is the object at arrayPos in the building blocks array
                Debug.Log("The chosen brick is " + currentObject + " " + arrayPos); // output message + the current object plus the array number
            }
        }
    }
}
