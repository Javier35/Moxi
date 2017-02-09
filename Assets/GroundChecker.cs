using UnityEngine;
using System.Collections;

public class GroundChecker : MonoBehaviour {

	[SerializeField] private BoxCollider2D groundBox;
	[HideInInspector] public bool grounded = true;
	[HideInInspector] public bool teetering = false;

	private bool rayHit1, rayHit2, rayHit3, rayHit4; //ground hit flags, are true when ray is touching ground
	private Vector3 rayStart1, rayStart2, rayStart3, rayStart4; //starting points for the 4 rays that check if character is grounded
	private float rayLength;

	private PlatformerCharacter2D characterReference;

	void Awake(){
		rayLength = groundBox.size.y / 2 + 0.1f;
		characterReference = GetComponentInParent<PlatformerCharacter2D> ();
	}

	void FixedUpdate(){
		PlaceVectorStartPoints ();
		RaycastGroundVectors ();
		InterpreteFlags ();
	}

	private void PlaceVectorStartPoints(){ // main function that raycasts the 4 rays and updates the flags if they touch ground

		rayStart1 = groundBox.bounds.center; //placing all the rays by default in the center of the player's hitbox
		rayStart2 = groundBox.bounds.center;
		rayStart3 = groundBox.bounds.center;
		rayStart4 = groundBox.bounds.center;

		rayStart1.x -= groundBox.bounds.extents.x; //moving the starting points of the rays to cover the player hitbox evenly
		rayStart2.x -= groundBox.bounds.extents.x / 4;
		rayStart3.x += groundBox.bounds.extents.x / 4;
		rayStart4.x += groundBox.bounds.extents.x;
	}

	private void RaycastGroundVectors(){

		/*
		Debug.DrawRay (rayStart1, Vector3.down, Color.red);
		Debug.DrawRay (rayStart2, Vector3.down, Color.red);
		Debug.DrawRay (rayStart3, Vector3.down, Color.red);
		Debug.DrawRay (rayStart4, Vector3.down, Color.red);
		*/

		rayHit1 = Physics2D.Raycast (rayStart1, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
		rayHit2 = Physics2D.Raycast (rayStart2, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
		rayHit3 = Physics2D.Raycast (rayStart3, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
		rayHit4 = Physics2D.Raycast (rayStart4, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
	}

	private void InterpreteFlags(){
		if (rayHit1 || rayHit2 || rayHit3 || rayHit4)
			grounded = true;
		else
			grounded = false;


		teetering = false;
		if (characterReference.m_FacingRight) {
			if (rayHit1 && !rayHit2)
				teetering = true;
		} else {
			if (rayHit4 && !rayHit3)
				teetering = true;
		}
	}

}
