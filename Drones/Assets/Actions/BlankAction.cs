using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BlankAction : Action<object> {

    public override void OnTrigger(object input)
    {
        OnTrigger();
    }

    abstract public void OnTrigger();
}
