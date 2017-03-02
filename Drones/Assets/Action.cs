using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Action<Input> : MonoBehaviour {
    abstract public void OnTrigger(Input input);
}
