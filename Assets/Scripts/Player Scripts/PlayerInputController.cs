using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (PlatformerCharacter2D))]
public class PlayerInputController : MonoBehaviour
{
	private SpriteEffector spriteEffector;
	private PlatformerCharacter2D m_Character;
	private PlayerDamageManager damageManager;
	private bool m_Jump;
	private Rigidbody2D rbody;
	private RigidbodyConstraints2D originalConstraints;
	float startTimer;
	float pressTime;
	float finalPressTime;
	bool attackEnabled = true;

	bool crouch;
	float h;

	private void Awake()
	{
		m_Character = GetComponent<PlatformerCharacter2D>();
		spriteEffector = GetComponent<SpriteEffector>();
		rbody = GetComponent<Rigidbody2D>();
		originalConstraints = rbody.constraints;
		damageManager = GetComponent<PlayerDamageManager>();
	}


	private void Update()
	{
		InterpreteKeys ();
		TintOnCharge();
		ThirdChageBehavior();
		
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
		BreakAttackChargeOnStates ();

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

	void BreakAttackChargeOnStates(){
		if (m_Character.animator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack") ||
		m_Character.animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage")) {
			pressTime = 0;
		}
	}

	void InterpreteAttackInput(){
		if (Input.GetKeyDown (KeyCode.X)) {
			startTimer = Time.time;
		}else if (Input.GetKey (KeyCode.X)) {
			pressTime = Time.time - startTimer;
		}else if (Input.GetKeyUp (KeyCode.X)) {

			if(!m_Character.animator.GetCurrentAnimatorStateInfo(0).IsTag("Damage")){

				var inAttackState = m_Character.animator.GetCurrentAnimatorStateInfo (0).IsTag ("Attack");

				if(!inAttackState || ( inAttackState && m_Character.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f) ){
					if (attackEnabled ||  (!attackEnabled && pressTime >= 0.5f)) {
						m_Character.animator.SetTrigger ("Attack");
						m_Character.animator.SetBool ("Run", false);
						m_Character.animator.SetFloat ("PressTime", pressTime);
						pressTime = 0;
						DisableAttackingForTime (0.6f);
					}
				}
			}
		}
	}

	void TintOnCharge(){
		if(pressTime > 0.5f && pressTime < 1.5f){
			spriteEffector.TintPink();
		}else if(pressTime > 1.5f){
			spriteEffector.TintRed();
		}else{
			spriteEffector.RestoreColor();
		}
	}

	bool charged;
	void ThirdChageBehavior(){
		if(m_Character.animator.GetCurrentAnimatorStateInfo(0).IsName("ThirdAttack")){
			rbody.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
			damageManager.SetInvincibility();
			charged = true;
		}else{
			if(charged){
				rbody.constraints = originalConstraints;
				damageManager.ResetInvincibility();
				charged = false;
			}
		}
	}

	public void EnableAttacking(){
		attackEnabled = true;
	}

	public void DisableAttacking(){
		attackEnabled = false;
	}

	public void DisableAttackingForTime(float time){
		attackEnabled = false;
		Invoke ("EnableAttacking", time);
	}
}