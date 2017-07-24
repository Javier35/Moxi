using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class PlayerInputController : MonoBehaviour
{
	private PlatformerCharacter2D m_Character;
	private bool m_Jump;
	private Rigidbody2D rbody;
	float startTimer;
	float pressTime;
	float finalPressTime;

	bool crouch;
	float h;

	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();
	}


	private void Update()
	{
		InterpreteKeys ();

		// Read the inputs.
		crouch = Input.GetKey(KeyCode.DownArrow);
		h = Input.GetAxisRaw ("Horizontal");

		m_Character.Move(h, crouch, m_Jump);
		m_Jump = false;
	}

	private void InterpreteKeys () {


		if (!m_Jump)
		{
			// Read the jump input in Update so button presses aren't missed.
			if (Input.GetKeyDown (KeyCode.Z))
				m_Jump = true;
			else
				m_Jump = false;
		}

		if(Input.GetKeyDown(KeyCode.R)){
			var scene = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene(scene, LoadSceneMode.Single);
		}

		InterpreteAttackInput();

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

	void InterpreteAttackInput(){
		if (Input.GetKeyDown (KeyCode.X)) {
			startTimer = Time.time;
		}else if (Input.GetKey (KeyCode.X)) {
			pressTime = Time.time - startTimer;
		}else if (Input.GetKeyUp (KeyCode.X)) {

			if(!m_Character.animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") && 
			!m_Character.animator.GetCurrentAnimatorStateInfo(0).IsName("Death")){

				if( !m_Character.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") && 
				!m_Character.animator.GetCurrentAnimatorStateInfo(0).IsName("SecondAttack")){

					m_Character.animator.SetTrigger ("Attack");
					m_Character.animator.SetBool ("Run", false);

				}else if(m_Character.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7f){
						//f it is attacking but its halfway done
					m_Character.animator.SetTrigger ("Attack");
					m_Character.animator.SetBool ("Run", false);
					//behavoir depending on time
				}
			}
		}
	}
}