using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldingTarget : MonoBehaviour {
    public Transform attacker;

    public Transform defender;

    public ShieldStorageFormation home_formation;

    public FreeFormation freeFormation;

    public float shield_inertia = 2f;

    public float wasted_time = 0f;

    public float wasted_inertia = 0.5f;

    public float idle_time = 0f;
}
