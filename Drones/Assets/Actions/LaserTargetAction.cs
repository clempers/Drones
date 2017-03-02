using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Laser Target Action")]
public class LaserTargetAction : TransformAction
{
    public override void OnTrigger(Transform input)
    {
        GetComponent<BasicDrone>().LaserTarget(input);
    }
}
