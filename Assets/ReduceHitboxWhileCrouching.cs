using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceHitboxWhileCrouching : MonoBehaviour {

	public float reductionOffset;

	public GameObject physicalHitboxesObject;
	public GameObject triggetHitboxesObject;

	private Collider2D[] physicalBoxes;
	private Collider2D[] triggerBoxes;

	void Start(){
		physicalBoxes = physicalHitboxesObject.GetComponents<Collider2D> ();
		triggerBoxes = triggetHitboxesObject.GetComponents<Collider2D> ();
	}

	void Update () {
		
	}

	private void ReduceHitboxesIfCrouching(){
	}

	private void RestoreHitboxValues(){
	}
}
