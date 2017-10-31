using UnityEngine;
using System.Collections;

public class EnemyDamageManager : DamageManager {

	public float deathTime = 1f;

	private ItemDropModule itemDropper;
	private Rigidbody2D rbody;
	private KnockbackModule knockbackModule;
	private LevelManager levelManager;
	private GameObject player;
	private CollidersManager [] allHitboxManagers;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Moxi");
		rbody = GetComponent<Rigidbody2D> ();
		knockbackModule = GetComponent<KnockbackModule> ();
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		itemDropper = gameObject.GetComponent<ItemDropModule> ();
		allHitboxManagers = GetComponentsInChildren<CollidersManager> ();
	}

	void FixedUpdate(){
		if (health <= 0 && !animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
			DestroySelf ();
		}
	}

	private void DisableAllHitboxes (){
		foreach(CollidersManager hitboxManager in allHitboxManagers){
			hitboxManager.DisableAllColliders ();
		}
	}

	private void EnableAllHitboxes (){
		foreach(CollidersManager hitboxManager in allHitboxManagers){
			hitboxManager.EnableAllColliders ();
		}
	}

	public override void ReceiveDamage(int damage){

		health -= damage;
		if (health > 0) {
			spriteEffector.FlashRedOnce ();
			animator.SetTrigger ("Damage");
		}else{
			animator.SetTrigger("Death");
			StartCoroutine (spriteEffector.Flicker(deathTime));
		}
		
	}

	private int GetKnockbackDir(){
		Vector2 playerPos = player.transform.position;
		if (playerPos.x <= transform.position.x) {
			//fall to the right
			return 1;

		} else {
			//fall to the left
			return -1;
		}
	}

	private void KnockbackWhenDead(int knockbackDir){
		rbody.velocity = new Vector2 (0, rbody.velocity.y);
		knockbackModule.Knockback(knockbackDir);
	}



	public void ResetVariables(){
		health = maxHealth;
		foreach (Collider2D tempcollider in gameObject.GetComponents<Collider2D> ()) {
			tempcollider.enabled = true;
		}
		EnableAllHitboxes ();
	}

	override public void DestroySelf(){

		if (!spawned) {
			levelManager.GetComponent<LevelManager> ().respawnables.Add (this.gameObject);
			StartDeathAnim ();
			var knockbackDir = GetKnockbackDir ();
			itemDropper.DropItem (knockbackDir);
			KnockbackWhenDead (knockbackDir);
			Invoke ("Deactivate", deathTime);
			foreach (Collider2D tempcollider in gameObject.GetComponents<Collider2D> ()) {
				tempcollider.enabled = false;
			}
			DisableAllHitboxes ();
		} else {
			Destroy (gameObject);
		}
	}

	public void InstantDestroy(){
		if (!spawned) {
			levelManager.GetComponent<LevelManager> ().respawnables.Add (this.gameObject);
			Deactivate ();
		} else {
			Destroy (gameObject);
		}
	}

	void Deactivate(){
		gameObject.SetActive(false);
	}

	override public void Respawn(){
		
		gameObject.GetComponent<EnemyDamageManager> ().ResetVariables ();

		gameObject.SetActive(true);
		gameObject.GetComponent<EnemyDamageManager> ().spriteEffector.stopFlicker ();
		//Debug.Log (transform.position);
	}
}
