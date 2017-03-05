using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class TransformTrigger : Trigger<Transform> {
	public new List<TransformAction> actions = new List<TransformAction>();

	public override void CheckTrigger()
	{
		Transform obj = FireTrigger();
		if(obj != null)
		{
			Debug.Log ("Fire Trigger "+actions.Count);
			actions.ForEach(a => a.OnTrigger(obj));
		}
	}
}
