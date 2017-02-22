using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class PlayerInputController : MonoBehaviour
{
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;
	public BoxCollider2D crouchBox;
	private float accelValue = 0;
	private Rigidbody2D rbody;

	private bool slowed = false;
	private float previousInput = 0;
	private float slowPercent = 0.5f;

	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();
	}


	private void Update()
	{
		
		InterpreteKeys ();
	}
		
	private IEnumerator increaseAcceleration(){
		if (Mathf.Abs (accelValue) < 1 && accelValue != 0) {
			accelValue = accelValue + (0.1f * (Mathf.Sign(accelValue)));
			yield return new WaitForSeconds (0.05f);
			yield return StartCoroutine (increaseAcceleration());
		}
	}


	float SlowDown(float inputMagnitude){
		if(previousInput == 0 && inputMagnitude != 0 && !slowed){
			slowed = true;
		}
		previousInput = inputMagnitude;

		if (slowed) {
			inputMagnitude *= slowPercent;
			slowPercent += 0.02f;
			if (slowPercent >= 1) {
				slowed = false;
				slowPercent = 0.5f;
			}
		}

		return inputMagnitude;
	}


	private void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.DownArrow);
		float h = Input.GetAxisRaw ("Horizontal");

		m_Character.Move(h, crouch, m_Jump);
		m_Jump = false;
	}

	private void ReduceHitboxIfCrouching(){
		if(m_Character.animator.GetCurrentAnimatorStateInfo(0).IsName("Crouch") ||
			m_Character.animator.GetCurrentAnimatorStateInfo(0).IsName("CrouchLoop"))
				crouchBox.enabled = false;
		else
				crouchBox.enabled = true;
	}

	private void InterpreteKeys () {


		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.UpArrow))
				m_Jump = true;
			else
				m_Jump = false;
		}

		if(Input.GetKeyDown(KeyCode.R))
			//SceneManagement.Scene
			SceneManager.LoadScene("LT1");

		ReduceHitboxIfCrouching ();

		if (Input.GetKeyDown (KeyCode.X)) {
			m_Character.animator.SetTrigger ("Attack");
			m_Character.animator.SetBool ("Run", false);
		}

		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			m_Character.animator.SetBool ("Crouch", false);
		}

		if (m_Character.animator.GetBool ("InGround") && Input.GetAxisRaw ("Horizontal") != 0)
			m_Character.animator.SetBool ("Run", true);
		else
			m_Character.animator.SetBool ("Run", false);

		//if it is moving forward while jumping, switch the layer to display the forward jumping animation
		if (Input.GetAxisRaw ("Horizontal") != 0 && !m_Character.animator.GetBool ("InGround")) {
			m_Character.animator.SetLayerWeight (1, 1);
		} else {
			m_Character.animator.SetLayerWeight (1, 0);
		}
	}
}