using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Trigger<Output> : MonoBehaviour {
    public List<Action<Input>> actions;

    private void Update()
    {
        CheckTrigger();
    }

    abstract public void CheckTrigger();
}
