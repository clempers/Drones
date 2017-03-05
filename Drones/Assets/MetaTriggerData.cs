using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaTriggerData {
    public string name;

    public System.Type triggerType;

    public List<MetaActionData> allowedActions;

    public delegate void AddAction(Component trigger, Component Action);

    public AddAction addAction;

    public delegate List<Component> GetActions(Component trigger);

    public GetActions getActions;

    public delegate GameObject GetCreator(CommonUIElements ui);

    public GetCreator creator;

    public MetaTriggerData ChangeActions(List<MetaActionData> actions){
        return new MetaTriggerData(name, actions, triggerType, creator, addAction, getActions);
    }

    public MetaTriggerData(string name,  List<MetaActionData> allowedActions, System.Type triggerType, GetCreator creator, AddAction addAction, GetActions getActions)
    {
        this.name = name;
        this.triggerType = triggerType;
        this.allowedActions = allowedActions;
        this.creator = creator;
        this.addAction = addAction;
        this.getActions = getActions;
    }
}
