using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Move Pylon Action")]
public class MovePylonAction : VectorAction
{
    public bool pylonOne = true;
    public override void OnTrigger(Vector3? input)
    {
        if (pylonOne)
            GetComponent<ShieldWallFormation>().pylon1_location = input ?? Vector3.zero;
        else
            GetComponent<ShieldWallFormation>().pylon2_location = input ?? Vector3.zero;
    }
}
