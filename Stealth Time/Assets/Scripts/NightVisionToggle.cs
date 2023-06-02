using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightVisionToggle : MonoBehaviour
{
    public GameObject nightvision;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            nightvision.SetActive(!nightvision.activeInHierarchy);
    }
}
