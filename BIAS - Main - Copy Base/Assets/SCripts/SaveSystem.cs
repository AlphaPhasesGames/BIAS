using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; // we need this for binary formatting of saved
using UnityEngine.SceneManagement;
using System;

public class SaveSystem : MonoBehaviour
{

    //---------------- This is the save system to controls saving and loading of all building material

    public static SaveSystem instance;
    [SerializeField] BrickSave brickPrefab; // access the BrickSave file call it brickPrefab
    [SerializeField] BrickSaveRotated brickPrefabRotated; // access the BrickSaveRotated file call it brickPrefabRotated
    [SerializeField] BrickHalfSave halfBrickPrefab; // access the BrickHalfSafe halfBrickPrefab
    [SerializeField] CementSave cementBrickPrefab;


    public static List<BrickSave> bricks = new List<BrickSave>(); // create Bricksave list to store bricks
    public static List<BrickSaveRotated> bricksRotated = new List<BrickSaveRotated>(); // create BrickRotates list to store  Rotated bricks
    public static List<BrickHalfSave> bricksHalves = new List<BrickHalfSave>(); // create brick halves list to store half bricks
    public static List<CementSave> cementBrick = new List<CementSave>();


    const string BRICK_SUB = "/brick"; // create a string to store the location of brick saves - this is in appdata/locallow/alphaphases
    const string BRICK_COUNT_SUB = "/brick.count"; // create a string to store the count of bricks.count - this is in appdata/locallow/alphaphases

    const string BRICK_ROTATED_SUB = "/brickRotated"; // create a string to store the location of rotated brick saves - this is in appdata/locallow/alphaphases
    const string BRICK_ROTATED_COUNT_SUB = "/brickRotated.count"; // create a string to store the count of bricksRoated.count - this is in appdata/locallow/alphaphases

    const string BRICK_HALF_SUB = "/brickHalf"; // create a string to store the location of half brick saves - this is in appdata/locallow/alphaphases
    const string BRICK_HALF_COUNT_SUB = "/brickHalf.count"; // create a string to store the count of brickhalf.count - this is in appdata/locallow/alphaphases

    const string CEMENT_SUB = "/cementBrick"; // create a string to store the location of half brick saves - this is in appdata/locallow/alphaphases
    const string CEMENT_COUNT_SUB = "/cementBrick.count"; // create a string to store the count of brickhalf.count - this is in appdata/locallow/alphaphases

    const string DETAILS_SUB = "/details"; // create a string to store the location of brick saves - this is in appdata/locallow/alphaphases
   // const string DETAILS_COUNT_SUB = "/details.count"; // create a string to store the count of brickhalf.count - this is in appdata/locallow/alphaphases


    private void Awake() // on awake of script
    {
        instance = this;
        LoadBrick(); // load bricks save
        LoadRotatedBrick(); // load rotated bricks save
        LoadHalfBrick(); // load half bricks save/
        LoadCement();
        //LoadBuilderDetails();
        
    }
   
    private void OnApplicationQuit() // on quit in editor
    {
        SaveBrick();  // call SaveBrick function and save bricks pos and rot
        SaveRotatedBrick(); // call SaveRotatedbrick function and save rotated bricks pos and rot
        SaveHalfBrick(); // call SaveHalfBrick function and save half bricks pos and rot/
        SaveCement();
        //SaveBuildersDetails();
    }
    

    void SaveBrick() // save brick function
    {
        BinaryFormatter formatter = new BinaryFormatter(); // create a new BinaryFormatter
        string path = Application.persistentDataPath + BRICK_SUB + SceneManager.GetActiveScene().buildIndex; // create save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + BRICK_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // create save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 

        FileStream countStream = new FileStream(countPath, FileMode.Create); // create a new filestream

        formatter.Serialize(countStream, bricks.Count); // format saves using binary
        countStream.Close(); // close the count stream

        for (int i = 0; i < bricks.Count; i++) // for i is less that brickscount, increment i
        {
            FileStream stream = new FileStream(path + i, FileMode.Create); // create new filestream
            BrickData data = new BrickData(bricks[i]); // create new brickdata save file
            formatter.Serialize(stream, data); // serialize file
            stream.Close(); // close stream
        }
    }

