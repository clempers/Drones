using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BlankAction : Action<bool> {

    public override void OnTrigger(bool input)
    {
        OnTrigger();
    }

    abstract public void OnTrigger();
}
