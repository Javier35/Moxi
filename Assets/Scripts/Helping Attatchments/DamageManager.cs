using UnityEngine;
using System.Collections;

public abstract class DamageManager : Destroyable {

	public int health = 3;
	public int maxHealth = 5;
	public int damage = 1;
	public bool invincible = false;

	public Animator animator;

	void Awake(){
		animator = gameObject.GetComponent<Animator> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	public abstract void ReceiveDamage (int damage);
	
	public void StartDeathAnim(){
		animator.SetTrigger("Death");
	}

	public void Heal(int healing){
		health += healing;
		if (health > maxHealth)
			health = maxHealth;
	}

	Transform currentPos;


	public void ShakeSelf (float duration, float amount) {

		currentPos = gameObject.transform;
		StartCoroutine(cShake(duration, amount));
		Invoke ("ResetPos", duration);
	}

	public IEnumerator cShake (float duration, float amount) {
		float endTime = Time.time + duration;

		while (Time.time < endTime) {
			transform.localPosition = transform.position + Random.insideUnitSphere * amount;

			duration -= Time.deltaTime;

			yield return null;
		}
	}

	void ResetPos(){
		gameObject.transform.position.Set (currentPos.transform.position.x, currentPos.transform.position.y, currentPos.transform.position.z);
	}

	public void ShakeSprite(){
	}
}