    void LoadBrick() // load brick function
    {
        BinaryFormatter formatter = new BinaryFormatter(); // create a new BinaryFormatter
        string path = Application.persistentDataPath + BRICK_SUB + SceneManager.GetActiveScene().buildIndex; // load save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + BRICK_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // load save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 
        int brickCount = 0; // create brickCount int and initilize it as 0

        if (File.Exists(countPath)) // if there is a save file
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open); // open filestream
            brickCount = (int)formatter.Deserialize(countStream); // deserialize file and assign to brickCount
            countStream.Close(); // close stream
        }

        else // if there is no save file
        {
            Debug.LogError("Path not found in stream" + countPath); // display log in console
        }
        for (int i = 0; i < brickCount; i++) // for i less than brickCount, increment i
        {
            if (File.Exists(path + i)) //if a save file exists
            {
                FileStream stream = new FileStream(path + i, FileMode.Open); // open stream
                BrickData data = formatter.Deserialize(stream) as BrickData; // deserialize save from brick data

                stream.Close(); // close the stream
                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]); // set the bricks position to the values in the save file
                Quaternion rotation = new Quaternion(data.rotation[0], data.rotation[1], data.rotation[2], data.rotation[3]); // set the bricks rotation to the values in the save file

                BrickSave brick = Instantiate(brickPrefab, position, rotation); // instantiate brick from Bricksave Data in saved pos and rot
                brick.brickName = data.brickType; // set brick name to data in save file
            }

            else
            {
                Debug.LogError("Path not found in " + path + i); // display log in console
            }
        }
    }

    void SaveRotatedBrick() // save rotated brick function
    {
        BinaryFormatter formatter = new BinaryFormatter();  // create a new BinaryFormatter
        string path = Application.persistentDataPath + BRICK_ROTATED_SUB + SceneManager.GetActiveScene().buildIndex;  // create save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + BRICK_ROTATED_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // create save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 

        FileStream countStream = new FileStream(countPath, FileMode.Create); // create a new filestream

        formatter.Serialize(countStream, bricksRotated.Count); // format saves using binary
        countStream.Close(); // close the count stream

        for (int i = 0; i < bricksRotated.Count; i++) // for i is less that brickscount, increment i
        {
            FileStream stream = new FileStream(path + i, FileMode.Create); // create new filestream
            BrickData data = new BrickData(bricksRotated[i]); // create new brickdata save file
            formatter.Serialize(stream, data); // serialize file
            stream.Close();  // close stream
        }
    }

    void LoadRotatedBrick()  // load rotated brick function
    {
        BinaryFormatter formatter = new BinaryFormatter(); // create a new BinaryFormatter
        string path = Application.persistentDataPath + BRICK_ROTATED_SUB + SceneManager.GetActiveScene().buildIndex; // load save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + BRICK_ROTATED_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // load save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 
        int brickRotatedCount = 0; // create brickRotatedCount int and initilize it as 0
         
        if (File.Exists(countPath)) //if a save file exists
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open); // open stream
            brickRotatedCount = (int)formatter.Deserialize(countStream); // deserialize save from brickRotated data
            countStream.Close(); // close the stream

        }

        else // if there is no save file
        {
            Debug.LogError("Path not found in stream" + countPath); // display log in console
        }
        for (int i = 0; i < brickRotatedCount; i++) // for i less than brickCount, increment i
        {
            if (File.Exists(path + i)) //if a save file exists
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);  // open stream
                BrickData data = formatter.Deserialize(stream) as BrickData; // deserialize save from brick data

                stream.Close(); // close the stream
                Vector3 positionRotated = new Vector3(data.position[0], data.position[1], data.position[2]); // set the bricks position to the values in the save file
                Quaternion rotationRotated = new Quaternion(data.rotation[0], data.rotation[1], data.rotation[2], data.rotation[3]);  // set the bricks rotation to the values in the save file

                BrickSaveRotated brick = Instantiate(brickPrefabRotated, positionRotated, rotationRotated);  // instantiate brick from Bricksave Data in saved pos and rot
                brick.brickName = data.brickType; // set brick name to data in save file
            }

            else
            {
                Debug.LogError("Path not found in " + path + i); // display log in console
            }
        }
    }

    void SaveHalfBrick()  // save half rotated brick function
    {
        BinaryFormatter formatter = new BinaryFormatter(); // create a new BinaryFormatter
        string path = Application.persistentDataPath + BRICK_HALF_SUB + SceneManager.GetActiveScene().buildIndex; // create save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + BRICK_HALF_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // create save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 

        FileStream countStream = new FileStream(countPath, FileMode.Create);  // create a new filestream

        formatter.Serialize(countStream, bricksHalves.Count); // format saves using binary
        countStream.Close(); // close the count stream

        for (int i = 0; i < bricksHalves.Count; i++) // for i is less that brickscount, increment i
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);  // create new filestream
            BrickData data = new BrickData(bricksHalves[i]); // create new brickdata save file
            formatter.Serialize(stream, data); // serialize file
            stream.Close(); // close stream
        }
    }

    void LoadHalfBrick() //load half brick function
    {
        BinaryFormatter formatter = new BinaryFormatter(); // create a new BinaryFormatter
        string path = Application.persistentDataPath + BRICK_HALF_SUB + SceneManager.GetActiveScene().buildIndex; // load save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + BRICK_HALF_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // load save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 
        int brickHalfCount = 0;  // create brickRotatedCount int and initilize it as 0

        if (File.Exists(countPath))  //if a save file exists
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open); // open stream
            brickHalfCount = (int)formatter.Deserialize(countStream); // deserialize save from brick data
            countStream.Close(); // close the stream

        }

        else
        {
            Debug.LogError("Path not found in stream" + countPath); // display log in console
        }
        for (int i = 0; i < brickHalfCount; i++) // for i less than brickHalfCount, increment i
        {
            if (File.Exists(path + i)) //if a save file exists
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);  // open stream
                BrickData data = formatter.Deserialize(stream) as BrickData; // deserialize save from brick data

                stream.Close(); // close the stream
                Vector3 positionHalf = new Vector3(data.position[0], data.position[1], data.position[2]); // set the bricks position to the values in the save file
                Quaternion rotationHalf = new Quaternion(data.rotation[0], data.rotation[1], data.rotation[2], data.rotation[3]); // set the bricks rotation to the values in the save file

                BrickHalfSave brickHalf = Instantiate(halfBrickPrefab, positionHalf, rotationHalf); // instantiate brick from Bricksave Data in saved pos and rot
                brickHalf.brickName = data.brickType; // set brick name to data in save file
            }

            else
            {
                Debug.LogError("Path not found in " + path + i); // display log in console
            }
        }
    }

    void SaveCement() // save brick function
    {
        BinaryFormatter formatter = new BinaryFormatter(); // create a new BinaryFormatter
        string path = Application.persistentDataPath + CEMENT_SUB + SceneManager.GetActiveScene().buildIndex; // create save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + CEMENT_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // create save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 

        FileStream countStream = new FileStream(countPath, FileMode.Create); // create a new filestream

        formatter.Serialize(countStream, cementBrick.Count); // format saves using binary
        countStream.Close(); // close the count stream

        for (int i = 0; i < cementBrick.Count; i++) // for i is less that brickscount, increment i
        {
            FileStream stream = new FileStream(path + i, FileMode.Create); // create new filestream
            BrickData data = new BrickData(cementBrick[i]); // create new brickdata save file
            formatter.Serialize(stream, data); // serialize file
            stream.Close(); // close stream
        }

        Debug.Log("The Cement Saved");
    }

    void LoadCement() // load brick function
    {
        Debug.Log("Loaded 10%");
        BinaryFormatter formatter = new BinaryFormatter(); // create a new BinaryFormatter
        string path = Application.persistentDataPath + CEMENT_SUB + SceneManager.GetActiveScene().buildIndex; // load save path and use persistant data path plus the BRICK string plus the active scene. 
        string countPath = Application.persistentDataPath + CEMENT_COUNT_SUB + SceneManager.GetActiveScene().buildIndex; // load save count path and use persistant data path plus the BRICK_COUNT string plus the active scene. 
        int cementCount = 0; // create brickCount int and initilize it as 0

        if (File.Exists(countPath)) // if there is a save file
        {
            FileStream countStream = new FileStream(countPath, FileMode.Open); // open filestream
            cementCount = (int)formatter.Deserialize(countStream); // deserialize file and assign to brickCount
            countStream.Close(); // close stream
            Debug.Log("Loaded 20 File Exists%");
        }

        else // if there is no save file
        {
            Debug.LogError("Path not found in stream" + countPath); // display log in console
        }
        for (int i = 0; i < cementCount; i++) // for i less than brickCount, increment i
        {
            if (File.Exists(path + i)) //if a save file exists
            {
                FileStream stream = new FileStream(path + i, FileMode.Open); // open stream
                BrickData data = formatter.Deserialize(stream) as BrickData; // deserialize save from brick data

                stream.Close(); // close the stream
                Vector3 position = new Vector3(data.position[0], data.position[1], data.position[2]); // set the bricks position to the values in the save file
                Quaternion rotation = new Quaternion(data.rotation[0], data.rotation[1], data.rotation[2], data.rotation[3]); // set the bricks rotation to the values in the save file

                CementSave cement = Instantiate(cementBrickPrefab, position, rotation); // instantiate brick from Bricksave Data in saved pos and rot
                cement.brickName = data.brickType4; // set brick name to data in save file
                Debug.Log("Loaded 20 File Exists%");
            }

            else
            {
                Debug.LogError("Path not found in " + path + i); // display log in console
            }
        }
    }

    public static void SaveBuildersDetails(BuildersDetails buildersDetails) // static files can be accessed without an instance
    {
        BinaryFormatter formatter = new BinaryFormatter(); // create new binary formatter
        string path = Application.persistentDataPath + DETAILS_SUB + SceneManager.GetActiveScene().buildIndex; // create save path and use persistant data path plus the BRICK string plus the active scene. 
        FileStream stream = new FileStream(path, FileMode.Create); // create a new file on the system ready to be written too

        BuildersDetailsData data = new BuildersDetailsData(buildersDetails); // this runs all the data in PlayerData, so this sets itself up

        formatter.Serialize(stream, data); // write data to the file. with a stream and our playerdata in it.
        stream.Close(); // close the stream
    }

    public static BuildersDetailsData LoadBuilderDetails()
    {
        string path = Application.persistentDataPath + DETAILS_SUB + SceneManager.GetActiveScene().buildIndex; // create save path and use persistant data path plus the BRICK string plus the active scene. 
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            BuildersDetailsData data = formatter.Deserialize(stream) as BuildersDetailsData;

            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }


    public void SaveGameButton()
    {
        SaveBrick();
        SaveRotatedBrick();
    }

    public void LoadGameButton()
    {
        LoadBrick();
        LoadRotatedBrick();
    }
}
