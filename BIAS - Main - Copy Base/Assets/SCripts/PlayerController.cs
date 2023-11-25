using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [Header("Array for objects")]
    public ObjectArray obj_Array;

    [Space]
    [Header("Controller Scripts")]
    public static PlayerController instance;
    public CharacterController charControl;
    public Builder builder;

    [Space]
    [Header("Navmesh and cameras")]
    [SerializeField] // so we can access it in the inspector but keep it private
    private NavMeshAgent agent; // declare NavMeshAgent for player called agent
    public Camera mainCam; // declare camera to store our camera from the inspector
    public Camera fPSCam; // declare camera to store the FPS cam -- EXPERIMENTAL
    public Vector3 currentPostition; // a vector 3 to hold the players current position - for the navemesh
                                     // public Vector3 transBrickposition;
    public Ray cameraRay; // public ray to detect when the player is over ground/brick etc
    RaycastHit cameraRayHit; // raycasthit to detect when the ray has hit something
    RaycastHit hit; // = Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition));
    public Vector3 velocity; // instantiate a vector3 called velocity, to move the player down, to simulate gravity
                             // [Header("UI Canvas")]
                             // public GameObject helpCanvasToHide;

    [Header("Textures")]
    public Texture2D cursorHammerTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    [Space]
    [Header("Floats for FPS")]
    public float MovementSpeed = 1;
    public float Gravity = 9.8f;
    public float speed = 10f;

    public float bricksThePlayerHasToBuildWith;
    //private float velocity = 0;

    [Space]
    [Header("Bools")]
    public bool isBuilding; // declare bool to detect when were building or not
    public bool isTransBrickShowing; // declare bool to detect when the transulcent brick is showing us where our bricks will be placed
    public bool brickForwardDirection; // declare bool to detect when  the brick is in the default position
    public bool transBrickForwardDirection; // declare bool to detect when the translucent brick is showing in the default rotation
    public bool playerBuildingLayer2; // Decalre bool to detect if were building above ground layer
    public bool halfBrickForwardDirection;
    public bool transHalfBrickForwardDirection;
    public bool buildingOtherBricks;
    public bool playerFirstPerson; // bool to detect wether the player is in First Person 
    public bool deleteBrickActive; // declare bool to detect when the destroy bricks mode is active
    public bool pickUpCollectableActive;
    public bool isHelpShowing;
    public LayerMask ignoreMe;
    [Space]
    [Header("List of Bricks")]
    public List<GameObject> bricks = new List<GameObject>(); //  list to store bricks create in called bricks
    [Header("Game Objects - bricks and building stuff")]
    public GameObject brickToBuild; // gameobject to hold brick object
    public GameObject brickToBuildRotated; // gameobject to hold the brick when rotated object
    public GameObject brickToBuildLayer2; //  Gameobject to hold the brick when on a higher level object
    public GameObject brickToBuildLayer2Rotated; // Game object to hold the brick then on a higher level rotated
    public GameObject brickHalfToBuild;
    public GameObject cementToBuild;

    public GameObject transBrick; // gameobject to hold the transparent brick icon 
    public GameObject transBrickRotated; // game object to hold the transparent brick icon when rotated
    public GameObject transHalfBrick; // gameobject to hold the transparent brick icon 
    public GameObject transCementBrick;


    private void Awake() // on awake of script
    {
        isBuilding = false; // player is not building on awake and can move
        brickForwardDirection = false; // Brick is in default position
        transBrickForwardDirection = false;   //Trasparent brick is in default position
        isTransBrickShowing = false; // trasparent brick is hideen 
        transBrick.SetActive(false); //set transparent brick to inactive
        transHalfBrick.SetActive(false);
        transBrickRotated.SetActive(false); // set trasparent brick rotated to inactive
       // transCementBrick.SetActive(false);
        playerBuildingLayer2 = false; // player is using ground bricks by default
        halfBrickForwardDirection = false;
        transHalfBrickForwardDirection = false;
        deleteBrickActive = false; // brick deleter is inactive
        fPSCam.enabled = false; // FPS cam is disabled 
        instance = this; // instance of this script, so we can access it from other places
        isHelpShowing = true;
        buildingOtherBricks = false;
        pickUpCollectableActive = false;

        //testBrick.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // get NavMeshAgent component and apply it to agent .. this is redundant, we have set it in the inspector 
        bricksThePlayerHasToBuildWith = builder.amountOfBricksCollected;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.L))
        {

            halfBrickForwardDirection = !halfBrickForwardDirection;
            transHalfBrickForwardDirection = !transHalfBrickForwardDirection;
            buildingOtherBricks = !buildingOtherBricks;
            pickUpCollectableActive = !pickUpCollectableActive;
        }

        if (Input.GetMouseButtonDown(1)) // if RMB is pressed
        {
            isBuilding = !isBuilding; // building mode actived - and deactivated if clicked again 
            deleteBrickActive = false; // disable delete tool if its active
        }

        //  transBrickposition = new Vector3(0,500, 0);

        if (Input.GetKeyDown(KeyCode.E)) // if E is pressed
        {
            brickForwardDirection = !brickForwardDirection; // change brick direction to and from the default and rotated brick
            transBrickForwardDirection = !transBrickForwardDirection;  // change transparent brick direction to and from the default and rotated brick
        }
        // 
        if (Input.GetKeyDown(KeyCode.B)) // if B is pressed
        {

            deleteBrickActive = !deleteBrickActive; // enable or disable the brick delete tool
            Cursor.SetCursor(cursorHammerTexture, hotSpot, cursorMode);
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            isHelpShowing = !isHelpShowing;
        }

        if (Input.GetKeyDown(KeyCode.V)) // if V is pressed
        {
            playerFirstPerson = !playerFirstPerson; // enable and disable the first person camera
        }


        if (!playerFirstPerson) // if FPS cam is disabled
        {
            fPSCam.enabled = false; // disable FPS cam
            mainCam.enabled = true; // Enable Main Camera
            charControl.enabled = false;
        }

        if (playerFirstPerson) // if FPS cam is enabled
        {
            fPSCam.enabled = true; // Enable FPS cam
            mainCam.enabled = false; // disable main camera
            charControl.enabled = true;
            isBuilding = false;

            float x = Input.GetAxis("Horizontal"); // declare float x and set it to the horizontal controls
            float z = Input.GetAxis("Vertical"); // declare float z and set it to the vertical controlls

            charControl.Move(velocity * Time.deltaTime); // characther controler move function velocity by time.deltatime
            velocity.y += Gravity * Time.deltaTime; // set velocity of downward force as gravity variable by time.delta time. 
            Vector3 move = transform.right * x + transform.forward * z; // vector3 move equals transform.right multiplied by x plus transform forward multiplied by z
            charControl.Move(move * speed * Time.deltaTime); // controller move is move mltiplied by speed by time.deltatime

        }

        if (!isBuilding) // if the player is not building -- if the player is moving
        {
            if (!deleteBrickActive)
            {
                if (!playerFirstPerson)
                {
                    transBrick.SetActive(false); // set transparent brick to inactive
                    transBrickRotated.SetActive(false); // set rotated transpare brick to inactive
                    transHalfBrick.SetActive(false);
                    //deleteBrickActive = false;
                    if (Input.GetMouseButtonDown(0) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject()) // on mouse click
                    {
                        Ray position = mainCam.ScreenPointToRay(Input.mousePosition); // new RAY decalred as position and is set to the mouse position called once per frame
                        RaycastHit hitInfo; // get a raycast variable to detect what we hit
                        if (Physics.Raycast(position, out hit, 100f)) // if the raycast from position hits in distance of 100f
                        {
                            Debug.Log(hit.point); // log the position it hits
                            agent.destination = hit.point; // move agent (player) to the hit position.
                        }

                        if (Physics.Raycast(position, out hitInfo, 100f)) // if the raycast from position hits in distance of 100f
                        {
                            Debug.Log("We Hit: " + hitInfo.collider); // log the position it hits
                        }
                        Debug.Log(hit.collider); // log the collider it hits
                    }
                }
            }
        }

        if (isBuilding)
        {
            if (builder.amountOfBricksCollected >= 1f )
            {
                //deleteBrickActive = false;
                cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition); // cameraRay decalred as position and shoots our from the middle of the screen to the mouse position and is called once per frame
                if (Physics.Raycast(cameraRay, out cameraRayHit)) // if raycast hits something
                {
                    if (cameraRayHit.transform.CompareTag("Ground")) // if raycast hits the ground
                    {
                        if (obj_Array.arrayPos == 0) // if transpare brick in the default position 
                        {
                            transBrick.transform.position = cameraRayHit.point; // transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transBrick.SetActive(true); // set traparent brick to active
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transHalfBrick.SetActive(false); // set traparent brick to inactive
                            transCementBrick.SetActive(false); // set traparent brick to inactive transCementBrick.SetActive(true); // set traparent brick to inactive                           // brickHalfToBuild.SetActive(false); // set traparent brick to inactive
                        }

                        if (obj_Array.arrayPos == 1)  // if transpare brick in the rotated position 
                        {
                            transBrickRotated.transform.position = cameraRayHit.point; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transBrickRotated.SetActive(true); // set traparent brick to inactive
                            transBrick.SetActive(false); // enable the rotated traparent brick
                            transHalfBrick.SetActive(false); // set traparent brick to inactive
                            transCementBrick.SetActive(false); // set traparent brick to inactive                                //  brickHalfToBuild.SetActive(false); // set traparent brick to inactive
                        }

                        if (obj_Array.arrayPos == 2)  // if transpare brick in the rotated position 
                        {
                            transHalfBrick.transform.position = cameraRayHit.point; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transHalfBrick.SetActive(true); // set traparent brick to inactive
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transBrick.SetActive(false); // enable the rotated traparent brick
                            transCementBrick.SetActive(false); // set traparent brick to inactive
                        }

                        if (obj_Array.arrayPos == 3)  // if transpare brick in the rotated position 
                        {
                            transCementBrick.transform.position = cameraRayHit.point; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transCementBrick.SetActive(true); // set traparent brick to inactive
                            transHalfBrick.SetActive(false);
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transBrick.SetActive(false); // enable the rotated traparent brick

                        }

                    }

                    if (cameraRayHit.transform.CompareTag("Brick")) // if raycast hits the ground
                    {
                        if (obj_Array.arrayPos == 0) // if transpare brick in the default position 
                        {
                            transBrick.transform.position = cameraRayHit.point;//transform.position; // transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transBrick.SetActive(true); // set traparent brick to active
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transCementBrick.SetActive(false); // set traparent brick to inactive  
                            transHalfBrick.SetActive(false); // set traparent brick to inactive// brickHalfToBuild.SetActive(false); // set traparent brick to inactive
                        }

                        if (obj_Array.arrayPos == 1)  // if transpare brick in the rotated position 
                        {
                            transBrickRotated.transform.position = cameraRayHit.point;//transform.position; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transBrickRotated.SetActive(true); // set traparent brick to inactive
                            transBrick.SetActive(false); // enable the rotated traparent brick
                            transCementBrick.SetActive(false); // set traparent brick to inactive 
                            transHalfBrick.SetActive(false); // set traparent brick to inactive// brickHalfToBuild.SetActive(false); // set traparent brick to inact// brickHalfToBuild.SetActive(false); // set traparent brick to inactive
                        }

                        if (obj_Array.arrayPos == 2)  // if transpare brick in the rotated position 
                        {
                            transHalfBrick.transform.position = cameraRayHit.point; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transHalfBrick.SetActive(true); // set traparent brick to inactive
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transCementBrick.SetActive(false); // set traparent brick to inactive   
                            transBrick.SetActive(false); // set traparent brick to active// transBrick.SetActive(false); // enable the rotated traparent brick
                        }

                        if (obj_Array.arrayPos == 3)  // if transpare brick in the rotated position 
                        {
                            transCementBrick.transform.position = cameraRayHit.point; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transCementBrick.SetActive(true); // set traparent brick to inactive
                            transHalfBrick.SetActive(false); // set traparent brick to inactive
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transBrick.SetActive(false); // set traparent brick to active// transBrick.SetActive(false); // enable the rotated traparent brick
                        }
                    }

                    if (cameraRayHit.transform.CompareTag("Cement")) // if raycast hits the ground
                    {
                        if (obj_Array.arrayPos == 0) // if transpare brick in the default position 
                        {
                            transBrick.transform.position = cameraRayHit.point;//transform.position; // transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transBrick.SetActive(true); // set traparent brick to active
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transCementBrick.SetActive(false); // set traparent brick to inactive  
                            transHalfBrick.SetActive(false); // set traparent brick to inactive// brickHalfToBuild.SetActive(false); // set traparent brick to inactive
                        }

                        if (obj_Array.arrayPos == 1)  // if transpare brick in the rotated position 
                        {
                            transBrickRotated.transform.position = cameraRayHit.point;//transform.position; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transBrickRotated.SetActive(true); // set traparent brick to inactive
                            transBrick.SetActive(false); // enable the rotated traparent brick
                            transCementBrick.SetActive(false); // set traparent brick to inactive 
                            transHalfBrick.SetActive(false); // set traparent brick to inactive// brickHalfToBuild.SetActive(false); // set traparent brick to inact// brickHalfToBuild.SetActive(false); // set traparent brick to inactive
                        }

                        if (obj_Array.arrayPos == 2)  // if transpare brick in the rotated position 
                        {
                            transHalfBrick.transform.position = cameraRayHit.point; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transHalfBrick.SetActive(true); // set traparent brick to inactive
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transCementBrick.SetActive(false); // set traparent brick to inactive   
                            transBrick.SetActive(false); // set traparent brick to active// transBrick.SetActive(false); // enable the rotated traparent brick
                        }

                        if (obj_Array.arrayPos == 3)  // if transpare brick in the rotated position 
                        {
                            transCementBrick.transform.position = cameraRayHit.point; // Rotated transparent brick position is where the raycast hits the ground -- this is important as if not set to the ground, the traparent brick can float anywhere between the two raycast position ( raycast location and the hit point)
                            transCementBrick.SetActive(true); // set traparent brick to inactive
                            transHalfBrick.SetActive(false); // set traparent brick to inactive
                            transBrickRotated.SetActive(false); // disable the rotated traparent brick
                            transBrick.SetActive(false); // set traparent brick to active// transBrick.SetActive(false); // enable the rotated traparent brick
                        }
                    }
                }


                if (obj_Array.arrayPos == 0) // if Brick is in the default postion 
                {
                    // deleteBrickActive = false;
                    if (Input.GetMouseButtonDown(0)) // on mouse click
                    {

                        Ray position = mainCam.ScreenPointToRay(Input.mousePosition); // new RAY decalred as position and is set to the mouse position called once per frame
                        if (Physics.Raycast(position, out hit, 100f)) // if the raycast from position hits in distance of 100f
                        {
                            if (hit.transform.CompareTag("Ground")) // if raycast hits ground
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the tag it hits
                                Instantiate(brickToBuild, hit.point, Quaternion.identity); // create brick at hitpoint(Ground) in its original rotation;
                                bricks.Add(brickToBuild); //  add brick to list of bricks
                                builder.Remove1ToBrickAmount();
                            }

                            if (hit.transform.CompareTag("Brick"))  // if raycast hits bricks
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the position it hits
                                                              //agent.destination = hit.point; // move agent (player) to the hit position.
                                Instantiate(brickToBuildLayer2, hit.point, Quaternion.identity); // create brick upper layer at hitpoint(Upper Bricks) in its original rotation;
                                bricks.Add(brickToBuildLayer2); // add upper layer bricks to list of bricks
                                builder.Remove1ToBrickAmount();
                            }

                            if (hit.transform.CompareTag("Cement"))  // if raycast hits bricks
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the position it hits
                                                              //agent.destination = hit.point; // move agent (player) to the hit position.
                                Instantiate(brickToBuildLayer2, hit.point, Quaternion.identity); // create brick upper layer at hitpoint(Upper Bricks) in its original rotation;
                                bricks.Add(brickToBuildLayer2); // add upper layer bricks to list of bricks
                                builder.Remove1ToBrickAmount();
                            }


                        }
                        Debug.Log(hit.collider); // log the collider it hits
                    }
                }
                if (obj_Array.arrayPos == 1) // if brick is in rotated position
                {
                    // deleteBrickActive = false;
                    if (Input.GetMouseButtonDown(0)) // on mouse click
                    {
                        Ray position = mainCam.ScreenPointToRay(Input.mousePosition); // new RAY decalred as position and is set to the mouse position called once per frame
                        if (Physics.Raycast(position, out hit, 100f)) // if the raycast from position hits in distance of 100f
                        {
                            if (hit.transform.CompareTag("Ground")) // if raycst detects tag Ground
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the Tag it hits                           
                                Instantiate(brickToBuildRotated, hit.point, Quaternion.identity); // create rotated brick at hitpoint ( The ground ) in its original rotation;
                                bricks.Add(brickToBuildRotated); // add this rotated brick to the list of bricks
                                builder.Remove1ToBrickAmount();
                            }

                            if (hit.transform.CompareTag("Brick"))
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the tag it hits
                                Instantiate(brickToBuildLayer2Rotated, hit.point, Quaternion.identity); // create rotated brick upper layer at hitpoint ( Rotated Bricks ) in its original rotation;
                                bricks.Add(brickToBuildLayer2Rotated); //  add upper layer rotated brick to list of bricks
                                builder.Remove1ToBrickAmount();
                            }

                            if (hit.transform.CompareTag("Cement"))
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the tag it hits
                                Instantiate(brickToBuildLayer2Rotated, hit.point, Quaternion.identity); // create rotated brick upper layer at hitpoint ( Rotated Bricks ) in its original rotation;
                                bricks.Add(brickToBuildLayer2Rotated); //  add upper layer rotated brick to list of bricks
                                builder.Remove1ToBrickAmount();
                            }
                        }
                        Debug.Log(hit.collider); // log the collider it hits
                    }
                }
                if (obj_Array.arrayPos == 2) // if Brick is in the default postion 
                {
                    // brickForwardDirection = false;
                    Debug.Log("This executes");
                    // deleteBrickActive = false;
                    if (Input.GetMouseButtonDown(0)) // on mouse click
                    {
                        Debug.Log("This code exectues as wekk");
                        Ray position = mainCam.ScreenPointToRay(Input.mousePosition); // new RAY decalred as position and is set to the mouse position called once per frame
                        if (Physics.Raycast(position, out hit, 100f)) // if the raycast from position hits in distance of 100f
                        {
                            if (hit.transform.CompareTag("Ground")) // if raycast hits ground
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the tag it hits
                                Instantiate(brickHalfToBuild, hit.point, Quaternion.identity); // create brick at hitpoint(Ground) in its original rotation;
                                bricks.Add(brickHalfToBuild); //  add brick to list of bricks
                                builder.Remove1ToBrickAmount();

                            }

                            if (hit.transform.CompareTag("Brick"))  // if raycast hits bricks
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the position it hits
                                                              //agent.destination = hit.point; // move agent (player) to the hit position.
                                Instantiate(brickHalfToBuild, hit.point, Quaternion.identity); // create brick upper layer at hitpoint(Upper Bricks) in its original rotation;
                                bricks.Add(brickHalfToBuild); // add upper layer bricks to list of bricks
                                builder.Remove1ToBrickAmount();
                            }

                            if (hit.transform.CompareTag("Cement"))  // if raycast hits bricks
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the position it hits
                                                              //agent.destination = hit.point; // move agent (player) to the hit position.
                                Instantiate(brickHalfToBuild, hit.point, Quaternion.identity); // create brick upper layer at hitpoint(Upper Bricks) in its original rotation;
                                bricks.Add(brickHalfToBuild); // add upper layer bricks to list of bricks
                                builder.Remove1ToBrickAmount();
                            }
                        }
                        Debug.Log(hit.collider); // log the collider it hits
                    }

                }
                if (obj_Array.arrayPos == 3) // if Brick is in the default postion 
                {
                    // brickForwardDirection = false;
                    Debug.Log("This executes");
                    // deleteBrickActive = false;
                    if (Input.GetMouseButtonDown(0)) // on mouse click
                    {
                        Debug.Log("This code exectues as wekk");
                        Ray position = mainCam.ScreenPointToRay(Input.mousePosition); // new RAY decalred as position and is set to the mouse position called once per frame
                        if (Physics.Raycast(position, out hit, 100f)) // if the raycast from position hits in distance of 100f
                        {
                            if (hit.transform.CompareTag("Ground")) // if raycast hits ground
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the tag it hits
                                Instantiate(cementToBuild, hit.point, Quaternion.identity); // create brick at hitpoint(Ground) in its original rotation;
                                bricks.Add(cementToBuild); //  add brick to list of bricks
                              //  builder.Remove1ToBrickAmount();

                            }

                            if (hit.transform.CompareTag("Brick"))  // if raycast hits bricks
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the position it hits
                                                              //agent.destination = hit.point; // move agent (player) to the hit position.
                                Instantiate(cementToBuild, hit.point, Quaternion.identity); // create brick upper layer at hitpoint(Upper Bricks) in its original rotation;
                                bricks.Add(cementToBuild); // add upper layer bricks to list of bricks
                                //builder.Remove1ToBrickAmount();
                            }

                            if (hit.transform.CompareTag("Cement"))  // if raycast hits bricks
                            {
                                Debug.Log(hit.point); // log the position it hits
                                Debug.Log(hit.transform.tag); // log the position it hits
                                                              //agent.destination = hit.point; // move agent (player) to the hit position.
                                Instantiate(cementToBuild, hit.point, Quaternion.identity); // create brick upper layer at hitpoint(Upper Bricks) in its original rotation;
                                bricks.Add(cementToBuild); // add upper layer bricks to list of bricks
                                //builder.Remove1ToBrickAmount();
                            }
                        }
                        Debug.Log(hit.collider); // log the collider it hits
                    }
                }

                if (deleteBrickActive)
                {
                    isBuilding = false;
                    transBrick.SetActive(false); // set transparent brick to inactive
                    transBrickRotated.SetActive(false); // set rotated transpare brick to inactive
                    transHalfBrick.SetActive(false);
                    transCementBrick.SetActive(false);
                    Cursor.SetCursor(cursorHammerTexture, hotSpot, cursorMode);

                }

                else
                {
                    Cursor.SetCursor(null, Vector2.zero, cursorMode);
                }

            }

        }
    }
}


