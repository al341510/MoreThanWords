using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{	
    public CharacterController2D controller;

	public Animator animator;

	public Rigidbody2D Rb;

    public float runSpeed = 40f;

    float horizontalMove = 0f; //from -1 to 1
    bool jump = false;
	[SerializeField] private bool cover = false;
	private float comboCD = 0.3f;
	private float comboTime; 
	private float comboTime2;
	private bool isAttacking = false;

	void Start()
	{
		comboTime = comboCD;
	}

    // Update is called once per frame
    void Update()
    {	
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //outside the if !cover to avoid keep moving when run and cover bug
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); 
		animator.SetFloat("VelocityY",Rb.velocity.y); //detects the Y speed for jump animation

		bool attackButton = Input.GetButtonDown("Attack");
		bool jumpButton = Input.GetButtonDown("Jump");

		if (!cover)
		{			
			if (jumpButton)
			{
				jump = true;
				//IsJumping = true
			}

			if (Input.GetButtonDown("Cover"))
			{
				cover = true;
				animator.SetBool("IsCovering", true);
			}

			if (attackButton) //if press attack
			{
				isAttacking = true;
				if (isAttacking)
				{
					animator.SetBool("AttackCombo", true);
					comboTime = comboCD;
				}						
				animator.SetBool("Attacking", true);

			}
			else 
				animator.SetBool("Attacking", false);
		
			if (comboTime > 0 && isAttacking)
			{
				comboTime -= Time.deltaTime;
			} 
			else
			{
				comboTime = comboCD;
				animator.SetBool("AttackCombo", false);	
				isAttacking = false;
			}
		}

		if (Input.GetButtonUp("Cover"))
		{
			cover = true;
			animator.SetBool("IsCovering", true);
		}

    }

    void FixedUpdate() 
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        ////fixedDeltaTime es el tiempo desde la ultima vez que se llamó a la funión (así funciona igual de bien independientemente de cada cuanto tiempo se llame a fixedUpdate (funciona igual a 30fps que a 60fps)
        jump = false;
    }
}
