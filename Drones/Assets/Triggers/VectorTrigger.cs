using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class VectorTrigger : Trigger<Vector3> {
    public new List<VectorAction> actions;
}
