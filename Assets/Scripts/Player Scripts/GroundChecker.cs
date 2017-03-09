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
		rayLength = 0.2f;
		characterReference = GetComponentInParent<PlatformerCharacter2D> ();
	}

	void FixedUpdate(){
		PlaceVectorStartPoints ();
		RaycastGroundVectors ();
		InterpreteRayHits ();
	}

	private void PlaceVectorStartPoints(){ // main function that raycasts the 4 rays and updates the flags if they touch ground

		var centerpointX = groundBox.bounds.center.x;
		rayStart1 = new Vector3 (centerpointX - groundBox.bounds.extents.x, groundBox.bounds.min.y + 0.1f, this.transform.position.z);
		rayStart2 = new Vector3 (centerpointX - groundBox.bounds.extents.x/4, groundBox.bounds.min.y + 0.1f, this.transform.position.z);
		rayStart3 = new Vector3 (centerpointX + groundBox.bounds.extents.x/4, groundBox.bounds.min.y + 0.1f, this.transform.position.z);
		rayStart4 = new Vector3 (centerpointX + groundBox.bounds.extents.x, groundBox.bounds.min.y + 0.1f, this.transform.position.z);
	}

	private void RaycastGroundVectors(){

		rayHit1 = Physics2D.Raycast (rayStart1, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
		rayHit2 = Physics2D.Raycast (rayStart2, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
		rayHit3 = Physics2D.Raycast (rayStart3, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
		rayHit4 = Physics2D.Raycast (rayStart4, Vector3.down, rayLength, 1 << LayerMask.NameToLayer("Platform"));
	}

	private void InterpreteRayHits(){
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
