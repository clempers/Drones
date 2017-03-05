using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationUI : ComponentUI {
    public MetaFormationData metaFormationData;

    public Dropdown trigger_selector;

    public Button button;

    public Transform target;

    public Component realFormation;

    public Text title;

    public GameObject newTrigger;

    private void Start()
    {
        if(realFormation != null)
            FillInFields();
        title.text = metaFormationData.name;
        trigger_selector.ClearOptions();
        trigger_selector.AddOptions(metaFormationData.allowedTriggers.ConvertAll<string>(meta => meta.name));
        button.onClick.AddListener(MakeNewTrigger);
    }

    private void FillInFields()
    {
        List<Component> triggers = new List<Component>();
        metaFormationData.allowedTriggers.ForEach(md => triggers.AddRange(realFormation.GetComponents(md.triggerType)));

        foreach (var t in triggers)
            RebuildTrigger(t);
    } 

    public void RebuildTrigger(Component trigger)
    {
        MetaTriggerData metaData = metaFormationData.allowedTriggers.Find(x => trigger.GetType() == x.triggerType);
        if (metaData != null)
        {
            GameObject newTrigger = MakeTrigger(metaData);
            newTrigger.GetComponent<TriggerUI>().realTrigger = trigger;
            if (metaData.creator(uiElements) != null)
            {
                GameObject triggerCreator = Instantiate(metaData.creator(uiElements));
                triggerCreator.transform.SetParent(newTrigger.transform);
            }
        }
    }

    private void MakeNewTrigger()
    {
        string trigger_name = trigger_selector.options[trigger_selector.value].text;

        MetaTriggerData metaData = metaFormationData.allowedTriggers.Find(x => x.name.Equals(trigger_name));
        realFormation.gameObject.AddComponent(metaData.triggerType);
        Component[] triggers = realFormation.GetComponents(metaData.triggerType);
        Component newTrigger = triggers[triggers.Length-1];
        GameObject triggerUI = MakeTrigger(metaData);
        triggerUI.GetComponent<TriggerUI>().realTrigger = newTrigger;
        Instantiate(metaData.creator(uiElements)).transform.SetParent(triggerUI.transform);
    }

    private GameObject MakeTrigger(MetaTriggerData metaData)
    {
        GameObject trigger = Instantiate(newTrigger);
        trigger.transform.SetParent(target);
        trigger.transform.localScale = new Vector3(1f, 1f, 1f);
        trigger.GetComponent<TriggerUI>().metaTriggerData = metaData;
        return trigger;
    }
}
