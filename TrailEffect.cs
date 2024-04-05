using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrailEffect : MonoBehaviour
{
    public float activeTime = 1f;
    private bool isTrailActive;
    public float cooldownEndTime;
    private float dashCooldownTime = 3f;

    [Header("Mesh Related")]
    public float mesRefreshRate = 0.1f;
    public Transform positionToSpawn;
    public float mesDestroyDelay = 1f;

    [Header("Shader Related")]
    public Material mat;

    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !isTrailActive && Time.time >= cooldownEndTime)
        {
            isTrailActive = true;
            StartCoroutine(ActivateTrail(activeTime));
            cooldownEndTime = Time.time + dashCooldownTime;
        }
    }

    IEnumerator ActivateTrail (float timeActive)
    {
        while (timeActive > 0)
        {
            timeActive -= mesRefreshRate;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = new SkinnedMeshRenderer[1];
                skinnedMeshRenderers[0] = GetComponentInChildren<SkinnedMeshRenderer>();
            
            for(int i=0; i<skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);
                

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf = gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);

                mf.mesh = mesh;
                mr.material = mat;

                Destroy(gObj, mesDestroyDelay);
            }

            yield return new WaitForSeconds(mesRefreshRate);
        }
        isTrailActive = false;
    }
}
