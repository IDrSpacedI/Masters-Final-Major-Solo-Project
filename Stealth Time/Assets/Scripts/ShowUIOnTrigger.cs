using UnityEngine;

public class ShowUIOnTrigger : MonoBehaviour
{
    public GameObject uiToShow; // The UI GameObject to display when the player enters the trigger
    private bool playerInsideTrigger; // Flag to check if the player is inside the trigger

    private void Start()
    {
        uiToShow.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            uiToShow.SetActive(true); // Display the UI GameObject
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = false;
            uiToShow.SetActive(false); // Hide the UI GameObject
        }
    }

    // Optionally, you can add an Update loop to continuously check for user input or other interactions
    // and respond accordingly while the player is inside the trigger.
    private void Update()
    {
        if (playerInsideTrigger)
        {
            // Your additional logic or interactions can go here while the player is inside the trigger.
        }
    }
}
