using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Trigger/Key Pressed Trigger")]
public class KeyPressedTrigger : BlankTrigger
{
    static public MetaTriggerData metaData = new MetaTriggerData("Key Pressed Trigger", new List<MetaActionData>() { MoveFormationTargetAction.metaData }, typeof(KeyPressedTrigger));

    public KeyCode keycode;
    public override void CheckTrigger()
    {
        if (Input.GetKeyDown(keycode))
            actions.ForEach(x => x.OnTrigger());
    }
}
