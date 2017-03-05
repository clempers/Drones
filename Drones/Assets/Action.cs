using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Action<T> : MonoBehaviour {
    abstract public void OnTrigger(T input);
}
