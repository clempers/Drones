using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaFormationData {

    public string name;

    public List<MetaTriggerData> allowedTriggers;

    public System.Type formationType;

    public delegate GameObject GetCreator(CommonUIElements ui);

    public GetCreator creator;

    public MetaFormationData(string name, List<MetaTriggerData> allowedTriggers, System.Type formationType, GetCreator creator)
    {
        this.name = name;
        this.allowedTriggers = allowedTriggers;
        this.formationType = formationType;
        this.creator = creator;
    }
}
