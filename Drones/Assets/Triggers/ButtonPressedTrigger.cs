using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Trigger/Button Pressed Trigger")]
public class ButtonPressedTrigger : BlankTrigger {
    static public MetaTriggerData metaData = new MetaTriggerData("Button Pressed Trigger", new List<MetaActionData>() { MoveFormationTargetAction.metaData }, typeof(KeyPressedTrigger), (ui => ui.pendingCreator), ((trigger,action) => ((ButtonPressedTrigger) trigger).actions.Add((BlankAction) action)), (c => ((ButtonPressedTrigger) c).actions.ConvertAll(a => (Component) a)));

	public bool while_paused = false;

    public string buttonName;
    public override object FireTrigger()
    {
		Debug.Log ("Checking button down "+buttonName);
		if ((Time.deltaTime != 0f || while_paused) && Input.GetButton (buttonName)) {
			Debug.Log ("Button is down "+buttonName);
			return "hi";
		}
        return null;
    }
}
