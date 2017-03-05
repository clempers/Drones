using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserTargetTriggerCreator : ComponentCreator
{
    public ColorCreator laser_color;

    public LaserTargetTrigger trigger;

    private void Start()
    {
        uiElements = transform.parent.GetComponent<TriggerUI>().uiElements;
        transform.localScale = new Vector3(1, 1, 1);
        trigger = FromComponent(transform.parent.GetComponent<TriggerUI>().realTrigger);
        laser_color.uiElements = uiElements;
        laser_color.saveCurrentValue = (c => trigger.laser_color = c);
        FillFields(trigger);
    }

    public void FillFields(LaserTargetTrigger laser)
    {
        laser_color.SetOptions();
        laser_color.SetValue(laser.laser_color);
        laser.worldState = GetComponentInParent<TriggerUI>().state;
    }

    public LaserTargetTrigger FromComponent(Component c)
    {
        return (LaserTargetTrigger) c;
    }
}
