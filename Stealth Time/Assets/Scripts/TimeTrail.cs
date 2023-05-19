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

    [Header("Shader Related")]
    public string shaderVarRef;
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.5f;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;

    void Start()
    {
        // Get all SkinnedMeshRenderer components on the player
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
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

            foreach (SkinnedMeshRenderer skinnedMeshRenderer in skinnedMeshRenderers)
            {
                if (skinnedMeshRenderer != null)
                {
                    GameObject obj = new GameObject();
                    obj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                    MeshRenderer mr = obj.AddComponent<MeshRenderer>();
                    MeshFilter mf = obj.AddComponent<MeshFilter>();

                    Material[] materials = skinnedMeshRenderer.materials;
                    Material[] sharedMaterials = new Material[materials.Length];

                    for (int i = 0; i < materials.Length; i++)
                    {
                        Material newMaterial = new Material(materials[i]);
                        newMaterial.SetFloat(shaderVarRef, materials[i].GetFloat(shaderVarRef));
                        sharedMaterials[i] = newMaterial;
                    }

                    mr.materials = sharedMaterials;

                    Mesh mesh = new Mesh();
                    skinnedMeshRenderer.BakeMesh(mesh);

                    mf.mesh = mesh;

                    StartCoroutine(AnimateMaterialFloat(mr.materials, 0, shaderVarRate, shaderVarRefreshRate));

                    Destroy(obj, meshDestroyDelay);
                }
            }

            yield return new WaitForSeconds(meshRefreshRate);
        }

        isTrailActive = false;
    }

    IEnumerator AnimateMaterialFloat(Material[] materials, float goal, float rate, float refreshRate)
    {
        while (true)
        {
            bool allFinished = true;

            for (int i = 0; i < materials.Length; i++)
            {
                float valueToAnimate = materials[i].GetFloat(shaderVarRef);

                if (valueToAnimate > goal)
                {
                    allFinished = false;
                    valueToAnimate -= rate;
                    materials[i].SetFloat(shaderVarRef, valueToAnimate);
                }
            }

            if (allFinished)
                break;

            yield return new WaitForSeconds(refreshRate);
        }
    }
}
