using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTimerManager : MonoBehaviour {

	[HideInInspector]public float startTimer;
	[HideInInspector]public float pressTime;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.X)) {
			startTimer = Time.time;
		}else if (Input.GetKey (KeyCode.X)) {
			pressTime = Time.time - startTimer;
		}else{
			pressTime = 0;
		}
	}
}
