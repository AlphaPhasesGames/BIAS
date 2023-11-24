using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuildersDetailsData
{

    public string buildersName; // declare string to store name of builder to save
    public float amountOfBricksCollected; // declare float for amount of bricks the player has picked up to be saved
    public float amountOfBrokenBricks; // declare float for amount of broken bricks the player is holding

    public BuildersDetailsData(BuildersDetails buildersDetails) //builders details data function - This is where the save data goes
    {
        buildersName = buildersDetails.buildersName; // builders name is the saved name from Builders Details
        amountOfBricksCollected = buildersDetails.amountOfBricksCollectedSaved; // Amount of Bricks the player is holding saved is the amount of bricks he has in Builders Details
        amountOfBrokenBricks = buildersDetails.amountOfBrokenBricks; // Amount of Broken Bricks the player is holding is the amount of broken bricks he has in Builders Details

    }
}
