using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TransformTrigger : Trigger<Transform> {
    public new List<TransformAction> actions;
}
