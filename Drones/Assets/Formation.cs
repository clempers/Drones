﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Formation : MonoBehaviour {
    public Transform follow;

    abstract public void add_drone(Drone d);

    public void remove_drone(Drone d)
    {
        d.transform.parent = null;
    }

    public void relocate(FreeFormation freeFormation, Transform newFollow)
    {
        Drone[] drones = new Drone[transform.childCount];

        int i = 0;

        Drone d;

        while (d = transform.GetComponentInChildren<Drone>())
        {
            drones[i] = d;
            d.transform.parent = null;
            DestinationFormation destionation = d.GetComponent<DestinationFormation>();
            destionation.destination = this;
            remove_drone(d);
            i++;
        }
        follow = newFollow;
        transform.position = newFollow.position;

        for (int j = 0; j < i; j++)
        {
            freeFormation.add_drone(drones[j]);
        }
    }

    private void LateUpdate()
    {
        transform.position = follow.position;
    }
}
