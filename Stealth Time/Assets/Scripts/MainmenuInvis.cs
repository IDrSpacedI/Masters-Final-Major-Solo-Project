using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainmenuInvis : MonoBehaviour
{
    public Material[] materials; // Array of materials to adjust
    public float dissolveSpeed = 0.1f; // Speed of the dissolve transition
    public float dissolveDelay = 1f; // Delay between dissolves

    private bool isDissolving = false; // Flag to check if currently dissolving
    private float dissolveAmount = 0f; // Current dissolve amount

    private void Start()
    {
        // Set the initial dissolve amount to 0
        foreach (Material material in materials)
            material.SetFloat("_Dissolve", 0f);

        // Start dissolving automatically after a 2-second delay
        StartCoroutine(StartDissolveCycle());
    }

    private IEnumerator StartDissolveCycle()
    {
        yield return new WaitForSeconds(2f);

        // Start dissolving automatically
        StartCoroutine(DissolveCycle());
    }

    private IEnumerator DissolveCycle()
    {
        while (true)
        {
            // Dissolve from 0 to 1
            yield return StartCoroutine(DissolveMaterials(0f, 1f));

            // Delay before starting the next dissolve
            yield return new WaitForSeconds(dissolveDelay);

            // Dissolve from 1 to 0
            yield return StartCoroutine(DissolveMaterials(1f, 0f));

            // Delay before starting the next dissolve
            yield return new WaitForSeconds(dissolveDelay);
        }
    }

    private IEnumerator DissolveMaterials(float startValue, float targetValue)
    {
        float t = 0f;

        while (t < 1f)
        {
            t += dissolveSpeed * Time.deltaTime;

            dissolveAmount = Mathf.Lerp(startValue, targetValue, t);

            foreach (Material material in materials)
                material.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }
    }
}
