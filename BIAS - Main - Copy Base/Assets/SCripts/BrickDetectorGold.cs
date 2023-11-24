using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrickDetectorGold : MonoBehaviour
{
    public BricksOnFloor bricks;
    public EndBrickDetecterA endBrickDetecta;
    public EndBrickDetecterB endBrickDetectb;
    public TopBrickDetector topBrickDetect;
    [Header("Box Colliders")]
    public BoxCollider boxToCountBricks;
    public BoxCollider endBrickCounter1;
    public BoxCollider endBrickCounter2;
    public BoxCollider topBrickDetector;

   [Header("Ticks and Crosses")]
    public GameObject tick1;
    public GameObject cross1;

    public GameObject tick2;
    public GameObject cross2;

    [Space]
    [Header("Canvas")]
    public Canvas winUI;
    public Text bricksUiAmount;
       
    [Space]
    [Header("Floats Various")]
    public float amountOfBricks;
    public float totalBricksAmount;

    
    ///public float amountOfBricksInEndZone1;
    ///public float amountOfBricksInEndZone2;

    [Space]
    [Header("Bools")]
    public bool isTaskFinished;
    public bool objective1aComplete;
    public bool objective1bComplete;
    public bool objective2Complete;
    // Start is called before the first frame update
    void Start()
    {
        tick1.SetActive(false);
        cross1.SetActive(false);
        tick2.SetActive(false);
        cross2.SetActive(false);
        boxToCountBricks.enabled = false;
        endBrickCounter1.enabled = false;
        endBrickCounter2.enabled = false;
        isTaskFinished = false;
        winUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTaskFinished)
        {
            totalBricksAmount = amountOfBricks;
            if(totalBricksAmount < 50f)
            {
                winUI.enabled = true;
                bricksUiAmount.enabled = true;
                bricksUiAmount.text = "You are currently using: " + amountOfBricks + " " + " Your best brick count it: " + totalBricksAmount + " Bricks!".ToString();
            }
        }

        if(objective1aComplete && objective1bComplete)
        {
            tick1.SetActive(true);
            cross1.SetActive(false);
        }

        if (objective2Complete)
        {
            tick2.SetActive(true);
            cross2.SetActive(false);
        }

        else
        {
            tick1.SetActive(false);
            cross1.SetActive(true);
            tick2.SetActive(false);
            cross2.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Brick"))
        {
           amountOfBricks++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            amountOfBricks--;
        }
    }

    public void FinishTaskButton()
    {
        StartCoroutine(CountTheBricks());
        StartCoroutine(bricks.CleanUpBrics());


    }

    public IEnumerator CountTheBricks()
    {
        boxToCountBricks.enabled = true;
        endBrickCounter1.enabled = true;
        endBrickCounter2.enabled = true;
        topBrickDetector.enabled = true;
        isTaskFinished = true;
        yield return new WaitForSeconds(3);
        isTaskFinished = false;
        boxToCountBricks.enabled = false;
        endBrickCounter1.enabled = false;
        endBrickCounter2.enabled = false;
        topBrickDetector.enabled = false;
        amountOfBricks = 0f;
        if (!objective1aComplete && !objective1bComplete && !objective2Complete)
        {
           
            endBrickDetecta.amountOfBricksInEndZone1 = 0f;
            endBrickDetectb.amountOfBricksInEndZone2 = 0f;
            topBrickDetect.amountOfBricksOnTop = 0f;
        }
        

    }


}
