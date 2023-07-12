using UnityEngine;
using TMPro;

public class Objective : MonoBehaviour
{
    public GameObject[] objectiveGameObjects; // Array of GameObjects representing the objectives
    public TextMeshProUGUI[] objectiveTexts; // Array of TextMeshProUGUI components representing the objectives' messages
    public string[] objectiveMessages; // Array of objective messages
    public KeyCode interactionKey = KeyCode.E; // Key to interact with the objectives

    private int currentObjectiveIndex = 0; // Index to track the current objective

    private void Start()
    {
        // Initialize the first objective message
        objectiveTexts[currentObjectiveIndex].text = objectiveMessages[currentObjectiveIndex];
    }

    private void Update()
    {
        // Check if the interaction key is pressed
        if (Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
    }

    private void Interact()
    {
        // Check if the current objective is active and not completed
        if (currentObjectiveIndex < objectiveGameObjects.Length && objectiveGameObjects[currentObjectiveIndex].activeSelf &&
            !objectiveTexts[currentObjectiveIndex].fontStyle.HasFlag(FontStyles.Strikethrough))
        {
            // Strike through the objective message
            objectiveTexts[currentObjectiveIndex].fontStyle |= FontStyles.Strikethrough;

            // Disable the current objective GameObject
            objectiveGameObjects[currentObjectiveIndex].SetActive(false);

            // Move to the next objective
            currentObjectiveIndex++;

            // Check if all objectives are completed
            if (currentObjectiveIndex >= objectiveGameObjects.Length)
            {
                // All objectives are completed
                Debug.Log("All objectives completed!");
                return;
            }

            // Enable the next objective GameObject
            objectiveGameObjects[currentObjectiveIndex].SetActive(true);

            // Update the objective text with the next message
            objectiveTexts[currentObjectiveIndex].text = objectiveMessages[currentObjectiveIndex];
        }
    }
}
