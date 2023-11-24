using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildersDetails : MonoBehaviour
{
 
    public string buildersName; // declare string to hold builders name before save
    public float amountOfBricksCollectedSaved; // declare float to store amount of bricks collected
    public float amountOfBrokenBricks; // declare float to store amount of broken bricks in possession


    public void SaveBuilderDetails() // Function to save builders details
    {
        SaveSystem.SaveBuildersDetails(this); // get access to save data script to allow us to store data in it
        buildersName = Builder.instance.buildersName; // Save builders name from Builder
        amountOfBricksCollectedSaved = Builder.instance.amountOfBricksCollected; // save amountOfBricksCollected from Builder
        amountOfBrokenBricks = Builder.instance.amountOfBrokenBricks; // save amountOfBrokenBricks from Builder
        Debug.Log("Builder Saved");
    }

    public void LoadBuildersDetails()
    {
        BuildersDetailsData data = SaveSystem.LoadBuilderDetails();
        Builder.instance.buildersName = data.buildersName;
        Builder.instance.amountOfBricksCollected = data.amountOfBricksCollected;
        Builder.instance.amountOfBrokenBricks = data.amountOfBrokenBricks;
        Debug.Log("The Builder save loaded");
    }
}
