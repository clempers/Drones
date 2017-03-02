using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoulderFormation : Formation { 
    public double base_offset;
    public double inc_offset;

    public override void add_drone(Drone d)
    {
        d.gameObject.transform.SetParent(GetComponent<Transform>(), false);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int child_count = transform.childCount;
        if (child_count == 0) return;

        for (int i = 0; i < child_count; i++)
        {
            Drone child = transform.GetChild(i).GetComponent<Drone>();
            int direction;
            if (i % 2 == 0)
                direction = 1;
            else
                direction = -1;
            Vector3 newDest = new Vector3((float) (base_offset + inc_offset * i / 2)*direction, 0, 0);
            child.move_towards_local(Time.deltaTime, newDest);
        }
    }
}
