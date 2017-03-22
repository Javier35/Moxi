using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Destroyable : MonoBehaviour {

	protected SpriteRenderer spriteRenderer;
	private bool corutineRunning = false;
	protected bool spawned = false;

	abstract public void DestroySelf ();
	abstract public void Respawn ();

	public void setSpawned(bool isSpawned){
		spawned = isSpawned;
	}

	public IEnumerator Flicker(float duration){
		if (!corutineRunning) {
			corutineRunning = true;

			InvokeRepeating ("ToggleRenderer", 0.1f, 0.1f);
			yield return new WaitForSeconds (duration);
			stopFlicker ();
		}
	}

	public void stopFlicker(){
		CancelInvoke ("ToggleRenderer");
		corutineRunning = false;
		spriteRenderer.enabled = true;
	}

	public IEnumerator CustomFlicker(float duration, float rate){
		if (!corutineRunning) {
			corutineRunning = true;

			InvokeRepeating ("ToggleRenderer", 0.1f, rate);
			yield return new WaitForSeconds (duration);
			CancelInvoke ("ToggleRenderer");
			spriteRenderer.enabled = true;

			corutineRunning = false;
		}
	}

	private void ToggleRenderer(){
		spriteRenderer.enabled = !spriteRenderer.enabled;
	}
}
