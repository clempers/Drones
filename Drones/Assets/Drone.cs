using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Drone : MonoBehaviour
{
    abstract public void move_towards_local(float t, Vector3 location);

    abstract public void move_towards(float t, Vector3 location);
}
