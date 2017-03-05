using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Action/Player Laser Action")]
public class PlayerLaserAction : BlankAction {
    public Laser laser;
    public WorldState worldstate;
    public Color color;

    public override void OnTrigger()
    {
		Debug.Log ("Fire player laser");
        laser.UpdateLaser(color, worldstate.playerAim);
    }
}
