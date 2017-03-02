using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Move Formation Target Action")]
public class MoveFormationTargetAction : TransformAction
{
    public FreeFormation freeFormation;

    public override void OnTrigger(Transform input)
    {
        GetComponent<Formation>().relocate(freeFormation, input);
    }
}
