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

    public bool FireLaser(Color color)
    {
        laserLine.enabled = true;
        laserLine.startColor = color;
        laserLine.endColor = color;
        Vector3 rayOrigin = tpsCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        RaycastHit hit;

        laserLine.SetPosition(0, gunHandle.position);

        if (Physics.Raycast(rayOrigin, tpsCamera.transform.forward, out hit, 100))
        {
            player.GetComponent<WorldState>().LaserFired(hit, color);
            gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, hit.point - gunHandle.position);
            laserLine.SetPosition(1, hit.point);
            return true;
        }
        else
        {
            gunHandle.rotation = Quaternion.FromToRotation(Vector3.forward, rayOrigin + (tpsCamera.transform.forward * 100) - gunHandle.position);
            laserLine.SetPosition(1, rayOrigin + (tpsCamera.transform.forward * 100));
            return false;
        }
    }

    // Update is called once per frame
	public void FixedUpdate ()
    {
        player.GetComponent<WorldState>().ResetLasers();
        if (Input.GetButton("Fire1"))
        {
            FireLaser(Color.red);
        }
        else if (Input.GetButton("Fire2"))
        {
            if (FireLaser(Color.blue)) {
                RaycastHit hit = player.GetComponent<WorldState>().GetLaserState(Color.blue).hit;
                player.attackFormation.transform.SetParent(hit.transform, true);
                player.attackFormation.transform.localRotation = Quaternion.identity;
                player.attackFormation.relocate(player.freeFormation, hit.transform.position);
            }
        }
        else if (Input.GetKey(KeyCode.Alpha1))
        {
            FireLaser(Color.green);
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            FireLaser(Color.yellow);
        }
        else
        {
            laserLine.enabled = false;
        }
    }

}
