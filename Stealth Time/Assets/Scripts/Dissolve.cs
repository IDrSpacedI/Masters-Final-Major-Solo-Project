using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dissolve : MonoBehaviour
{
    public Material[] materials; // Array of materials to adjust
    public float dissolveSpeed = 0.1f; // Speed of the dissolve transition

    private bool isDissolving = false; // Flag to check if currently dissolving
    private float dissolveAmount = 0f; // Current dissolve amount

    public Slider countdownSlider; // Reference to the UI slider
    public float countdownDuration = 30f; // Duration of the countdown in seconds

    private bool isCountingDown = false; // Flag to check if currently counting down
    private float countdownTime = 0f; // Current countdown time

    private bool stopdissolve = false;

    private void Start()
    {
        // Set the initial dissolve amount to 0
        foreach (Material material in materials)
            material.SetFloat("_Dissolve", 0f);

        // Set the initial value of the slider
        countdownSlider.value = countdownSlider.maxValue;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !stopdissolve)
        {
            isDissolving = !isDissolving; // Toggle dissolve state

            if (isDissolving)
                StartCoroutine(DissolveMaterials());
            else
                StartCoroutine(ReverseDissolveMaterials());


            if (!isCountingDown)
            {
                StartCountdown();
            }
            else
            {
                StopCountdown();
            }
        }

        if (isCountingDown)
        {
            countdownTime -= Time.deltaTime;

            if (countdownTime <= 0f)
            {
                StopCountdown();
                StartCoroutine(ReverseDissolveMaterials());
                stopdissolve = true;
                countdownSlider.value = 0f;
            }
            else
            {
                countdownSlider.value = countdownTime;
            }
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

    private void StartCountdown()
    {
        isCountingDown = true;
        countdownTime = countdownDuration;
        countdownSlider.maxValue = countdownDuration;
        countdownSlider.value = countdownDuration;
    }

    private void StopCountdown()
    {
        isCountingDown = false;
        countdownSlider.value = countdownSlider.maxValue;
    }
}