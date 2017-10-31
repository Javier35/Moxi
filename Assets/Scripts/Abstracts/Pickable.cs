using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

	[SerializeField] bool pickable = false;
	public GameObject projectileForm;
	private Transform grabPoint;
	Destroyable destroyable;

	void Awake(){
		destroyable = this.gameObject.GetComponent<Destroyable> ();
		grabPoint = projectileForm.transform.Find ("GrabPoint");

		if (destroyable == null)
			destroyable = this.gameObject.GetComponentInParent<Destroyable> ();
	}

	public bool IsPickable(){
		return pickable;
	}

	public void SetPickable(bool pickable){
		this.pickable = pickable;
	}

	public GameObject GetProjectile(){

		return projectileForm;
	}

	public virtual void DestroySelf(){
		destroyable.DestroySelf ();
	}

	public Vector3 GetGrabPoint(){

		if (grabPoint == null) {
			return Vector3.zero;
		}
		return grabPoint.position;
	}
}

