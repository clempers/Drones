using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Formation/Orbit Formation")]
public class OrbitFormation : Formation {
    public float seconds_per_rotation=5f;

    public float seconds_ellapsed=0f;

    public float radius=1f;

    static public MetaFormationData metaData = new MetaFormationData("Orbit Formation", new List<MetaTriggerData>() { LaserTargetTrigger.metaData.ChangeActions(new List<MetaActionData>() { MoveFormationTargetAction.metaData }) }, typeof(OrbitFormation), (ui => ui.orbitFormationCreator));

    public override void add_drone(Drone d)
    {
        Debug.Log("Adding Drone to Orbit");
        if(transform.childCount == 0)
        {
            Vector3 projected = Vector3.ProjectOnPlane(d.transform.position- transform.position, new Vector3(0, 1, 0));
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
            int i = (int) ((angle * ((float) (child_count + 1))) / 360);
            d.transform.SetParent(transform, true);
            d.transform.SetSiblingIndex(i);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.DrawRay(transform.position, new Vector3(0f, 0f, 100f), Color.green);
        int child_count = transform.childCount;
        if (child_count == 0) return;
        float base_radians = (2 *  ((float) Math.PI) * seconds_ellapsed)/seconds_per_rotation;
        float inc_radians = (2 * ((float) Math.PI)) / child_count;

        for (int i = 0; i < child_count; i++)
        {
            Drone child = transform.GetChild(i).GetComponent<Drone>();
            Vector3 newDest = new Vector3(radius * (float) Math.Cos(base_radians + (i * inc_radians)), 0, radius * (float)Math.Sin(base_radians + (i * inc_radians)));
            child.move_towards_local(Time.deltaTime, newDest);
        }
        seconds_ellapsed += Time.deltaTime;
        if (seconds_ellapsed > seconds_per_rotation)
            seconds_ellapsed -= seconds_per_rotation;
	}
}
