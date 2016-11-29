using UnityEngine;
using System.Collections;

public class KnockbackModule : MonoBehaviour {

	//private Rigidbody2D rbody;
	public float knockDur = 1.2f; //maximum duration of knockback in x and y axis
	public float knockbackX = 1f;
	public float knockbackY = 1f;

	public float knockDurX = 2F; //maximum duration of knockback in x axis
	public float knockDurY = 1.2F; //maximum duration of knockback in y axis

	// Use this for initialization
	void Start () {
	}

	public void OtherKnockback(float knockbackDir, float xPower, float yPower){
		Rigidbody2D rbody = GetComponent<Rigidbody2D> ();
		rbody.velocity = new Vector2 (xPower * knockbackDir, yPower);
	}

	public IEnumerator Knockback(float knockbackDir){
		float timer = 0;
		while (knockDur > timer) {
			timer += Time.deltaTime;
			Vector2 dir = new Vector2 (knockbackDir, 1);
			Rigidbody2D rbody = GetComponent<Rigidbody2D> ();
			rbody.AddForce (new Vector2 (dir.x * knockbackX, dir.y * knockbackY));
		}

		yield return 0;
	}

	public IEnumerator KnockbackX(float knockbackDir){
		float timer = 0;
		while (knockDurX > timer) {
			timer += Time.deltaTime;
			Vector2 dir = new Vector2 (knockbackDir, 1);
			Rigidbody2D rbody = GetComponent<Rigidbody2D> ();
			rbody.AddForce (new Vector2 (dir.x * knockbackX, 0));
		}
		yield return 0;
	}

	public IEnumerator KnockbackY(float knockbackDir){
		float timer = 0;
		while (knockDurY > timer) {
			timer += Time.deltaTime;
			Vector2 dir = new Vector2 (1, knockbackDir);
			Rigidbody2D rbody = GetComponent<Rigidbody2D> ();
			rbody.AddForce (new Vector2 (0, dir.y * knockbackY));
		}
		yield return 0;
	}

	public void SimpleKnockback(float knockbackDir){
			Vector2 dir = new Vector2 (knockbackDir, 1);
			Rigidbody2D rbody = GetComponent<Rigidbody2D> ();
			rbody.AddForce (new Vector2 (dir.x * knockbackX, dir.y * knockbackY));
	}

}
