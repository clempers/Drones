using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Laser Point Action")]
public class LaserPointAction : VectorAction
{
    public override void OnTrigger(Vector3? input)
    {
        GetComponent<BasicDrone>().LaserPoint(input ?? Vector3.zero);
    }
}
