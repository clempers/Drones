using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Drone/Basic Drone")]
public class BasicDrone : Drone {
    public bool precision_target = true;

    static public MetaDroneData metaData = new MetaDroneData("Basic Drone", new List<MetaTriggerData>() { LaserTargetTrigger.metaData }, typeof(BasicDrone), (ui => ui.basicDroneCreator));

    private Vector3 target;

    public WorldState state;

    public Laser laser;

    public Color laser_color;

    public Vector3 aim = new Vector3(1f, 1f, 1f);

    private float shotDuration = 1f;

    // Use this for initialization
    void Start () {
        laser = GetComponent<Laser>();
        laser.state = state;
        laser.shouldFire = DifferentOwner;
        laser.laser_range = (aim => 1f + Vector3.Distance(transform.position, aim));
        laser.onHit = (hit =>
        {
            DamageTarget dt = hit.collider.GetComponent<DamageTarget>();
            if (dt != null)
                dt.damage(1, transform);
        });
    }

    public bool DifferentOwner(RaycastHit hit)
    {
        String hit_owner = "";
        Owner drone_owner = GetComponent<Owner>();
        if (hit.collider.GetComponent<Owner>() != null)
            hit_owner = hit.collider.GetComponent<Owner>().owner;
        return drone_owner == null || !drone_owner.Equals(hit_owner);
    }

    public void LaserTarget(Transform target)
    {
        laser.UpdateLaser(laser_color, target.position);
    }

    public void LaserPoint(Vector3 target)
    {
        laser.UpdateLaser(laser_color, target);
    }
}

