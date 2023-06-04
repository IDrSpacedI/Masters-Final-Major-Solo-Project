using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public Material[] materials; // Array of materials to adjust
    public float dissolveSpeed = 0.1f; // Speed of the dissolve transition

    private bool isDissolving = false; // Flag to check if currently dissolving
    private float dissolveAmount = 0f; // Current dissolve amount

    private void Start()
    {
        // Set the initial dissolve amount to 0
        foreach (Material material in materials)
            material.SetFloat("_Dissolve", 0f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isDissolving = !isDissolving; // Toggle dissolve state

            if (isDissolving)
                StartCoroutine(DissolveMaterials());
            else
                StartCoroutine(ReverseDissolveMaterials());
        }
    }

    private System.Collections.IEnumerator DissolveMaterials()
    {
        while (dissolveAmount < 0.7f)
        {
            dissolveAmount += dissolveSpeed * Time.deltaTime;

            foreach (Material material in materials)
                material.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }
    }

    private System.Collections.IEnumerator ReverseDissolveMaterials()
    {
        while (dissolveAmount > 0f)
        {
            dissolveAmount -= dissolveSpeed * Time.deltaTime;

            foreach (Material material in materials)
                material.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }
    }
}
