using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTrail : MonoBehaviour
{
    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefreshRate = 0.1f;
    public Transform positionToSpawn;
    public float meshDestroyDelay = 3f;

    [Header("Material Related")]
    public Material newMaterial; // Your own material

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    void Start()
    {
        // Get all SkinnedMeshRenderer components on the player
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // Replace materials with the new material
        foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
        {
            Material[] materials = new Material[skinnedMeshRenderer.sharedMaterials.Length];
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = newMaterial;
            }
            skinnedMeshRenderer.sharedMaterials = materials;
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.F) && !isTrailActive)
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

            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            {
                if (skinnedMeshRenderer != null)
                {
                    GameObject obj = new GameObject();
                    obj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                    MeshRenderer mr = obj.AddComponent<MeshRenderer>();
                    MeshFilter mf = obj.AddComponent<MeshFilter>();

                    Material[] materials = new Material[skinnedMeshRenderer.sharedMaterials.Length];

                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i] = newMaterial;
                    }

                    mr.materials = materials;

                    Mesh mesh = new Mesh();
                    skinnedMeshRenderer.BakeMesh(mesh);

                    mf.mesh = mesh;

                    Destroy(obj, meshDestroyDelay);
                }
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }
}
