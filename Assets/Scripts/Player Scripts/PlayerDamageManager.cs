using UnityEngine;
using System.Collections;

public class PlayerDamageManager : DamageManager {

	private bool flickering = false;

	private LevelManager levelManager;

	private PlatformerCharacter2D player;
	private KnockbackModule knockbackModule;
	public BoxCollider2D attackBox;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();

		player = GetComponent<PlatformerCharacter2D> ();
		knockbackModule = GetComponent<KnockbackModule> ();
	}
	
	// Update is called once per frame
	void Update () {

		GameObject.Find ("Canvas").GetComponentInChildren<Animator> ().SetInteger ("Health", health);

		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack") && 
			!animator.GetCurrentAnimatorStateInfo (0).IsName ("SecondAttack"))
				DisableAttackBox ();
		
		FlickerWhenDamaged ();
	}

	public void BecomeInvincible(float invincibilityTime){
		invincible = true;
		CameraShake.Shake (0.2f, 0.024f);
		Invoke ("ResetValues", invincibilityTime);
	}

	private void FlickerWhenDamaged(){
		if (invincible == true) {
			if (flickering == false) {
				flickering = true;
				Invoke ("Flicker", 0.1f);
			}
		} else
			spriteRenderer.enabled = true;
	}

	private void Flicker(){
		spriteRenderer.enabled = !spriteRenderer.enabled;
		flickering = false;
	}

	void ResetValues(){
		invincible = false;
	}

	public void PlayerReceiveDamage(int damage){
		if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
			if (invincible == false) {
				health -= damage;
				animator.SetTrigger("Damage");

				if (health <= 0) {
					animator.SetTrigger("Death");
					Debug.Log ("triggered");
				}

				Knockback ();
				invincible = true;
				CameraShake.Shake (0.2f, 0.024f);
				Invoke ("ResetValues", 2);
			}
	}

	private void Knockback(){
		var rbody = GetComponent<Rigidbody2D> ();
		rbody.velocity = new Vector2 (0, 0);
		if (player.m_FacingRight) {
			knockbackModule.OtherKnockback (-1, 5, 5);
		}
			
		else {
			knockbackModule.OtherKnockback (1, 5, 5);
		}
}

	void DestroySelf(){
		//Destroy (transform.parent.gameObject);
	}

	public void EnableAttackBox(){
		attackBox.enabled = true;
	}
	public void DisableAttackBox(){
		attackBox.enabled = false;
		GetComponentInChildren<AttackTriggerManager> ().ResetEnemyDamage ();
	}

	public void Respawn(){
		levelManager.RespawnPlayer ();
	}
}
