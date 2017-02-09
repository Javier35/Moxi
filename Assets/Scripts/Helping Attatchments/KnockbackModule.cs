using UnityEngine;
using System.Collections;

public class KnockbackModule : MonoBehaviour {

	//private Rigidbody2D rbody;
	public float knockDur = 1.2f; //maximum duration of knockback in x and y axis
	public float knockbackX = 1f;
	public float knockbackY = 1f;

	public float knockDurX = 2F; //maximum duration of knockback in x axis
	public float knockDurY = 1.2F; //maximum duration of knockback in y axis

	private float force = 40f;
	private float repeatRate = 10f;
	private float deceleration = 5f;

	private Rigidbody2D rbody;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
	}

	public void OtherKnockback(float knockbackDir, float xPower, float yPower){
		rbody.velocity = new Vector2 (xPower * knockbackDir, yPower);
		StartCoroutine(PushBackX (knockbackDir, 100, 1));
	}

	public IEnumerator PushBackX(float knockbackDir, float force, float counter1){

		rbody.AddForce (new Vector2 (force * knockbackDir, 2));
		new WaitForSeconds (0.05f);
			
		if (force > 0) {
			new WaitForSeconds (0.6f);


			if (counter1 < 4)
				counter1++;
			
			force -= (10 + counter1);
			StartCoroutine (PushBackX (knockbackDir, force, counter1));
		}
		yield return 0;
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

}
