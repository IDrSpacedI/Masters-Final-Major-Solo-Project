using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject objectivesUI;
    private bool objectivesVisible = false;

    private void Start()
    {
        objectivesUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            objectivesVisible = !objectivesVisible;
            objectivesUI.SetActive(objectivesVisible);
        }
    }

}
