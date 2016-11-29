using UnityEngine;
using System.Collections;

public class EnemyDamageManager : DamageManager {

	public float deathTime = 1f;
	private bool flickering = false;

	private Rigidbody2D rbody;
	private KnockbackModule knockbackModule;
	private LevelManager levelManager;
	private Animator anim;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		knockbackModule = GetComponent<KnockbackModule> ();
		levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		anim = gameObject.GetComponent<Animator> ();
	}

	void FixedUpdate(){
		if (health <= 0) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
				levelManager.GetComponent<LevelManager> ().enemiesToSpawn.Add (this.gameObject);
				StartDeathAnim ();
				KnockbackWhenDead ();
				Invoke ("DestroySelf", deathTime);
				foreach (Collider2D tempcollider in gameObject.GetComponents<Collider2D> ()) {
					//if(tempcollider.isTrigger == true)
					tempcollider.enabled = false;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {
			if (flickering == false) {
				flickering = true;
				Invoke ("Flicker", 0.1f);
			}
		}
	}

	public void Flicker(){
		spriteRenderer.enabled = !spriteRenderer.enabled;
		flickering = false;
	}

	private void KnockbackWhenDead(){
		//fall back code
		rbody.velocity = new Vector2 (0, rbody.velocity.y);
		Vector2 playerPos = GameObject.Find("Moxi").transform.position;
		if (playerPos.x <= transform.position.x) {
			//fall to the right
				StartCoroutine(knockbackModule.Knockback(1));
			
		} else {
			//fall to the left
				StartCoroutine(knockbackModule.Knockback(-1));

		}
	}

	void DestroySelf(){
		//Destroy (gameObject);
		gameObject.SetActive(false);
	}

	public void ResetVariables(){
		health = maxHealth;
		foreach (Collider2D tempcollider in gameObject.GetComponents<Collider2D> ()) {
			//if(tempcollider.isTrigger == true)
			tempcollider.enabled = true;
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.tag == "Player" && !animator.GetCurrentAnimatorStateInfo(0).IsName("Damage")) {
			var playersScript = col.gameObject.GetComponent<PlayerDamageManager> ();
			playersScript.PlayerReceiveDamage (damage);
		}
	}
}
