using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Action : MonoBehaviour {

    abstract public void OnTriggerEnter(Collider other);
}
