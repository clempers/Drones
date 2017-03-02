using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public Camera tpsCamera;

    public Transform gunEnd;

    private LineRenderer laserLine;

    public Transform gunHandle;

    public Player player;

    private float shotDuration = 0.5f;

    // Update is called once per frame
    public void Start()
    {
        laserLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
	public void FixedUpdate ()
    {
        player.state.red_laser_hit = false;
        player.state.blue_laser_hit = false;
        if (Input.GetButton("Fire1"))
        {
            laserLine.enabled = true;
            laserLine.startColor = Color.red;
            laserLine.endColor = Color.red;
            Vector3 rayOrigin = tpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition(0, gunHandle.position);

            if (Physics.Raycast(rayOrigin, tpsCamera.transform.forward, out hit, 100))
            {
                player.state.red_laser_point = hit;
                player.state.red_laser_set = true;
                player.state.red_laser_hit = true;
                gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, hit.point - gunHandle.position);
                laserLine.SetPosition(1, hit.point);
            }
            else
            {
                gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, rayOrigin + (tpsCamera.transform.forward * 100) - gunHandle.position);
                laserLine.SetPosition(1, rayOrigin + (tpsCamera.transform.forward * 100));
            }
        }
        else if (Input.GetButton("Fire2"))
        {
            laserLine.enabled = true;
            laserLine.startColor = Color.blue;
            laserLine.endColor = Color.blue;
            Vector3 rayOrigin = tpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition(0, gunHandle.position);

            if (Physics.Raycast(rayOrigin, tpsCamera.transform.forward, out hit, 100))
            {
                player.state.blue_laser_point = hit;
                player.state.blue_laser_hit = true;
                player.state.blue_laser_set = true;
                gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, hit.point - gunHandle.position);
                laserLine.SetPosition(1, hit.point);
                player.attackFormation.transform.SetParent(hit.transform, true);
                player.attackFormation.transform.localRotation = Quaternion.identity;
                player.attackFormation.relocate(player.freeFormation, hit.transform.position);
            }
            else
            {
                gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, rayOrigin + (tpsCamera.transform.forward * 100) - gunHandle.position);
                laserLine.SetPosition(1, rayOrigin + (tpsCamera.transform.forward * 100));
            }
        }
        else
        {
            laserLine.enabled = false;
        }
    }

}
