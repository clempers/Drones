using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerUI : ComponentUI
{
    public MetaTriggerData metaTriggerData;

    public Dropdown action_selector;

    public Button button;

    public Transform target;

    public Text title;

    public GameObject newAction;

    public Component realTrigger;

    private void Start()
    {
        SetState();
        if (realTrigger != null)
            FillInFields();
        title.text = metaTriggerData.name;
        action_selector.ClearOptions();
        action_selector.AddOptions(metaTriggerData.allowedActions.ConvertAll<string>(meta => meta.name));
        button.onClick.AddListener(MakeNewAction);
    }

    private void FillInFields()
    {
        List<Component> actions = metaTriggerData.getActions(realTrigger);

        foreach (var t in actions)
            RebuildAction(t);
    }

    public void RebuildAction(Component trigger)
    {
        MetaActionData metaData = metaTriggerData.allowedActions.Find(x => trigger.GetType() == x.actionType);
        if (metaData != null) {
            GameObject newAction = MakeAction(metaData);
            newAction.GetComponent<ActionUI>().realAction = trigger;
            if (metaData.creator(uiElements) != null)
            {
                GameObject actionCreator = Instantiate(metaData.creator(uiElements));
                actionCreator.transform.parent = newAction.transform;
            }
        }
    }

    private void MakeNewAction()
    {
        string trigger_name = action_selector.options[action_selector.value].text;

        MetaActionData metaData = metaTriggerData.allowedActions.Find(x => x.name.Equals(trigger_name));
        if (metaData == null)
            return;
        Component action = realTrigger.gameObject.AddComponent(metaData.actionType);
        metaTriggerData.addAction(realTrigger, action);
        GameObject newAction = MakeAction(metaData);
        newAction.GetComponent<ActionUI>().realAction = action;
        if (metaData.creator(uiElements) != null)
        {
            GameObject actionCreator = Instantiate(metaData.creator(uiElements));
            actionCreator.transform.parent = newAction.transform;
        }
    }

    private GameObject MakeAction(MetaActionData metaData)
    {
        GameObject action = Instantiate(newAction);
        action.transform.SetParent(target);
        action.transform.localScale = new Vector3(1f, 1f, 1f);
        action.GetComponent<ActionUI>().metaActionData = metaData;
        return action;
    }
}
