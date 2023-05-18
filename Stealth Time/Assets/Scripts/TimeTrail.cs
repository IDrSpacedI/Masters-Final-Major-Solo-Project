using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrail : MonoBehaviour
{
    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;

    private bool isTrailActive;
    private SkinnedMeshRenderer skinnedMeshRenderer;

    void Start()
    {
        // Get the SkinnedMeshRenderer component on the player
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
        }
    }

    IEnumerator ActivateTrail(float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= meshRefreshRate;

            if (skinnedMeshRenderer != null)
            {
                GameObject obj = new GameObject();
                obj.AddComponent<MeshRenderer>().sharedMaterial = skinnedMeshRenderer.sharedMaterial;
                obj.AddComponent<MeshFilter>().sharedMesh = skinnedMeshRenderer.sharedMesh;
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }
}
