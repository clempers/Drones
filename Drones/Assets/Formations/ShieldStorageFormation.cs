using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Formation/Shield Storage Formation")]
public class ShieldStorageFormation : Formation
{
    public float seconds_per_rotation;

    public float seconds_ellapsed;

    public float radius;

    public DirectShieldFormation direct_shield;

    public List<DirectShieldFormation> active_shields;

    public FreeFormation freeFormation;

    private List<Transform> attackers;
    private List<Transform> defenders;

    private float shield_inertia=2.0f;

    private List<Transform> pending_attackers;
    private List<Transform> pending_defenders;
    private List<float> pending_idle;

    public void Awake()
    {
        attackers = new List<Transform>();
        defenders = new List<Transform>();

        pending_attackers = new List<Transform>();
        pending_defenders = new List<Transform>();
        pending_idle = new List<float>();
    }

    private void add_pending(Transform attacker, Transform defender)
    {
        pending_attackers.Add(attacker);
        pending_defenders.Add(defender);
        pending_idle.Add(0f);
    }

    private void add_attacker(Transform attacker, Transform defender)
    {
        attackers.Add(attacker);
        defenders.Add(defender);
    }

    public override void add_drone(Drone d)
    {
        if (pending_defenders.Count !=0 )
            helpPending(d.transform);
        else if (transform.childCount == 0)
        {
            Vector3 projected = Vector3.ProjectOnPlane(d.transform.position - transform.position, new Vector3(0, 1, 0));
            float angle = Vector3.Angle(projected, new Vector3(1f, 0f, 0f));
            if (Math.Sin((2 * ((float)Math.PI) * angle) / 360f) * projected.z < 0)
                seconds_ellapsed = seconds_per_rotation - (seconds_per_rotation * angle / 360f);
            else
                seconds_ellapsed = seconds_per_rotation * angle / 360f;
            d.transform.SetParent(transform, true);
        }
        else
        {
            Vector3 projected = Vector3.ProjectOnPlane(d.transform.position - transform.position, new Vector3(0, 1, 0));
            float angle = Vector3.Angle(projected, new Vector3(1f, 0f, 0f));
            if (Math.Sin((2 * ((float)Math.PI) * angle) / 360f) * projected.z < 0)
                angle = 360 - angle;
            angle -= 360f * seconds_ellapsed / seconds_per_rotation;
            int child_count = transform.childCount;
            int i = (int)((angle * ((float)(child_count + 1))) / 360);
            d.transform.SetParent(transform, true);
            d.transform.SetSiblingIndex(i);
        }
    }

    public void helpPending(Transform drone)
    {
        sendHelp(drone.transform, pending_attackers[0], pending_defenders[0]);
        pending_attackers.RemoveAt(0);
        pending_defenders.RemoveAt(0);
        pending_idle.RemoveAt(0);
    }

    public void cancelHelp(Transform attacker, Transform victim)
    {
        int idx = -1;
        for (int i = 0; i < attackers.Count; i++)
        {
            if (attacker.Equals(attackers[i]) && victim.Equals(defenders[i]))
                idx = i;
        }
        if (idx == -1)
            return;
        attackers.RemoveAt(idx);
        defenders.RemoveAt(idx);
    }

    public void cancelPending(int idx)
    {
        cancelHelp(pending_attackers[idx], pending_defenders[idx]);
        pending_attackers.RemoveAt(idx);
        pending_defenders.RemoveAt(idx);
        pending_idle.RemoveAt(idx);
    }

    private void sendHelp(Transform drone, Transform attacker, Transform victim)
    {
        ShieldDrone d = drone.GetComponent<ShieldDrone>();
        if (d.GetComponent<ShieldingTarget>() == null)
            d.gameObject.AddComponent<ShieldingTarget>();
        ShieldingTarget target = d.GetComponent<ShieldingTarget>();
        target.attacker = attacker;
        target.defender = victim;
        target.freeFormation = freeFormation;
        target.home_formation = this;
        remove_drone(d);
        DirectShieldFormation directShield = active_shields.Find(f => f.follow.Equals(victim));
        if (directShield == null)
        {
            directShield = Instantiate(direct_shield, victim.position, Quaternion.identity);
            active_shields.Add(directShield);
            directShield.follow = victim;
        }
        freeFormation.move_drone(d, directShield);
    }

    public void requestHelp(Transform attacker, Transform victim)
    {
        bool is_duplicate = false;
        for(int i = 0; i < attackers.Count; i++)
        {
            if (attacker.Equals(attackers[i]) && victim.Equals(defenders[i]))
                is_duplicate = true;
        }

        if (is_duplicate) return;

        add_attacker(attacker, victim);
        if (transform.childCount == 0)
            add_pending(attacker, victim);
        else
            sendHelp(transform.GetChild(0), attacker, victim);
    }

    public void reassignShield(ShieldDrone drone)
    {
        ShieldingTarget target = drone.GetComponent<ShieldingTarget>();
        drone.SetShieldPower(false);
        cancelHelp(target.attacker, target.defender);
        if(pending_attackers.Count == 0)
            target.freeFormation.move_drone(drone, target.home_formation);
        else
        {
            helpPending(drone.transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i =0; i < pending_attackers.Count; i++)
        {
            pending_idle[i] += Time.deltaTime;
            if(pending_idle[i] > shield_inertia)
            {
                cancelPending(i);
                i--;
            }
        }

        Debug.DrawRay(transform.position, new Vector3(0f, 0f, 100f), Color.green);
        int child_count = transform.childCount;
        if (child_count == 0) return;
        float base_radians = (2 * ((float)Math.PI) * seconds_ellapsed) / seconds_per_rotation;
        float inc_radians = (2 * ((float)Math.PI)) / child_count;

        for (int i = 0; i < child_count; i++)
        {
            Drone child = transform.GetChild(i).GetComponent<Drone>();
            float rads = base_radians + (i * inc_radians);
            Vector3 newDest = new Vector3(radius * (float)Math.Cos(rads), 0, radius * (float)Math.Sin(rads));
            child.move_towards_local(Time.deltaTime, newDest);
            child.transform.localRotation = Quaternion.Euler(0, 90 + 180 * -rads / ((float)Math.PI), 0);
        }
        seconds_ellapsed += Time.deltaTime;
        if (seconds_ellapsed > seconds_per_rotation)
            seconds_ellapsed -= seconds_per_rotation;
    }
}
