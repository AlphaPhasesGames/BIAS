using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BricksOnFloor : MonoBehaviour
{
    public BrickDetectorGold brickOnGround;
    [Header("Box Colliders for ground bricks")]
    public BoxCollider boxToCountMessyBricks;
    public BoxCollider boxToCountMessyBricks2;

    [Space]
    [Header("Canvas")]
    public Text bricksUiAmountOnFloor;

    [Space]
    [Header("Floats Various")]
    public float amountOfBricksOnfloor;
    public float totalBricksAmount;


    // Start is called before the first frame update
    void Start()
    {
        boxToCountMessyBricks.enabled = false;
        boxToCountMessyBricks2.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        totalBricksAmount = amountOfBricksOnfloor;
        if (brickOnGround.isTaskFinished)
        {
            bricksUiAmountOnFloor.text = "There are " + totalBricksAmount + " bricks On The floor, Clean Em Up";
        }
          
           
    }

    public void CountBricksOnFloor()
    {
        StartCoroutine(CleanUpBrics());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            amountOfBricksOnfloor++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            amountOfBricksOnfloor--;
        }
    }

    public IEnumerator CleanUpBrics()
    {
        boxToCountMessyBricks.enabled = true;
        boxToCountMessyBricks2.enabled = true;
        yield return new WaitForSeconds(3);
        boxToCountMessyBricks.enabled = false;
        boxToCountMessyBricks2.enabled = false;
        amountOfBricksOnfloor = 0f;
       // totalBricksAmount = 0f;
        
    }   
}
