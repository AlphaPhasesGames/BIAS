using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBrickDetecterA : MonoBehaviour
{
    public BrickDetectorGold bDetector;
    [Header("Box Colliders")]
    public BoxCollider endBrickCounter1;
    [Space]
    [Header("Floats Various")]
    public float amountOfBricksInEndZone1;
   
   


    // Start is called before the first frame update
    void Start()
    {
       
        endBrickCounter1.enabled = false;
        amountOfBricksInEndZone1 = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(amountOfBricksInEndZone1 > 6)
        {
            bDetector.objective1aComplete = true;
         
        }

        else
        {
            bDetector.objective1aComplete = false;
           
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Brick"))
        {
            amountOfBricksInEndZone1++;
        }
    }
}
