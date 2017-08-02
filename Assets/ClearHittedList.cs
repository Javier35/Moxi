using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearHittedList : MonoBehaviour {

	private AttackTriggerManager attackTriggerManager;

	void Start(){
		attackTriggerManager = GetComponentInChildren<AttackTriggerManager> ();
	}

	public void ClearHitted (){
		attackTriggerManager.ClearHittedList ();
	}
}
