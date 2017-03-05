using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitFormationCreator : ComponentCreator
{
    public NumberCreator radiusCreator;

    public NumberCreator periodCreator;

    public TextCreator nameCreator;

    public OrbitFormation orbitFormation;

    private void Start()
    {
        radiusCreator.saveCurrentValue = (r => orbitFormation.radius = r);
        periodCreator.saveCurrentValue = (r => { orbitFormation.seconds_ellapsed *= r / orbitFormation.seconds_per_rotation; orbitFormation.seconds_per_rotation = r; });
        nameCreator.saveCurrentValue = (s => orbitFormation.name = s);

        transform.localScale = new Vector3(1f, 1f, 1f);
        orbitFormation = FromComponent(transform.parent.GetComponent<FormationUI>().realFormation);
        FillFields(orbitFormation);
    }

    public void FillFields(OrbitFormation c)
    {
        radiusCreator.SetValue(c.radius);

        periodCreator.SetValue(c.seconds_per_rotation);

        nameCreator.SetValue(c.name);
    }

    public OrbitFormation FromComponent(Component c)
    {
        return (OrbitFormation) c;
    }
}
