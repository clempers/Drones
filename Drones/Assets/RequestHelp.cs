using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequestHelp : MonoBehaviour, DamageTarget
{
    public ShieldStorageFormation shieldFormation;

    public void damage(float damage, Transform source)
    {
        Debug.Log("Damage");
        if (shieldFormation != null)
            shieldFormation.requestHelp(source, transform);
    }
}
