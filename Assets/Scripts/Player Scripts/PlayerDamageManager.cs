using UnityEngine;
using System.Collections;

public class PlayerDamageManager : DamageManager {

	private LevelManager levelManager;
	private PlatformerCharacter2D player;
	private float invincibilityTime = 1.5f;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager> ();
		player = GetComponent<PlatformerCharacter2D> ();
	}
	
	void LateUpdate () {
		GameObject.Find ("Canvas").GetComponentInChildren<Animator> ().SetInteger ("Health", health);
	}

	public void BecomeInvincible(){
		invincible = true;
		CameraShake.Shake (0.2f, 0.024f);
		StartCoroutine (spriteEffector.Flicker (invincibilityTime));
		Invoke ("ResetInvincibility", invincibilityTime);
	}

	public override void ReceiveDamage(int damage){
		
		if(!animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage") && !invincible){
			health -= damage;
			BecomeInvincible ();
			animator.SetTrigger("Damage");
			if (health <= 0) {
				animator.SetTrigger("Death");
			}
			Knockback ();
		}
	}

	public void SetInvincibility(){
		invincible = true;
	}

	public void ResetInvincibility(){
		invincible = false;
	}

	private void Knockback(){
		var rbody = GetComponent<Rigidbody2D> ();
		rbody.velocity = new Vector2 (0, 0);
		player.WasDamaged ();
	}
		
	override public void DestroySelf(string cause){
		levelManager.fader.BeginFade (1);
		levelManager.RespawnPlayer ();
	}

	//move it a little bit just to wake up the physics engine
	public void Nudge(){
		this.transform.position = this.transform.position + (new Vector3 (0.0001f, 0f, 0f));
	}

	override public void Respawn(){
		DestroySelf ("");
	}
}
