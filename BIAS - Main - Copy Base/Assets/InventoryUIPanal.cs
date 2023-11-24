using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIPanal : MonoBehaviour
{

    public Button normalBrickButton, rotatedBrickButton, halfBrickButton, cementButton;


    // Start is called before the first frame update
    void Start()
    {
        PlayerController pControl = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        normalBrickButton.onClick.AddListener(SelectNormalBrick);
        rotatedBrickButton.onClick.AddListener(SelectRotatedBrick);
        halfBrickButton.onClick.AddListener(SelectHalfBrick);
        cementButton.onClick.AddListener(SelectCement);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectNormalBrick()
    {
        PlayerController.instance.obj_Array.arrayPos = 0;
        PlayerController.instance.isBuilding = true;
    }

    public void SelectRotatedBrick()
    {
        PlayerController.instance.obj_Array.arrayPos = 1;
        PlayerController.instance.isBuilding = true;
    }

    public void SelectHalfBrick()
    {
        PlayerController.instance.obj_Array.arrayPos = 2;
        PlayerController.instance.isBuilding = true;
    }

    public void SelectCement()
    {
        PlayerController.instance.obj_Array.arrayPos = 3;
        PlayerController.instance.isBuilding = true;
    }
}
