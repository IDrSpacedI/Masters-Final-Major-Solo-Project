using UnityEngine;
using TMPro;

public class Objective : MonoBehaviour
{
    public TMP_Text objectiveText; // Reference to the TextMeshPro UI component representing the objective
    public KeyCode interactionKey = KeyCode.E; // Key to interact with the objective

    private bool isCompleted = false; // Flag to track completion status

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Enable interaction prompt, show UI or any other desired behavior
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Disable interaction prompt, hide UI or any other desired behavior
        }
    }

    private void Update()
    {
        // Check if the player is inside the trigger and the interaction key is pressed
        if (isCompleted == false && objectiveText.fontStyle != FontStyles.Strikethrough && Input.GetKeyDown(interactionKey))
        {
            CompleteObjective();
        }
    }

    private void CompleteObjective()
    {
        // Toggle completion status
        isCompleted = true;

        // Update the objective text to show completion
        objectiveText.fontStyle = FontStyles.Strikethrough;

        // Disable interaction prompt, hide UI or any other desired behavior
    }
}
