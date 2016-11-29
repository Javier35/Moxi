using UnityEngine;
using System.Collections;

public class DamageManager : MonoBehaviour {

	public int health = 3;
	public int maxHealth = 5;
	public int damage = 1;
	public bool invincible = false;

	public Animator animator;
	public SpriteRenderer spriteRenderer;
	private bool isDamaged = false;

	void Awake(){
		animator = gameObject.GetComponent<Animator> ();
		spriteRenderer = gameObject.GetComponent <SpriteRenderer> ();
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	//TODO: polish kill player
	//enable restarting in checkpoints, lives and such
	public void StartDeathAnim(){
		//Destroy (this.gameObject);
		animator.SetTrigger("Death");
	}

	public void ReceiveDamage(int damage){

		if (!isDamaged) {
			health -= damage;
			isDamaged = true;
			EnemyShake (0.16f, 0.05f);
		}

		if(health > 0)
			animator.SetTrigger("Damage");
		else if(health <= 0)
			animator.SetTrigger("Death");
	}

	public void Heal(int healing){
		health += healing;
		if (health > maxHealth)
			health = maxHealth;
	}

	public void ResetDamage(){
		isDamaged = false;
	}

	Transform currentPos;

	public void EnemyShake (float duration, float amount) {

		currentPos = gameObject.transform;
		StartCoroutine(cEnemyShake(duration, amount));
		Invoke ("ResetPos", duration);
	}

	public IEnumerator cEnemyShake (float duration, float amount) {
		float endTime = Time.time + duration;

		while (Time.time < endTime) {
			transform.localPosition = transform.position + Random.insideUnitSphere * amount;

			duration -= Time.deltaTime;

			yield return null;
		}

		//transform.localPosition = _originalPos;
	}

	void ResetPos(){
		gameObject.transform.position.Set (currentPos.transform.position.x, currentPos.transform.position.y, currentPos.transform.position.z);
	}
}
