using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Drone : MonoBehaviour
{
    public float max_velocity = 4f;

    public Vector3 proper_scale;

    private void Awake()
    {
        proper_scale = transform.lossyScale;
    }

    public void fix_scale()
    {
        //transform.localScale = new Vector3(transform.localScale.x * proper_scale.x / transform.lossyScale.x, transform.localScale.y * proper_scale.y / transform.lossyScale.y, transform.localScale.z * proper_scale.x / transform.lossyScale.z);
    }

    public void move_towards_local(float t, Vector3 location)
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, location, t * max_velocity);
    }

    public void move_towards(float t, Vector3 location)
    {
        transform.position = Vector3.MoveTowards(transform.position, location, t * max_velocity);
    }
}
