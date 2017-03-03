using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Move Formation Target Action")]
public class MoveFormationTargetAction : TransformAction
{
    public FreeFormation freeFormation;

    static public MetaActionData metaData = new MetaActionData("Move Formation", typeof(MoveFormationTargetAction));

    public override void OnTrigger(Transform input)
    {
        GetComponent<Formation>().relocate(freeFormation, input);
    }
}
