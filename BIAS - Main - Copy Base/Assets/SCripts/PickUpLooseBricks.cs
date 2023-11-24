using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpLooseBricks : MonoBehaviour
{
    public bool hasThisObjectBeenCollected;

    private BuildersDetails bDeets;

    private void Awake()
    {
        bDeets = GameObject.FindGameObjectWithTag("manager").GetComponent<BuildersDetails>();
        //bDeets = GameObject.FindAnyObjectByType<BuildersDetails>();
        //bDeets = gameObjec findby(BuildersDetails);
    }

    private void OnMouseDown() // if mouse is over this object and clicked
    {

        Builder.instance.Add1ToBrickAmount();
        bDeets.SaveBuilderDetails();
        Destroy(this.gameObject);
        Destroy(transform.parent.gameObject);

    }
}
