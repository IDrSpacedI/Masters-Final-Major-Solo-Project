using UnityEngine;

public class SmoothTeleport : MonoBehaviour
{
    public float teleportDistance = 5f;
    public float teleportDuration = 1f;

    private Vector3 targetPosition;
    private bool isTeleporting = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !isTeleporting) // Change the input key as needed
        {
            StartTeleport();
        }
    }

    private void StartTeleport()
    {
        Vector3 forwardDirection = transform.forward;
        targetPosition = transform.position + forwardDirection * teleportDistance;
        StartCoroutine(TeleportCoroutine());
    }

    private System.Collections.IEnumerator TeleportCoroutine()
    {
        isTeleporting = true;
        float elapsedTime = 0f;
        Vector3 startingPosition = transform.position;

        while (elapsedTime < teleportDuration)
        {
            float t = elapsedTime / teleportDuration;
            transform.position = Vector3.Lerp(startingPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isTeleporting = false;
    }
}
