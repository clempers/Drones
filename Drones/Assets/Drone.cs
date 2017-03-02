using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Drone : MonoBehaviour
{
    public float max_velocity = 4f;

    public void move_towards_local(float t, Vector3 location)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, location, t * max_velocity);
    }

    public void move_towards(float t, Vector3 location)
    {
        transform.position = Vector3.MoveTowards(transform.position, location, t * max_velocity);
    }
}
