using UnityEngine;
using System.Collections;

public class EnemyDamageManager : DamageManager {

	public float deathTime = 1f;

	private ItemDropModule itemDropper;
	private Rigidbody2D rbody;
	private KnockbackModule knockbackModule;
	private LevelManager levelManager;
	private bool isDamaged = false;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		knockbackModule = GetComponent<KnockbackModule> ();
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		itemDropper = gameObject.GetComponent<ItemDropModule> ();
	}

	void FixedUpdate(){
		if (health <= 0 && !animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
			Die ();
		}
	}

	public void ReceiveDamage(int damage){

		if (!isDamaged) {
			health -= damage;
			isDamaged = true;
			EnemyShake (0.16f, 0.05f);

			if (health > 0)
				animator.SetTrigger ("Damage");
			else{
				animator.SetTrigger("Death");
				StartCoroutine (Flicker(deathTime));
			}
		}
	}

	public void ResetDamage(){
		isDamaged = false;
	}


	public void Die(){
	
		levelManager.GetComponent<LevelManager> ().enemiesToSpawn.Add (this.gameObject);
		StartDeathAnim ();
		var knockbackDir = getKnockbackDir ();
		itemDropper.DropItem (knockbackDir);
		KnockbackWhenDead (knockbackDir);
		Invoke ("DeactivateSelf", deathTime);
		foreach (Collider2D tempcollider in gameObject.GetComponents<Collider2D> ()) {
			tempcollider.enabled = false;
		}
	}

	private int getKnockbackDir(){
		Vector2 playerPos = GameObject.Find("Moxi").transform.position;
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

	void DeactivateSelf(){
		gameObject.SetActive(false);
	}

	public void ResetVariables(){
		health = maxHealth;
		foreach (Collider2D tempcollider in gameObject.GetComponents<Collider2D> ()) {
			tempcollider.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("Damage")) {
			var playersScript = col.gameObject.GetComponent<PlayerDamageManager> ();
			playersScript.PlayerReceiveDamage (damage);
		}
	}

	override public void DestroySelf(){
		//play death particle effect?
		levelManager.GetComponent<LevelManager> ().enemiesToSpawn.Add (this.gameObject);
		gameObject.SetActive(false);
	}
}
