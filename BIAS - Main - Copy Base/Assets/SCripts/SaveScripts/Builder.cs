using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Builder : MonoBehaviour
{
   
    public BuildersDetails bDeets; // get access to BuildersDetails and allow us to assign it in the inspector
    public static Builder instance; // create instance of this class so we can access it elsewhere -  we dont need to assign this in the inspector
    
    public string buildersInputName; // string to store the builders name before save
    public string buildersName; // string to store the builders name before save

    public float amountOfBricksCollected; //  float to store amount of bricks in inventory
    public float amountOfBrokenBricks; // float to store the amount of broken bricks in inventory
    public Text bricksAmountText;

    public Text inputText;
    public Text buildersNameToDisplay;
    private void OnApplicationQuit() // on application quit ( Editor )
    {
      //  bDeets.SaveBuilderDetails(); // force save game
    }
    private void Awake()
    {
        instance = this; // set instance of this class to this script
       buildersName = PlayerPrefs.GetString("name", "none");
       buildersNameToDisplay.text = buildersName.ToString();
     
    }
    private void Start()
    {
        BuildersDetailsData data = SaveSystem.LoadBuilderDetails(); // Get access to the save data from BuildersDetailsData
        amountOfBricksCollected = data.amountOfBricksCollected; // amount of bricks possessed is float from saved data

    }
    private void Update() 
    {
        BuildersDetailsData data = SaveSystem.LoadBuilderDetails(); // Get access to the save data from BuildersDetailsData
        if (Input.GetKeyDown(KeyCode.Alpha0)) // debug key to force save
        {
          //  bDeets.SaveBuilderDetails(); // force save game

        }
    
        bricksAmountText.text = amountOfBricksCollected.ToString();
        buildersName = PlayerPrefs.GetString("name", "none");
        buildersNameToDisplay.text = buildersName;
    }

    public void InputYourNameOnChanged()
    {
        buildersName = inputText.text;
        PlayerPrefs.SetString("name", buildersName);

    }

    public void Add1ToBrickAmount() // function to add a brick to the players inventory
    {
        amountOfBricksCollected++; // amountOfBricksCollected + 1 on every call
       // bDeets.SaveBuilderDetails(); // force save game
    }

    public void Remove1ToBrickAmount() // function to remove a brick to the players inventory
    {
        amountOfBricksCollected--; // amountOfBricksCollected - 1 on every call
      // bDeets.SaveBuilderDetails(); // force save game
    }
}
