using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaTriggerData {
    public string name;

    public System.Type triggerType;

    public List<MetaActionData> allowedActions;

    public MetaTriggerData(string name,  List<MetaActionData> allowedActions, System.Type triggerType)
    {
        this.name = name;
        this.triggerType = triggerType;
        this.allowedActions = allowedActions;
    }
}
