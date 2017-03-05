using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaActionData {
    public string name;

    public System.Type actionType;

    public delegate GameObject GetCreator(CommonUIElements ui);

    public GetCreator creator;

    public MetaActionData(string name, System.Type actionType, GetCreator creator)
    {
        this.name = name;
        this.actionType = actionType;
        this.creator = creator;
    }
}
