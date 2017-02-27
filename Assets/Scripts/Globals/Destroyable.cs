using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destroyable : MonoBehaviour {

	protected SpriteRenderer spriteRenderer;
	private bool crRunning = false;

	abstract public void DestroySelf ();

	public IEnumerator Flicker(float duration){
		if (!crRunning) {
			crRunning = true;

			InvokeRepeating ("ToggleRenderer", 0.1f, 0.1f);
			yield return new WaitForSeconds (duration);
			stopFlicker ();
		}
	}

	public void stopFlicker(){
		CancelInvoke ("ToggleRenderer");
		crRunning = false;
		spriteRenderer.enabled = true;
	}

	public IEnumerator CustomFlicker(float duration, float rate){
		if (!crRunning) {
			crRunning = true;

			InvokeRepeating ("ToggleRenderer", 0.1f, rate);
			yield return new WaitForSeconds (duration);
			CancelInvoke ("ToggleRenderer");
			spriteRenderer.enabled = true;

			crRunning = false;
		}
	}

	private void ToggleRenderer(){
		spriteRenderer.enabled = !spriteRenderer.enabled;
	}
}
