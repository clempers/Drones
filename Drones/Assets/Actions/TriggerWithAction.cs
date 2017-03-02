using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TriggerWithAction<Input, InputAction> : BlankAction where InputAction : Action<Input> {

    public Input input;

    public InputAction action;

    public override void OnTrigger()
    {
        action.OnTrigger(input);
    }
}
