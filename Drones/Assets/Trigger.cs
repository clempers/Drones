using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Trigger<T> : MonoBehaviour {
    public List<Action<T>> actions = new List<Action<T>>();

    private void Awake()
    {
        //actions = new List<Action<T>>();
    }

    private void Update()
    {
        CheckTrigger();
    }

    abstract public T FireTrigger();

	abstract public void CheckTrigger ();
}
