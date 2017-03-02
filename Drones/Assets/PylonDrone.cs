using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PylonDrone : Drone {
    public Vector3 bottom_offset;

    public void move_bottom_towards(float t, Vector3 location)
    {
        move_towards(t, location + -bottom_offset);
    }
}
