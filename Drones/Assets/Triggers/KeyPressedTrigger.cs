using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Trigger/Key Pressed Trigger")]
public class KeyPressedTrigger : BlankTrigger
{
    public KeyCode keycode;
    public override void CheckTrigger()
    {
        if (Input.GetKeyDown(keycode))
            actions.ForEach(x => x.OnTrigger());
    }
}
