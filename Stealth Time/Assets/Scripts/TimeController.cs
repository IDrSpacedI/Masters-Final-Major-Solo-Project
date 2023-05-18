using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
    public float slowdownFactor = 0.2f; // Determines the degree of time slowdown.
    public float slowdownDuration = 5f; // Determines how long the slowdown effect lasts.

    private float originalTimeScale;
    private float remainingTime;
    private bool isSlowdownActive;

    private float secondsLeft = 60;
    [SerializeField] private Slider timeSlide;

    private InputAction slowdownAction; // Reference to the Slowdown Input Action.

    private void Start()
    {
        originalTimeScale = Time.timeScale;
        ResetTime();

        // Set up the Slowdown Input Action
        slowdownAction = new InputAction("Slowdown", binding: "<Keyboard>/f");
        slowdownAction.performed += OnSlowdownPerformed;
        slowdownAction.canceled += OnSlowdownCanceled;
        slowdownAction.Enable();
    }

    private void Update()
    {
        timeSlide.value = secondsLeft;
        if (secondsLeft >= 0)
        {
            if (isSlowdownActive)
            {
                secondsLeft -= 1 * (Time.deltaTime * 20);
                remainingTime -= Time.unscaledDeltaTime;

                if (remainingTime <= 0)
                {
                    isSlowdownActive = false;
                    ResetTime();
                }
            }
            else
            {
                ResetTime();
                if (secondsLeft <= 60)
                {
                    secondsLeft += 2 * Time.deltaTime;
                }
            }
        }
        else
        {
            ResetTime();
            if (secondsLeft <= 60)
            {
                secondsLeft += 2 * Time.deltaTime;
            }
        }
    }

    private void OnSlowdownPerformed(InputAction.CallbackContext context)
    {
        if (!isSlowdownActive)
        {
            isSlowdownActive = true;
            Time.timeScale = slowdownFactor;
        }
    }

    private void OnSlowdownCanceled(InputAction.CallbackContext context)
    {
        isSlowdownActive = false;
        ResetTime();
    }

    private void ResetTime()
    {
        remainingTime = slowdownDuration;
        Time.timeScale = originalTimeScale;
    }

    private void OnDisable()
    {
        // Disable the Slowdown Input Action
        slowdownAction.Disable();
    }
}
