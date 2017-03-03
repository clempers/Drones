using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaActionData {
    public string name;

    public System.Type actionType;

    public MetaActionData(string name, System.Type actionType)
    {
        this.name = name;
        this.actionType = actionType;
    }
}
