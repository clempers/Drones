using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Trigger/Key Trigger")]
public class KeyTrigger : BlankTrigger
{
	static public MetaTriggerData metaData = new MetaTriggerData("Key Trigger", new List<MetaActionData>() {  ResetLaserAction.metaData }, typeof(KeyTrigger), (ui => ui.pendingCreator), ((trigger, action) => ((KeyTrigger) trigger).actions.Add((BlankAction) action)),(c => ((KeyTrigger)c).actions.ConvertAll(a => (Component)a)));

	public bool while_paused = false;

	public KeyCode keycode;

	public override object FireTrigger()
	{
		if ((Time.deltaTime != 0f || while_paused) && Input.GetKey(keycode))
			return "hi";
		return null;
	}
}
