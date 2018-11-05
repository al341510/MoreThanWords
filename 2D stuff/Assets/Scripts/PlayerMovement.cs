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
	bool cover = false;

    // Update is called once per frame
    void Update()
    {		
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //outside the if !cover to avoid keep moving when run and cover bug
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); 
		animator.SetFloat("VelocityY",Rb.velocity.y); //detects the Y speed for jump animation

		if (!cover)
		{
			
			if (Input.GetButtonDown("Jump"))
			{
				jump = true;
				//IsJumping = true
			}

			if (Input.GetButtonDown("Cover"))
			{
				cover = true;
				animator.SetBool("IsCovering", true);
			}
		}

		if (Input.GetButtonUp("Cover"))
		{
			cover = false;
			animator.SetBool("IsCovering", false);
		}

    }

    void FixedUpdate() ////no se llama cada frame sino X veces por segundo
    {
        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
        ////fixedDeltaTime es el tiempo desde la ultima vez que se llamó a la funión (así funciona igual de bien independientemente de cada cuanto tiempo se llame a fixedUpdate (funciona igual a 30fps que a 60fps)
        jump = false;
    }
}
