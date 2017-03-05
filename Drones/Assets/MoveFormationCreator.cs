using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFormationCreator : ComponentCreator {
    public MoveFormationTargetAction action;

    private void Start()
    {
        transform.localScale = new Vector3(1, 1, 1);
        action = FromComponent(transform.parent.GetComponent<ActionUI>().realAction);
        action.freeFormation = GetComponentInParent<ActionUI>().uiElements.freeFormation;
    }

    public void FillFields(MoveFormationTargetAction action)
    {
    }

    public MoveFormationTargetAction FromComponent(Component c)
    {
        return (MoveFormationTargetAction) c;
    }
}
