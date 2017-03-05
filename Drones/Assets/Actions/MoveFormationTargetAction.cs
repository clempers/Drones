using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Move Formation Target Action")]
public class MoveFormationTargetAction : TransformAction
{
    public FreeFormation freeFormation;

    static public MetaActionData metaData = new MetaActionData("Move Formation", typeof(MoveFormationTargetAction), (ui => ui.moveFormationTargetCreator));

    public override void OnTrigger(Transform input)
    {
        Formation formation = GetComponent<Formation>();
        if (!formation.transform.position.Equals(input.position))
        {
            Debug.Log("Moving Formation");
            Debug.Log(input.position);
            GetComponent<Formation>().relocate(freeFormation, input);
        }

    }
}
