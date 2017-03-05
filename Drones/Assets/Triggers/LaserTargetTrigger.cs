using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Trigger/Laser Target Trigger")]
public class LaserTargetTrigger : TransformTrigger {
    // Update is called once per frame
    public WorldState worldState;

    public Color laser_color;

    static public MetaTriggerData metaData = new MetaTriggerData("Laser Target", new List<MetaActionData>() { MoveFormationTargetAction.metaData },  typeof(LaserTargetTrigger), (ui => ui.laserTargetCreator), ((trigger, action) => ((LaserTargetTrigger)trigger).actions.Add((TransformAction)action)), (c => ((LaserTargetTrigger)c).actions.ConvertAll(a => (Component)a)));

    private void Start()
    {
    }

    public override Transform FireTrigger()
    {
        if (worldState == null || laser_color == null)
            return null;
        LaserState ls = worldState.GetLaserState(laser_color);

		if (Time.deltaTime != 0f && ls != null)
        {
            return ls.hit.collider.transform;
        }
        return null;
    }
}
