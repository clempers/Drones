using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldWallFormation : Formation {
    public PylonDrone pylon1;
    public Vector3 pylon1_location;

    public PylonDrone pylon2;
    public Vector3 pylon2_location;

    public Shield shield;

    public Vector3 shieldNormal;

    public Player player;


    public override void add_drone(Drone d)
    {
        PylonDrone pylon = d.GetComponent<PylonDrone>();
        if (pylon == null) return;
        if(pylon1 == null)
        {
            pylon1 = pylon;
            pylon.transform.SetParent(transform, true);
        } else if(pylon2 == null)
        {
            pylon2 = pylon;
            pylon.transform.SetParent(transform, true);
        }
    }

    public new void remove_drone(Drone d)
    {
        d.transform.parent = null;

        if (pylon1 == d.GetComponent<PylonDrone>())
            pylon1 = null;
        else if (pylon2 == d.GetComponent<PylonDrone>())
            pylon2 = null;
    }
    
	void Update ()
    {
        LaserState laser;

        laser = player.GetComponent<WorldState>().GetLaserState(Color.green);
        if (laser != null && laser.is_active)
            pylon1_location = laser.hit.point;

        laser = player.GetComponent<WorldState>().GetLaserState(Color.yellow);
        if (laser != null && laser.is_active)
            pylon2_location = laser.hit.point;

        if (pylon1 != null)
            pylon1.move_bottom_towards(Time.deltaTime, pylon1_location);
        if (pylon2 != null)
            pylon2.move_bottom_towards(Time.deltaTime, pylon2_location);

        if(pylon1 != null && pylon2 != null  && shield != null)
        {
            pylon1.aim_at_pylon(pylon2.transform.position);
            pylon2.aim_at_pylon(pylon1.transform.position);
            shield.transform.position = Vector3.Lerp(pylon1.transform.position, pylon2.transform.position, 0.5f);
            shield.transform.rotation = Quaternion.FromToRotation(shieldNormal, Vector3.Cross(new Vector3(0f, 1f, 0f), pylon1.transform.position - pylon2.transform.position));
            shield.transform.localScale = new Vector3(Vector3.Distance(pylon1.transform.position,pylon2.transform.position), shield.transform.localScale.y, shield.transform.localScale.z);
        }
    }
}
