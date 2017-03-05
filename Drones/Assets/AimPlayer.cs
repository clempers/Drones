using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimPlayer : MonoBehaviour {

    public Camera tpsCamera;
    public Transform gunHandle;
    public WorldState worldState;
    public float range;
	
	// Update is called once per frame
	void Update () {
        Vector3 cameraCenter = tpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        Vector3 rayOrigin = cameraCenter + Vector3.Project(gunHandle.position - cameraCenter, tpsCamera.transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, tpsCamera.transform.forward, out hit, range))
        {
            gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, hit.point - gunHandle.position);
            worldState.playerAim = hit.point;
        }
        else
        {
            gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, rayOrigin + (tpsCamera.transform.forward * range) - gunHandle.position);
            worldState.playerAim = rayOrigin + (tpsCamera.transform.forward * range);
        }
    }
}
