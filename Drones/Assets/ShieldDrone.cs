using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDrone : Drone {
    public float max_velocity;

    void Start()
    {
        SetShieldPower(false);
    }

    public override void move_towards_local(float t, Vector3 location)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, location, t * max_velocity);
    }

    public override void move_towards(float t, Vector3 location)
    {
        transform.position = Vector3.MoveTowards(transform.position, location, max_velocity);
    }

    public void shieldFrom(Transform attacker, Transform victim)
    {
        transform.rotation = Quaternion.FromToRotation(new Vector3(0f,0f,1f), attacker.position - victim.position);
    }

    public void SetShieldPower(bool enabled)
    {
        Transform child;
        for(int i =0; i < transform.childCount; i++)
        {
            child = transform.GetChild(i);
            child.GetComponent<MeshRenderer>().enabled = enabled;
            child.GetComponent<Collider>().enabled = enabled;
        }
    }
}
