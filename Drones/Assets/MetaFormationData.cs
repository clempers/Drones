using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaFormationData {

    public string name;

    public List<MetaTriggerData> allowedTriggers;

    public System.Type formationType;

    public MetaFormationData(string name, List<MetaTriggerData> allowedTriggers, System.Type formationType)
    {
        this.name = name;
        this.allowedTriggers = allowedTriggers;
        this.formationType = formationType;
    }
}
