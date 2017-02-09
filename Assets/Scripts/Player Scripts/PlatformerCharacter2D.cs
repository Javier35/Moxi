using System;
using UnityEngine;
using System.Collections;

public class PlatformerCharacter2D : MonoBehaviour
{
    [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
    [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
    [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character
	[SerializeField] private float m_KnockbackHeight = 300f;

//    private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .15f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.

    private Transform m_CeilingCheck;   // A position marking where to check for ceilings
	private Transform m_EdgeCheck;   // A position marking where to check for ceilings

    const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
    public Animator animator;            // Reference to the player's animator component.
    private Rigidbody2D m_Rigidbody2D;
    public bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private bool m_Damaged = false;

	private GroundChecker groundchecker;

    private void Awake()
    {
        // Setting up references.
//        m_GroundCheck = transform.Find("GroundCheck");
        m_CeilingCheck = transform.Find("CeilingCheck");

        animator = GetComponent<Animator>();
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

		groundchecker = GetComponentInChildren<GroundChecker>();
    }
		
    private void FixedUpdate()
    {
		m_Grounded = groundchecker.grounded;
		animator.SetBool("InGround", m_Grounded);
		animator.SetBool ("OnEdge", groundchecker.teetering);


//		if (m_Grounded) {
//			if (!Physics2D.OverlapCircle (m_EdgeCheck.position, 0.10f, m_WhatIsGround)) 
//				animator.SetBool ("OnEdge", true);
//			else
//				animator.SetBool ("OnEdge", false);
//		} else {
//			animator.SetBool ("OnEdge", false);
//		}

        // Set the vertical animation
        //animator.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }


    public void Move(float move, bool crouch, bool jump)
    {
        // If crouching, check to see if the character can stand up
        if (!crouch && animator.GetBool("Crouch"))
        {
            // If the character has a ceiling preventing them from standing up, keep them crouching
            if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
            {
				if(!animator.GetCurrentAnimatorStateInfo(0).IsName("Jump") || !animator.GetCurrentAnimatorStateInfo(0).IsName("Fall"))
					crouch = true;
            }
        }

        // Set whether or not the character is crouching in the animator
        animator.SetBool("Crouch", crouch);

        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Reduce the speed if crouching by the crouchSpeed multiplier
            //move = (crouch ? move*m_CrouchSpeed : move);
			//move = (crouch ? 0 : move);

			//Break crouch animation if player starts moving
			if(move !=0){
				animator.SetBool("Crouch",false);

				if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage") || animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) {

					move = 0;
				}

				//if he is attacking, he shouldnt move
				if ((animator.GetCurrentAnimatorStateInfo(0).IsName ("Attack") || animator.GetCurrentAnimatorStateInfo(0).IsName ("SecondAttack")) 
					&& animator.GetBool ("InGround")){
					move = 0;
				}
				if (animator.GetBool("Attack")){
					animator.SetBool ("Run", false);
				}
			}


            // Move the character
            m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);

            // If the input is moving the player right and the player is facing left...
            if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }
                // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                Flip();
            }


			//move back while damaged
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Damage") || animator.GetCurrentAnimatorStateInfo (0).IsName ("Death")) { 
				if (m_FacingRight) {
					m_Rigidbody2D.velocity = new Vector2 (-m_MaxSpeed, m_Rigidbody2D.velocity.y);
				} else {
					m_Rigidbody2D.velocity = new Vector2(m_MaxSpeed, m_Rigidbody2D.velocity.y);
				}
			}

			//knockback if damaged
			if(m_Damaged){
				m_Damaged = false;
				m_Rigidbody2D.AddForce (new Vector2 (0f, m_KnockbackHeight));
			}

        }
        // If the player should jump...
		if (m_Grounded && jump && animator.GetBool ("InGround")
			&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Damage")
			&& !animator.GetCurrentAnimatorStateInfo(0).IsName("Death")) {
			// Add a vertical force to the player.
			m_Grounded = false;
			animator.SetBool ("InGround", false);
			animator.SetBool ("Jump", true);
			animator.SetBool ("Crouch", false);
			m_Rigidbody2D.AddForce (new Vector2 (0f, m_JumpForce));

		//if the player didnt jump, but is in the air, he should be falling
		} else if (!m_Grounded && !jump && !animator.GetBool ("InGround")) {
			m_Grounded = false;
			animator.SetBool ("InGround", false);
			animator.SetBool ("Fall", true);
			animator.SetBool ("Crouch", false);
		}
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

//		if (m_FacingRight)
//			transform.position = new Vector3 (transform.position.x + 0.3f, transform.position.y, transform.position.z);
//		else
//			transform.position = new Vector3 (transform.position.x - 0.3f, transform.position.y, transform.position.z);
//
		m_FacingRight = !m_FacingRight;
    }

	public void WasDamaged(){
		m_Damaged = true;
	}
}

