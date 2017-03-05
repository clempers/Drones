using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroneUI : ComponentUI {
    public MetaDroneData metaDroneData;

    public Dropdown trigger_selector;

    public Button button;

    public Transform target;

    public Component realDrone;

    public Text title;

    public GameObject newTrigger;

    private void Start()
    {
        if (realDrone != null)
            FillInFields();
        title.text = metaDroneData.name;
        trigger_selector.ClearOptions();
        trigger_selector.AddOptions(metaDroneData.allowedTriggers.ConvertAll<string>(meta => meta.name));
        button.onClick.AddListener(MakeNewTrigger);
    }

    private void FillInFields()
    {
        List<Component> triggers = new List<Component>();
        metaDroneData.allowedTriggers.ForEach(md => triggers.AddRange(realDrone.GetComponents(md.triggerType)));

        foreach (var t in triggers)
            RebuildTrigger(t);
    }

    public void RebuildTrigger(Component trigger)
    {
        MetaTriggerData metaData = metaDroneData.allowedTriggers.Find(x => trigger.GetType() == x.triggerType);
        if (metaData != null)
        {
            GameObject newTrigger = MakeTrigger(metaData);
            newTrigger.GetComponent<TriggerUI>().realTrigger = trigger;
            if (metaData.creator(uiElements) != null)
            {
                GameObject triggerCreator = Instantiate(metaData.creator(uiElements));
                triggerCreator.transform.parent = newTrigger.transform;
            }
        }
    }

    private void MakeNewTrigger()
    {
        string trigger_name = trigger_selector.options[trigger_selector.value].text;

        MetaTriggerData metaData = metaDroneData.allowedTriggers.Find(x => x.name.Equals(trigger_name));
        Component newTrigger = realDrone.gameObject.AddComponent(metaData.triggerType);
        GameObject triggerUI = MakeTrigger(metaData);
        triggerUI.GetComponent<TriggerUI>().realTrigger = newTrigger;
        Instantiate(metaData.creator(uiElements)).transform.parent = triggerUI.transform;
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
