using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Drone/Pylon Drone")]

public class PylonDrone : Drone {
    public Vector3 bottom_offset;

    public Vector3 shield_direction;

    public void move_bottom_towards(float t, Vector3 location)
    {
        move_towards(t, location + -bottom_offset);
    }

    public void aim_at_pylon(Vector3 pylon)
    {
        transform.rotation = Quaternion.FromToRotation(shield_direction, pylon - transform.position);
    }


    private void LateUpdate()
    {
        fix_scale();
    }
}

