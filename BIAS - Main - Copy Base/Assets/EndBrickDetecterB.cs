using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBrickDetecterB : MonoBehaviour
{
    public BrickDetectorGold bDetector;
    [Header("Box Colliders")]
    public BoxCollider endBrickCounter2;
    [Space]
    [Header("Floats Various")]
    public float amountOfBricksInEndZone2;




    // Start is called before the first frame update
    void Start()
    {

        endBrickCounter2.enabled = false;
        amountOfBricksInEndZone2= 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (amountOfBricksInEndZone2 > 6)
        {
            bDetector.objective1bComplete = true;

        }

        else
        {
            bDetector.objective1bComplete = false;

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            amountOfBricksInEndZone2++;
        }
    }
}
