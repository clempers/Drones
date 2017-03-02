using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Drone/Shield Drone")]
public class ShieldDrone : Drone {
    public Shield shield;

    void Start()
    {
        SetShieldPower(false);
    }

    public void shieldFrom(Transform attacker, Transform victim)
    {
        transform.rotation = Quaternion.FromToRotation(new Vector3(0f,0f,1f), attacker.position - victim.position);
    }

    public void SetShieldPower(bool enabled)
    {
        shield.powered = enabled;
    }

    private void LateUpdate()
    {
        fix_scale();
    }
}
