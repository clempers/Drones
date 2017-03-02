using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldStorageFormation : Formation
{
    public float seconds_per_rotation;

    public float seconds_ellapsed;

    public float radius;

    public DirectShieldFormation directShield;

    public FreeFormation freeFormation;

    private Transform[] attackers;
    private Transform[] defenders;
    private int attackers_size;
    private int num_attackers;

    private float shield_inertia=2.0f;

    private Transform[] pending_attackers;
    private Transform[] pending_defenders;
    private float[] pending_idle;
    private int pending_size;
    private int num_pending;

    public void Awake()
    {
        attackers = new Transform[8];
        defenders = new Transform[8];
        attackers_size = 8;
        num_attackers = 0;

        pending_attackers = new Transform[8];
        pending_defenders = new Transform[8];
        pending_idle = new float[8];
        pending_size = 8;
        num_pending = 0;
    }

    private void add_pending(Transform attacker, Transform victim)
    {
        if (1 + num_pending >= pending_size)
        {
            Transform[] new_pending_attackers = new Transform[pending_size * 2];
            Transform[] new_pending_defenders = new Transform[pending_size * 2];
            float[] new_pending_idle = new float[pending_size * 2];
            for (int i = 0; i < pending_size; i++)
            {
                new_pending_attackers[i] = pending_attackers[i];
                new_pending_defenders[i] = pending_defenders[i];
                new_pending_idle[i] = pending_idle[i];
            }
            pending_attackers = new_pending_attackers;
            pending_defenders = new_pending_defenders;
            pending_idle = new_pending_idle;
            pending_size *= 2;
        }
        pending_attackers[num_pending] = attacker;
        pending_defenders[num_pending] = victim;
        pending_idle[num_pending] = 0f;
        num_pending++;
    }

    private void add_attacker(Transform attacker, Transform victim)
    {
        if (1 + num_attackers >= attackers_size)
        {
            Transform[] new_attackers = new Transform[attackers_size * 2];
            Transform[] new_defenders = new Transform[attackers_size * 2];
            for (int i = 0; i < attackers_size; i++)
            {
                new_attackers[i] = attackers[i];
                new_defenders[i] = defenders[i];
            }
            attackers = new_attackers;
            defenders = new_defenders;
            attackers_size *= 2;
        }
        attackers[num_attackers] = attacker;
        defenders[num_attackers] = victim;
        num_attackers++;
    }

    public override void add_drone(Drone d)
    {
        if (transform.childCount == 0)
        {
            Vector3 projected = Vector3.ProjectOnPlane(d.transform.position - transform.position, new Vector3(0, 1, 0));
            float angle = Vector3.Angle(projected, new Vector3(1f, 0f, 0f));
            Debug.Log(Math.Cos((2 * ((float)Math.PI) * angle) / 360f));
            if (Math.Sin((2 * ((float)Math.PI) * angle) / 360f) * projected.z < 0)
                seconds_ellapsed = seconds_per_rotation - (seconds_per_rotation * angle / 360f);
            else
                seconds_ellapsed = seconds_per_rotation * angle / 360f;
            Debug.Log(seconds_ellapsed);
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
        if (num_pending != 0)
            helpPending(d.transform);
    }

    public void helpPending(Transform drone)
    {
        num_pending--;
        sendHelp(drone.transform, pending_attackers[num_pending], pending_defenders[num_pending]);
    }

    public void cancelHelp(Transform attacker, Transform victim)
    {
        int idx = -1;
        for (int i = 0; i < num_attackers; i++)
        {
            if (attacker.Equals(attackers[i]) && victim.Equals(defenders[i]))
                idx = i;
        }
        if (idx == -1)
            return;
        for (int i = idx + 1; i < num_attackers; i++)
        {
            attackers[i - 1] = attackers[i];
            defenders[i - 1] = defenders[i];
        }
        num_attackers--;
    }

    public void cancelPending(int idx)
    {
        cancelHelp(pending_attackers[idx], pending_defenders[idx]);
        for (int i = idx + 1; i < num_pending; i++)
        {
            pending_attackers[i - 1] = pending_attackers[i];
            pending_defenders[i - 1] = pending_defenders[i];
            pending_idle[i - 1] = pending_idle[i];
        }
        num_pending--;
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
        if (victim.GetComponent<DirectShieldFormation>() == null)
            Instantiate(directShield, victim.position, Quaternion.identity).transform.SetParent(victim);
        freeFormation.move_drone(d, victim.GetComponentInChildren<DirectShieldFormation>());
    }

    public void requestHelp(Transform attacker, Transform victim)
    {
        bool isNew = true;
        for(int i = 0; i < num_attackers; i++)
        {
            if (attacker.Equals(attackers[i]) && victim.Equals(defenders[i]))
                isNew = false;
        }

        if (!isNew) return;

        add_attacker(attacker, victim);
        if (transform.childCount == 0)
        {
            add_pending(attacker, victim);
            return;
        }
        sendHelp(transform.GetChild(0), attacker, victim);
    }

    public void reassignShield(ShieldDrone drone)
    {
        ShieldingTarget target = drone.GetComponent<ShieldingTarget>();
        drone.SetShieldPower(false);
        cancelHelp(target.attacker, target.defender);
        Debug.Log(num_pending);
        if(num_pending == 0)
            target.freeFormation.move_drone(drone, target.home_formation);
        else
        {
            helpPending(drone.transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i =0; i < num_pending; i++)
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
