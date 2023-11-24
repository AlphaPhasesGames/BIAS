using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopBrickDetector : MonoBehaviour
{
    public BrickDetectorGold bDetector;
    [Header("Box Colliders")]
    public BoxCollider topBrickCounter;
    [Space]
    [Header("Floats Various")]
    public float amountOfBricksOnTop;

    // Start is called before the first frame update
    void Start()
    {

        topBrickCounter.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfBricksOnTop > 5)
        {
            bDetector.objective2Complete = true;

        }
        if (amountOfBricksOnTop < 5)
        {
            bDetector.objective2Complete = false;

        }
       
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            amountOfBricksOnTop++;
        }
    }
}
