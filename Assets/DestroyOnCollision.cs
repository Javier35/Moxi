using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour {

	void OnCollisionStay2D (){
		Destroy (this);
	}
}
