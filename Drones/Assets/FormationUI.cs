using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormationUI : MonoBehaviour {
    public MetaFormationData metaFormationData;

    public Dropdown trigger_selector;

    public Button button;

    public Transform target;

    public Formation realFormation;

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
        Debug.Log("FILL FIELDS");
        Trigger<Object>[] triggers = realFormation.GetComponents<Trigger<Object>>();

        foreach (var t in triggers)
            RebuildTrigger(t);
    } 

    public void RebuildTrigger<T>(Trigger<T> trigger)
    {
        Debug.Log("Rebuild Trigger");
        MetaTriggerData metaData = metaFormationData.allowedTriggers.Find(x => trigger.GetType() == x.triggerType);
        Debug.Log(metaData == null);
        if(metaData != null)
            MakeTrigger(metaData);
    }

    private void MakeNewTrigger()
    {
        string trigger_name = trigger_selector.options[trigger_selector.value].text;

        MetaTriggerData metaData = metaFormationData.allowedTriggers.Find(x => x.name.Equals(trigger_name));
        MakeTrigger(metaData);
    }

    private void MakeTrigger(MetaTriggerData metaData)
    {
        GameObject trigger = Instantiate(newTrigger);
        trigger.transform.SetParent(target);
        trigger.transform.localScale = new Vector3(1f, 1f, 1f);
        trigger.GetComponent<TriggerUI>().metaTriggerData = metaData;
    }
}
