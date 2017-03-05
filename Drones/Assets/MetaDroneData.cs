using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaDroneData {
    public string name;

    public List<MetaTriggerData> allowedTriggers;

    public System.Type droneType;

    public delegate GameObject GetCreator(CommonUIElements ui);

    public GetCreator creator;

    public MetaDroneData(string name, List<MetaTriggerData> allowedTriggers, System.Type droneType, GetCreator creator)
    {
        this.name = name;
        this.allowedTriggers = allowedTriggers;
        this.droneType = droneType;
        this.creator = creator;
    }
}
