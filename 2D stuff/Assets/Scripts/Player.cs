using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{	
	public CharacterController2D controller;

	public Animator animator;

	public Rigidbody2D Rb;

	public float runSpeed = 40f;
	private float maxSpeed = 30f;

	float horizontalMove = 0f; //from -1 to 1
	bool jump = false;
	[SerializeField] private bool playerIsCovering = false;

	[SerializeField]private bool isAttacking;
	[SerializeField]private int nowAnim;
	[SerializeField]private int previousAnim = 0;

	private float mainCharacterLife = 10f;
	[SerializeField] private float attackPower = 1f;
	[SerializeField] private int collectedKeys = 0;

	public enum playerMagic {NEUTRAL, FIRE, ICE};
	public playerMagic activeMagic;
	private playerMagic storedMagic;

	int noOfClicks;
	bool canClick;

	float comboCDStart = 0.55f;
	float comboCD;


	void Start()
	{
		isAttacking = false;
		activeMagic = playerMagic.NEUTRAL;
		comboCD = comboCDStart;
	}

	// Update is called once per frame
	void Update()
	{	
		if (Input.GetMouseButtonDown(0) && !isAttacking)
		{
			isAttacking = true;
			comboCD = comboCDStart;
		}
		if (isAttacking)
		{
			if (comboCD > 0)
			{
				comboCD = comboCD -= Time.deltaTime;
			} 
			else
			{
				isAttacking = false;
			}
		}

		/*
		if (isAttacking)
		{
			nowAnim = IsAttackingAnim(); //we store our animation
			//isAttacking = nowAnim != 0; //check if we are attacking
			if (nowAnim != previousAnim) //if animation has change now we can reset combo
			{
				animator.SetBool("AttackCombo", false);
			} 
			if (nowAnim == 0)
				isAttacking = false;
		}
		*/
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //outside the if !cover to avoid keep moving when run and cover bug
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); 
		animator.SetFloat("VelocityY",Rb.velocity.y); //detects the Y speed for jump animation
		bool jumpButton = Input.GetButtonDown("Jump");
		//animator.SetBool("Attacking", false);
		bool magicButton = Input.GetButtonDown("UseMagic");

		if (!playerIsCovering)
		{	
			/*
			bool attackButton = Input.GetButtonDown("Attack");
			if (attackButton) //if press attack 
			{			
				CalculateImpact();
				if (!isAttacking) //and we were on idle
				{
					isAttacking = true;							
					animator.SetBool("Attacking", true); //we are attacking (for the animator)
					animator.SetBool("AttackCombo", false);
					previousAnim = 1;
				}
				else //if we press in the middle of an attack
				{
					previousAnim = nowAnim; //store actual anim
					animator.SetBool("AttackCombo", true); //he waits for next attack
				}
			}	*/		

			if (jumpButton)
			{
				jump = true;
			}

			if (Input.GetButtonDown("Cover") && horizontalMove == 0 && Rb.velocity.y == 0) //if we press cover and we are not moving
			{
				playerIsCovering = true;
				animator.SetBool("IsCovering", true);
			}
			/*
			bool pickUpButton = Input.GetButtonDown("PickUpMagic");
			if (pickUpButton)
			{
				//comprobar si se puede recoger una magia
				//detectar el tipo
				//si se puede:
				//si hielo, storedMagic = playerMagic.ICE .. etc

			}*/
		}

		if (Input.GetButtonUp("Cover") && playerIsCovering)
		{
			playerIsCovering = false;
			animator.SetBool("IsCovering", false);
		}

	}

	void FixedUpdate() 
	{
		// Trying to Limit Speed
		if(Rb.velocity.magnitude > maxSpeed){
			Rb.velocity = Vector3.ClampMagnitude(Rb.velocity, maxSpeed);
		}
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
		////fixedDeltaTime es el tiempo desde la ultima vez que se llamó a la funión (así funciona igual de bien independientemente de cada cuanto tiempo se llame a fixedUpdate (funciona igual a 30fps que a 60fps)
		jump = false;
	}

	int IsAttackingAnim()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kallum_attack_1"))
			return 1;
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kallum_attack_2"))
			return 2;
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kallum_attack_3"))
			return 3;
		else
			return 0;		
	}

	IEnumerator UseMagic(playerMagic selectedMagic)
	{
		//cambiar el estado del personaje
		//iniciar particulas
		//particleSystem.Invoke(ICE)
		yield return new WaitForSeconds(5f);
		//desactivar particulas
		activeMagic = playerMagic.NEUTRAL;
	}


	void CalculateImpact ()
	{
		RaycastHit hit;
		Ray ray;
		//ray si hay enemigo
		//si lo hay
		//GiveDamage(enemigo);
	}

	void RecieveDamage (Enemy enemy)
	{
		float damage;
		switch (enemy.enemyMagic)
		{
		case (Enemy.enemyType.NEUTRAL):
			{
				damage = 1f;
			}
			break;
		case (Enemy.enemyType.ICE):
			{
				if (activeMagic == playerMagic.ICE)
					damage = 2f;
				else
					damage = 1.5f;								
			}
			break;
		default: //FIRE
			{
				if (activeMagic == playerMagic.FIRE)
					damage = 2f;
				else
					damage = 1.5f;					
			}
			break;
		}

		mainCharacterLife -= damage;
	}

	void GiveDamage (Enemy enemy)
	{
		float damageGiven;
		switch (enemy.enemyMagic)
		{
		case (Enemy.enemyType.ICE):
			{
				if (activeMagic == playerMagic.ICE)
				{
					damageGiven = 0f;
					//stun
				}					
				else if (activeMagic == playerMagic.FIRE)
					damageGiven = 3f;
				else //player is neutral
					damageGiven = 0.5f;
			}
			break;
		case (Enemy.enemyType.FIRE):
			{
				if (activeMagic == playerMagic.FIRE)
				{
					damageGiven = 0f;
					//stun
				}					
				else if (activeMagic == playerMagic.ICE)
					damageGiven = 3f;
				else //player is neutral
					damageGiven = 0.5f;			
			}
			break;
		default: //NEUTRAL
			{
				if (activeMagic == playerMagic.FIRE || activeMagic == playerMagic.ICE)
					damageGiven = 1.5f;
				else //player is neutral
					damageGiven = 1f;				
			}
			break;
		}

		enemy.TakeDamage(damageGiven);
	}
	/*
	void ComboStarter()
	{
		if (canClick)
		{
			noOfClicks++;
		}
		if (noOfClicks == 1)
		{
			animator.SetInteger("AnimationN", 1);
			isAttacking = true;
		}
	}

	void ComboCheck() 
	{
		canClick = false;

		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Kallum_attack_1") && noOfClicks == 1) // we are on the first and 1 click
		{
			animator.SetInteger("AnimationN", 0);//idle
			canClick = true;
			noOfClicks = 0;
		}
	}
	*/

	/*
	IEnumerator ComboCheck()
	{
		bool nextAttack = false;
		while (isAttacking)
		{
			if (Input.GetMouseButtonDown(0))
			{
				nextAttack = true;
				isAttacking = false;
			}
		}

		yield return new WaitForSeconds(2f);
		if (nextAttack)
		{
			animator.SetBool("AttackCombo", true);	
		}
		else
		{
			animator.SetBool("AttackCombo", false);
		}
			
	}*/

}
