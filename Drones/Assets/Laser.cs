using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public delegate bool HitConditional(RaycastHit hit);

    public HitConditional shouldFire;

    public delegate void HitAction(RaycastHit hit);

    public HitAction onHit;

    public delegate float TargetRange(Vector3 target);

    public TargetRange laser_range;

    public Transform gunEnd;

    private LineRenderer laserLine;

    public WorldState state;

    private bool powered = false;

    private bool remain_on = false;

    public Color color;

    public Vector3 aim;

    public Vector3 hit_location;

    // Update is called once per frame
    public void Start()
    {
        laserLine = GetComponent<LineRenderer>();
        laserLine.enabled = false;
    }

    IEnumerator FireLaser()
    {
		while (remain_on || Time.deltaTime == 0f)
        {
			if (Time.deltaTime != 0f) {
				float range = 100f;
				if (laser_range != null)
					range = laser_range (aim);
				laserLine.startColor = color;
				laserLine.endColor = color;
				Vector3 rayOrigin = gunEnd.position;

				RaycastHit hit;

				laserLine.SetPosition (0, gunEnd.position);

				if (Physics.Raycast (rayOrigin, aim - rayOrigin, out hit, range)) {
					if (shouldFire == null || shouldFire (hit)) {
						state.LaserFired (hit, color);
						laserLine.SetPosition (1, hit.point);
						if (onHit != null)
							onHit (hit);
					} else
						laserLine.enabled = false;
				} else {
					laserLine.SetPosition (1, rayOrigin + (aim - rayOrigin).normalized * range);
				}
				remain_on = false;
			}
            yield return null;
        }
        powered = false;
        laserLine.enabled = false;
    }

    private void ActivateLaser()
    {
        powered = true;
        laserLine.enabled = true;
        StartCoroutine(FireLaser());
    }

    public void UpdateLaser(Color color, Vector3 aim)
    {
        this.color = color;
        this.aim = aim;
        remain_on = true;
        if (!powered)
            ActivateLaser();
    }
}
