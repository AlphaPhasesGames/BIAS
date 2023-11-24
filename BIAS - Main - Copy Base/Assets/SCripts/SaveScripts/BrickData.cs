using UnityEngine;


[System.Serializable]
public class BrickData
{
    public string brickType; // string to store brick type - for default brick
    public string brickType2; // string to store brick type 2 - for rotated brick
    public string brickType3; // string to store brick type 2 - for rotated brick
    public string brickType4;

    public float[] position; //  float array to store the position of the brick in the game world
    public float[] rotation; // float array to store the rotation of the brick in the game world

    public BrickData(BrickSave brickSave)  // class BrickData for default brick - inherits Bricksave script as bricksave variable
    {

        brickType = brickSave.brickName; // brickType is loaded from brickSave.brickname

        Quaternion brickRot = brickSave.transform.rotation; // rotation is the rotation of any default brick object with the Bricksave script attached
        Vector3 brickPos = brickSave.transform.position; // position is the position of any default brick object with the Bricksave script attached

        // We could do it the method directly below but we can make it look nicer - unlike the rest of this spagetti code
        // position[0] = brickPos.x;
        // position[1] = brickPos.y;
        // position[2] = brickPos.z;

        position = new float[] // position = a new float array
        {
           brickPos.x,brickPos.y,brickPos.z // store brick positions from X Y and Z coordinats 
       }; //  ; goes here for some reason - Unknown

        rotation = new float[] // rotation = a new float array
        {
            brickRot.x,brickRot.y,brickRot.z, brickRot.w // store brick rotation X Y and Z and W ( which is Width
        }; // ; goes here for some reason - Unknown
    }
    public BrickData(BrickSaveRotated brickSaveRotated)  // class BrickData for Rotated brick - inherits Bricksave script as bricksave variable
    {

        brickType2 = brickSaveRotated.brickName; // brickType is loaded from brickSave.brickname

        Quaternion brickRotatedRot = brickSaveRotated.transform.rotation; // rotation is the rotation of any rotated brick object with the Bricksave script attached
        Vector3 brickRotatedPos = brickSaveRotated.transform.position;  // position is the position of any rotated brick object with the Bricksave script attached

        position = new float[] // create position = a new float array
        {
           brickRotatedPos.x,brickRotatedPos.y,brickRotatedPos.z // store rotated brick positions from X Y and Z coordinats 
       };

        rotation = new float[]  // create rotated = a new float array
        {
            brickRotatedRot.x,brickRotatedRot.y,brickRotatedRot.z, brickRotatedRot.w  // store rotated brick rotation X Y and Z and W ( which is Width
        };
    }

    public BrickData(BrickHalfSave brickHalfSave)  // class BrickData for default brick - inherits Bricksave script as bricksave variable
    {

        brickType3 = brickHalfSave.brickName; // brickType is loaded from brickSave.brickname

        Quaternion brickHalfRot = brickHalfSave.transform.rotation; // rotation is the rotation of any default brick object with the Bricksave script attached
        Vector3 brickHalfPos = brickHalfSave.transform.position; // position is the position of any default brick object with the Bricksave script attached

        // We could do it the method directly below but we can make it look nicer - unlike the rest of this spagetti code
        // position[0] = brickPos.x;
        // position[1] = brickPos.y;
        // position[2] = brickPos.z;

        position = new float[] // position = a new float array
        {
           brickHalfPos.x,brickHalfPos.y,brickHalfPos.z // store brick positions from X Y and Z coordinats 
       }; //  ; goes here for some reason - Unknown

        rotation = new float[] // rotation = a new float array
        {
            brickHalfRot.x,brickHalfRot.y,brickHalfRot.z, brickHalfRot.w // store brick rotation X Y and Z and W ( which is Width
        }; // ; goes here for some reason - Unknown
    }

    public BrickData(CementSave cementSave)  // class BrickData for default brick - inherits Bricksave script as bricksave variable
    {

        brickType4 = cementSave.brickName; // brickType is loaded from brickSave.brickname

        Quaternion cementRot = cementSave.transform.rotation; // rotation is the rotation of any default brick object with the Bricksave script attached
        Vector3 cementPos = cementSave.transform.position; // position is the position of any default brick object with the Bricksave script attached

        // We could do it the method directly below but we can make it look nicer - unlike the rest of this spagetti code
        // position[0] = brickPos.x;
        // position[1] = brickPos.y;
        // position[2] = brickPos.z;

        position = new float[] // position = a new float array
        {
           cementPos.x,cementPos.y,cementPos.z // store brick positions from X Y and Z coordinats 
       }; //  ; goes here for some reason - Unknown

        rotation = new float[] // rotation = a new float array
        {
            cementRot.x,cementRot.y,cementRot.z, cementRot.w // store brick rotation X Y and Z and W ( which is Width
        }; // ; goes here for some reason - Unknown
    }

}
