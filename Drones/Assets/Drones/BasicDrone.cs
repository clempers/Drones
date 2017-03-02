using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Drone/Basic Drone")]
public class BasicDrone : Drone {
    public bool precision_target = true;

    private Vector3 target;

    public Player player;

    private LineRenderer laserLine;

    public Vector3 aim = new Vector3(1f, 1f, 1f);

    private float shotDuration = 1f;

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        laserLine.enabled = false;
        if (!precision_target)
        {
        }
        else
        {
        }
    }

    public void LaserTarget(Transform target)
    {
        String hit_check_owner = "";
        Owner drone_owner = GetComponent<Owner>();
        RaycastHit hit_check;
        if (Physics.Raycast(transform.position, target.position - transform.position, out hit_check, 1.0f + (target.position - transform.position).magnitude))
        {
            if (hit_check.collider.GetComponent<Owner>() != null)
                hit_check_owner = hit_check.collider.GetComponent<Owner>().owner;
            if (drone_owner == null || !drone_owner.owner.Equals(hit_check_owner))
            {
                if (hit_check.collider.GetComponent<DamageTarget>() != null)
                    hit_check.collider.GetComponent<DamageTarget>().damage(1.0f, transform);
                laserLine.enabled = true;
                laserLine.SetPosition(0, transform.position);
                laserLine.SetPosition(1, hit_check.point);
                aim = target.position;
            }
        }
    }

    public void LaserPoint(Vector3 target)
    {
        String hit_check_owner = "";
        Owner drone_owner = GetComponent<Owner>();
        RaycastHit hit_check;
        if (Physics.Raycast(transform.position, target - transform.position, out hit_check, 1.0f + (target - transform.position).magnitude))
        {
            if (hit_check.collider.GetComponent<Owner>() != null)
                hit_check_owner = hit_check.collider.GetComponent<Owner>().owner;
            if (drone_owner == null || !drone_owner.owner.Equals(hit_check_owner))
            {
                if (hit_check.collider.GetComponent<DamageTarget>() != null)
                    hit_check.collider.GetComponent<DamageTarget>().damage(1.0f, transform);
                laserLine.enabled = true;
                laserLine.SetPosition(0, transform.position);
                laserLine.SetPosition(1, hit_check.point);
                aim = target;
            }
        }
    }
}

