using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public List<LaserState> lasers;

    void Awake()
    {
        lasers = new List<LaserState>();
    }

    public void ResetLasers()
    {
        foreach (LaserState ls in lasers)
        {
            ls.is_active = false;
        }
    }

    public void LaserFired(RaycastHit hit, Color color)
    {
        LaserState laser = lasers.Find(ls => ls.laser_color.Equals(color));
        if (laser != null)
        {
            laser.is_active = true;
            laser.hit = hit;
        } else {
            laser = new LaserState();
            laser.hit = hit;
            laser.is_active = true;
            laser.laser_color = color;
            lasers.Add(laser);
        }
    }

    public LaserState GetLaserState(Color color)
    {
        return lasers.Find(ls => ls.laser_color.Equals(color));
    }

    public void ResetLaser(Color color)
    {
        LaserState laser = lasers.Find(ls => ls.laser_color.Equals(color));
        if(laser != null)
            lasers.Remove(laser);
    }
}
