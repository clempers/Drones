using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDroneCreator : ComponentCreator {
    public BasicDrone basicDrone;

    public ColorCreator laser_color;

    public TextCreator nameCreator;

    public GameObject basicDroneTemplate;

    private void Start()
    {
        uiElements = transform.parent.GetComponent<DroneUI>().uiElements;
        transform.localScale = new Vector3(1, 1, 1);
        basicDrone = FromComponent(transform.parent.GetComponent<DroneUI>().realDrone);
        laser_color.uiElements = uiElements;
        laser_color.saveCurrentValue = (c => basicDrone.laser_color = c);
        nameCreator.saveCurrentValue = (c => basicDrone.name = c);
        FillFields(basicDrone);
    }

    public void FillFields(BasicDrone c)
    {
        laser_color.SetOptions();
        laser_color.SetValue(basicDrone.laser_color);
        nameCreator.SetValue(c.name);
        basicDrone.state = GetComponentInParent<DroneUI>().state;
    }

    public BasicDrone FromComponent(Component c)
    {
        return (BasicDrone) c;
    }
}
