using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Objective : MonoBehaviour
{
    public string objectiveID; // Unique identifier for the objective
    public TextMeshProUGUI objectiveText; // Reference to the TextMeshProUGUI component representing the objective
    public KeyCode interactionKey = KeyCode.E; // Key to interact with the objective

    private static Dictionary<string, bool> objectiveCompletionStatus = new Dictionary<string, bool>();

    private void Start()
    {
        // Initialize the completion status for this objective
        if (!objectiveCompletionStatus.ContainsKey(objectiveID))
        {
            objectiveCompletionStatus.Add(objectiveID, false);
        }
    }

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
        if (!objectiveCompletionStatus[objectiveID] && Input.GetKeyDown(interactionKey))
        {
            CompleteObjective();
        }
    }

    private void CompleteObjective()
    {
        // Toggle completion status
        objectiveCompletionStatus[objectiveID] = true;

        // Apply strikethrough effect
        objectiveText.fontStyle = FontStyles.Strikethrough;

        // Disable interaction prompt, hide UI or any other desired behavior
    }
}
