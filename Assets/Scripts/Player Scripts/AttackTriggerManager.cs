using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackTriggerManager : MonoBehaviour {

	private BoxCollider2D attackBox1;
	private CapsuleCollider2D attackBox2;
	private List<int> objectsHit = new List<int>();
	private Animator anim;
	[SerializeField] int damageStrength = 1;



	void Awake(){
		attackBox1 = gameObject.GetComponent<BoxCollider2D> ();
		attackBox2 = gameObject.GetComponent<CapsuleCollider2D> ();
		anim = gameObject.GetComponentInParent<Animator> ();
	}

	void Update (){
		if(!CheckAttackBoxesForActive()){
			ClearHittedList();
		}
	}

	void OnTriggerEnter2D(Collider2D col){

		var hitHandler = col.gameObject.GetComponent<HitHandler> ();
		if (hitHandler != null) {

			int hittedObjectId = col.gameObject.GetInstanceID ();

			if (!objectsHit.Contains (hittedObjectId)) {

				if (anim.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) {
					damageStrength = 1;
				} else if (anim.GetCurrentAnimatorStateInfo (0).IsName ("SecondAttack")) {
					damageStrength = 2;
				}else if(anim.GetCurrentAnimatorStateInfo (0).IsName ("ThirdAttack")) {
					damageStrength = 7;
				}

				hitHandler.HitEvent (damageStrength);
				objectsHit.Add (hittedObjectId);
			}
		}

	}

	private void ShowHitEffect(){
		GetComponentInChildren<Animator> ().SetTrigger ("Hit1");
	}

	public void ClearHittedList (){
		objectsHit.Clear ();
	}

	private bool CheckAttackBoxesForActive(){
		if(attackBox1.enabled && attackBox2.enabled){
			return true;
		}
		return false;
	}

}
