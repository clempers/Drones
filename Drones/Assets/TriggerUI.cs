using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerUI : MonoBehaviour
{
    public MetaTriggerData metaTriggerData;

    public Dropdown action_selector;

    public Button button;

    public Transform target;

    public Text title;

    public GameObject newAction;

    private void Start()
    {
        title.text = metaTriggerData.name;
        action_selector.ClearOptions();
        action_selector.AddOptions(metaTriggerData.allowedActions.ConvertAll<string>(meta => meta.name));
        button.onClick.AddListener(MakeAction);
    }

    private void MakeAction()
    {
        string action_name = action_selector.options[action_selector.value].text;

        MetaActionData metaData = metaTriggerData.allowedActions.Find(x => x.name.Equals(action_name));
        GameObject action = Instantiate(newAction);
        action.transform.SetParent(target);
        action.transform.localScale = new Vector3(1f, 1f, 1f);
        action.GetComponent<ActionUI>().metaActionData = metaData;
    }
}
