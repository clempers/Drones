using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Trigger/Key Pressed Trigger")]
public class KeyPressedTrigger : BlankTrigger
{
    static public MetaTriggerData metaData = new MetaTriggerData("Key Pressed Trigger", new List<MetaActionData>() {  ResetLaserAction.metaData }, typeof(KeyPressedTrigger), (ui => ui.pendingCreator), ((trigger, action) => ((KeyPressedTrigger) trigger).actions.Add((BlankAction) action)), (c => ((KeyPressedTrigger)c).actions.ConvertAll(a => (Component)a)));

	public bool while_paused = false;

    public KeyCode keycode;
    public override object FireTrigger()
	{
		Debug.Log ("Checking button down "+keycode);
		if ((Time.deltaTime != 0f || while_paused) && Input.GetKeyDown (keycode)) {
			Debug.Log ("Button is down "+keycode);
			return "hi";
		}
        return null;
    }
}
