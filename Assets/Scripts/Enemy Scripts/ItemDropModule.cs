using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropModule : MonoBehaviour {

	private GameObject item = null;
	[SerializeField] private GameObject moneyItem = null;
	[SerializeField] private GameObject healthItem = null;


	[SerializeField] private int healthDropChance = 10;
	[SerializeField] private int moneyDropChance = 25;

	// Use this for initialization
	void Start () {
		if (item != null) {
			var floatScript = GetComponent<Float>();
			if (floatScript != null)
				floatScript.enabled = false;
		}
	}
	
	public void DropItem(int knockbackDir){

		var healthRoll = Random.Range (1, 101);
		var moneyRoll = Random.Range (1, 101);

		if (healthRoll <= healthDropChance) {
			item = healthItem;
		} else if (moneyRoll <= moneyDropChance) {
			item = moneyItem;
		}

		if (item != null) {
			
			GameObject spawnedItem = (GameObject)Instantiate (item, this.transform.position, this.transform.rotation);
			spawnedItem.GetComponents<BoxCollider2D> ()[0].enabled = true;
			spawnedItem.GetComponent<Float> ().enabled = false;
			spawnedItem.GetComponent<Collectible> ().setRespawnable(false);
			var rbody = spawnedItem.GetComponent<Rigidbody2D> ();
			rbody.bodyType = RigidbodyType2D.Dynamic;
			rbody.AddForce (new Vector2 (35 * knockbackDir, 100));
		}

	}
}
