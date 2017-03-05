using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ValueCreator<T> : MonoBehaviour {
    public CommonUIElements uiElements;

    public delegate void ValueSaver(T v);

    public ValueSaver saveCurrentValue;

    abstract public T GetValue();

    abstract public void SetValue(T v);
}
