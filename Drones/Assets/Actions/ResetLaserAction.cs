using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Reset Laser Action")]
public class ResetLaserAction : BlankAction {
    public Color laser_color;
    public WorldState lasers;
    static public MetaActionData metaData = new MetaActionData("Reset Laser Action", typeof(ResetLaserAction), (ui => ui.pendingCreator));

    public override void OnTrigger()
    {
        lasers.ResetLaser(laser_color);
    }
}
