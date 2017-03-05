using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldState : MonoBehaviour
{
    public List<LaserState> lasers;
    public List<LaserState> laser_changes;
    public List<Color> removed_lasers;
    public Vector3 playerAim;

    void Awake()
    {
        lasers = new List<LaserState>();
        laser_changes = new List<LaserState>();
        removed_lasers = new List<Color>();
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
        LaserState laser = laser_changes.Find(ls => ls.laser_color.Equals(color));
        if (laser != null)
        {
            laser.is_active = true;
            laser.hit = hit;
        } else {
            laser = new LaserState();
            laser.hit = hit;
            laser.is_active = true;
            laser.laser_color = color;
            laser_changes.Add(laser);
        }
    }

    public LaserState GetLaserState(Color color)
    {
        return lasers.Find(ls => ls.laser_color.Equals(color));
    }

    public void RemoveLaser(Color color)
    {
        removed_lasers.Add(color);
    }

    public void ResetLaser(Color color)
    {
        LaserState laser = lasers.Find(ls => ls.laser_color.Equals(color));
        if(laser != null)
            lasers.Remove(laser);
    }

    private void LateUpdate()
    {
        lasers.ForEach(ls => ls.is_active = false);
        lasers = lasers.ConvertAll(ls =>
        {
            LaserState new_laser = laser_changes.Find(lsc => ls.laser_color.Equals(lsc.laser_color));
            if (new_laser == null)
                return ls;
            else
            {
                laser_changes.Remove(new_laser);
                return new_laser;
            }
        });
        lasers.AddRange(laser_changes);
        laser_changes.Clear();
        lasers.RemoveAll(ls => removed_lasers.Contains(ls.laser_color));
        removed_lasers.Clear();
    }
}
