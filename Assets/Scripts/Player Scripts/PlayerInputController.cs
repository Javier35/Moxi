using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class PlayerInputController : MonoBehaviour
{
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;
	public BoxCollider2D crouchBox;

	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();
	}


	private void Update()
	{
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
		InterpreteKeys ();
	}


	private void FixedUpdate()
	{
		// Read the inputs.
		bool crouch = Input.GetKey(KeyCode.DownArrow);
		float h = Input.GetAxisRaw ("Horizontal");
		// Pass all parameters to the character control script.
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