using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldTarget : MonoBehaviour, DamageTarget
{
    public void damage(float damage, Transform source)
    {
        if(source.Equals(transform.parent.GetComponentInChildren<ShieldingTarget>().attacker))
        transform.parent.GetComponentInChildren<ShieldingTarget>().idle_time = 0f;
    }
}
