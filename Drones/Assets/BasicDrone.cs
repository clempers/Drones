using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDrone : Drone {

    public float max_velocity = 1;

    public bool precision_target = true;

    private Vector3 target;

    public Player player;

    private LineRenderer laserLine;

    public Vector3 aim = new Vector3(1f, 1f, 1f);

    private float shotDuration = 1f;

    public override void move_towards_local(float t, Vector3 location)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, location, t * max_velocity);
    }

    public override void move_towards(float t, Vector3 location)
    {
        transform.position = Vector3.MoveTowards(transform.position, location, max_velocity);
    }

    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (precision_target)
        {
            RaycastHit hit = player.state.red_laser_point;
            String hit_owner = "";
            String hit_check_owner = "";

            laserLine.enabled = false;
            if (player.state.red_laser_hit)
            {
                if (hit.collider.GetComponent<Owner>() != null)
                    hit_owner = hit.collider.GetComponent<Owner>().owner;
                RaycastHit hit_check;
                if (Physics.Raycast(transform.position, hit.point - transform.position, out hit_check, 1.0f + (hit.point - transform.position).magnitude))
                {
                    if (hit_check.collider.GetComponent<Owner>() != null)
                        hit_check_owner = hit_check.collider.GetComponent<Owner>().owner;
                    if (hit_owner.Equals(hit_check_owner))
                    {
                        if (hit_check.collider.GetComponent<DamageTarget>() != null)
                            hit_check.collider.GetComponent<DamageTarget>().damage(1.0f, transform);
                        laserLine.enabled = true;
                        laserLine.SetPosition(0, transform.position);
                        laserLine.SetPosition(1, hit_check.point);
                        aim = hit.point;
                    }
                }
            }
        }
        else
        {
            RaycastHit hit = player.state.blue_laser_point;
            laserLine.enabled = false;
            String hit_owner = "";
            String hit_check_owner = "";
            if (player.state.blue_laser_set )
            {
                if (hit.collider.GetComponent<Owner>() != null)
                    hit_owner = hit.collider.GetComponent<Owner>().owner;
                RaycastHit hit_check;
                if (Physics.Raycast(transform.position, hit.collider.transform.position - transform.position, out hit_check, 1.0f + (hit.collider.transform.position - transform.position).magnitude))
                {
                    if (hit_check.collider.GetComponent<Owner>() != null)
                        hit_check_owner = hit_check.collider.GetComponent<Owner>().owner;
                    if (hit_owner.Equals(hit_check_owner))
                    {
                        if (hit_check.collider.GetComponent<DamageTarget>() != null)
                            hit_check.collider.GetComponent<DamageTarget>().damage(1.0f, transform);
                        laserLine.enabled = true;
                        laserLine.SetPosition(0, transform.position);
                        laserLine.SetPosition(1, hit_check.point);
                        aim = hit.point;
                    }
                }
            }
        }
    }
}
