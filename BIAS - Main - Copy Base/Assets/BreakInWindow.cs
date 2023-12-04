using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakInWindow : MonoBehaviour
{
    public Animator windowOpen;
    public bool canOpenWindow;
    public GameObject player;
    public Transform placeToMovePLayer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canOpenWindow)
            {
                windowOpen.SetBool("openWindow", true);
                player.transform.position = placeToMovePLayer.position;
            }
        }
       
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpenWindow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canOpenWindow = false;
        }
    }
}
