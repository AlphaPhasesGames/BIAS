using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideControls : MonoBehaviour
{
    [SerializeField] Button deletePlayerPrefs;
    public bool isHelpShowing;
    public bool isObjectivesShowing;
    public bool optionsMenuShowing;
    public GameObject helpPanal;
    public GameObject objectivePanal;
    public GameObject optionsMenu;
    // Start is called before the first frame update
    void Start()
    {
        isHelpShowing = true;
        isObjectivesShowing = true;
        optionsMenuShowing = false;

        Button btn = deletePlayerPrefs.GetComponent<Button>();
        btn.onClick.AddListener(DeletePlayerPrefs);

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            isHelpShowing = !isHelpShowing;

        }

        if (Input.GetKeyDown(KeyCode.O))
        {
           
            isObjectivesShowing = !isObjectivesShowing;

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            optionsMenuShowing = !optionsMenuShowing;

        }

        if (isHelpShowing)
        {
            helpPanal.SetActive(true);
        }

        else
        {
            helpPanal.SetActive(false);
        }

        if (isObjectivesShowing)
        {
            objectivePanal.SetActive(true);
        }

        else
        {
            objectivePanal.SetActive(false);
        }

        if (optionsMenuShowing)
        {
            optionsMenu.SetActive(true);
        }

        else
        {
            optionsMenu.SetActive(false);
        }

    }

    void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
