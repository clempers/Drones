using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Formation/Free Formation")]
public class FreeFormation : Formation {

    static public MetaFormationData metaData = new MetaFormationData("Free Formation", new List<MetaTriggerData>() { LaserTargetTrigger.metaData }, typeof(FreeFormation));

    public override void add_drone(Drone d)
    {
        if (d.GetComponent<DestinationFormation>() != null)
        {
            d.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
            DestinationFormation dest = d.GetComponent<DestinationFormation>();
            UnityEngine.AI.NavMeshAgent agent = d.GetComponent<UnityEngine.AI.NavMeshAgent>();
            d.transform.SetParent(transform, true);
            agent.SetDestination(dest.destination.transform.position);
        }
    }

    public void move_drone(Drone d, Formation destination)
    {
        if (d.GetComponent<DestinationFormation>() == null)
            d.gameObject.AddComponent<DestinationFormation>();
        DestinationFormation destinationFormation = d.GetComponent<DestinationFormation>();
        destinationFormation.destination = destination;
        add_drone(d);
    }

    public new void remove_drone(Drone d)
    {
        d.GetComponent<UnityEngine.AI.NavMeshAgent>().Stop();
        d.transform.parent = null;
    }

	// Update is called once per frame
	void Update () {
        int children_count = transform.childCount;

        for (int i = 0; i < children_count; i++)
        {
            Transform child = transform.GetChild(i);
            DestinationFormation dest = child.GetComponent<DestinationFormation>();
            UnityEngine.AI.NavMeshAgent agent = child.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (Vector3.Distance(child.position, dest.destination.transform.position) <= dest.transfer_distance)
            {
                agent.Stop();
                agent.enabled = false;
                dest.destination.add_drone(child.GetComponent<Drone>());
                i--;
                children_count--;
            }
            else if (Vector3.Distance(dest.destination.transform.position, agent.destination) > dest.transfer_distance || Vector3.Distance(dest.destination.transform.position, agent.destination) > Vector3.Distance(child.position, agent.destination)) {
                agent.SetDestination(dest.destination.transform.position);
            }
        }
	}
}
