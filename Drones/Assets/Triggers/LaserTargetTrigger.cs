using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Trigger/Laser Target Trigger")]
public class LaserTargetTrigger : TransformTrigger {
    // Update is called once per frame
    public WorldState worldState;

    public Color laser_color;

    public override void CheckTrigger()
    {
        LaserState ls = worldState.GetLaserState(laser_color);

        if (ls != null)
        {
            actions.ForEach(x => x.OnTrigger(ls.hit.collider.transform));
        }
    }
}
