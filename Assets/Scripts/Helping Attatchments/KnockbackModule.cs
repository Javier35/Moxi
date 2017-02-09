using UnityEngine;
using System.Collections;

public class KnockbackModule : MonoBehaviour {

	//private Rigidbody2D rbody;
	public float knockDur = 1.2f; //maximum duration of knockback in x and y axis
	public float knockbackX = 1f;
	public float knockbackY = 1f;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
	}

	public IEnumerator Knockback(float knockbackDir){
		float timer = 0;
		while (knockDur > timer) {
			timer += Time.deltaTime;
			Vector2 dir = new Vector2 (knockbackDir, 1);
			rbody.AddForce (new Vector2 (dir.x * knockbackX, dir.y * knockbackY));
		}

		yield return 0;
	}

}
