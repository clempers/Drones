using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class VectorTrigger : Trigger<Vector3?> {
	public new List<VectorAction> actions = new List<VectorAction>();

	public override void CheckTrigger()
	{
		Vector3? obj = FireTrigger();
		if(obj != null)
		{
			Debug.Log ("Fire Trigger "+actions.Count);
			actions.ForEach(a => a.OnTrigger(obj ?? Vector3.zero));
		}
	}
}
