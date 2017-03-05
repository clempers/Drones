using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectShieldFormation : Formation
{
    public float radius=2.0f;

    public float duplicate_radius = 0.1f;
    
    public override void add_drone(Drone d)
    {
        if (d.GetComponent<ShieldDrone>() != null && d.GetComponent<ShieldingTarget>() != null)
        {
            d.transform.SetParent(transform);
            d.GetComponent<ShieldDrone>().SetShieldPower(true);
            d.GetComponent<ShieldingTarget>().idle_time = 0f;
        }
    }

    public void Update()
    {
        ShieldDrone d;
        Transform child;
        ShieldingTarget target;

        int child_count = transform.childCount;

        for(int i = 0; i < child_count; i++)
        {
            target = transform.GetChild(i).GetComponent<ShieldingTarget>();
            if (target.wasted_time >= 0)
                target.wasted_time -= Time.deltaTime / 4;
            for (int j = i +1; j < child_count; j++)
            {
                if(Vector3.Distance(transform.GetChild(i).position, transform.GetChild(j).position) < duplicate_radius)
                {
                    ShieldDrone drone = transform.GetChild(j).GetComponent<ShieldDrone>();
                    target = drone.GetComponent<ShieldingTarget>();
                    target.wasted_time += Time.deltaTime;
                    if (target.wasted_time > target.wasted_inertia)
                    {
                        remove_drone(drone);
                        j--;
                        child_count--;
                        drone.GetComponent<ShieldingTarget>().home_formation.reassignShield(drone);
                    }
                }
            }
        }

        for(int i =0; i < child_count; i++)
        {
            child = transform.GetChild(i);
            d = child.GetComponent<ShieldDrone>();
            target = child.GetComponent<ShieldingTarget>();
            target.idle_time += Time.deltaTime;
            Vector3 aim = target.attacker.GetComponent<Laser>().aim - transform.position;
            Vector3 projected = Vector3.ProjectOnPlane(aim, target.attacker.position - transform.position - aim);
            Debug.DrawRay(transform.position, aim, Color.black);
            Debug.DrawRay(transform.position + aim, projected, Color.red);
            Debug.DrawRay(transform.position+aim, target.attacker.position - transform.position-aim, Color.blue);
            if (projected.magnitude > radius)
                d.move_towards_local(Time.deltaTime, (target.attacker.position - transform.position).normalized * radius);
            else
            {
                float scalar = Mathf.Sqrt(radius * radius - (projected.magnitude * projected.magnitude));
                Vector3 scaled_vector = (target.attacker.position - transform.position - aim).normalized * scalar;
                d.move_towards_local(Time.deltaTime, scaled_vector + projected);
            }
            d.shieldFrom(target.attacker, transform);
            if(target.idle_time > target.shield_inertia)
            {
                remove_drone(d);
                target.home_formation.reassignShield(d);
                i--;
                child_count--;
            }
        }
    }


}
