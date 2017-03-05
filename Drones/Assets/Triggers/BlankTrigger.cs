using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BlankTrigger : Trigger<object>
{
	public new List<BlankAction> actions = new List<BlankAction>();


	public override void CheckTrigger()
	{
		object obj = FireTrigger();
		if(obj != null)
		{
			Debug.Log ("Fire Trigger "+actions.Count);
			actions.ForEach(a => a.OnTrigger(obj));
		}
	}
}
