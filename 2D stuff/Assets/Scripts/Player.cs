using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Health health;

    private int startHealth = 100;

    public CharacterController2D controller;

	public Animator animator;

	public Rigidbody2D Rb;

	public float runSpeed = 40f;
	private float maxSpeed = 30f;

	float horizontalMove = 0f; //from -1 to 1
	bool jump = false;
	[SerializeField] private bool playerIsCovering = false;

	private float mainCharacterLife = 10f;
	private float attackPower = 1f;
	private int collectedKeys = 0;

	[SerializeField] private bool isAttacking;
	public enum playerMagic {NEUTRAL, FIRE, ICE};
	public playerMagic activeMagic;

    [SerializeField]
    private Text keyNumber;

    [SerializeField]
    private int keyOnMap;

    private int collectedKey = 0;

    float comboCDStart = 0.3f;
	private float comboCD;

	void Start()
	{
		isAttacking = false;
		activeMagic = playerMagic.NEUTRAL;
		comboCD = comboCDStart;
        health.setHealth(startHealth, startHealth);
    }

	// Update is called once per frame
	void Update()
	{
        keyNumber.text = collectedKey.ToString() + "/" + keyOnMap.ToString();
        //Test health
        if (Input.GetKeyDown(KeyCode.Q))
        {
            health.CurrentValue -= 20;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            health.CurrentValue += 20;
        }

        if (isAttacking)
		{
			if (comboCD > 0)
				comboCD -= Time.deltaTime;
			else //finished
			{
				if (animator.GetBool("AttackCombo"))
				{
					comboCD = comboCDStart;
					animator.SetBool("AttackCombo", false); 
				}					
				else
				{
					animator.SetBool("Attacking", false);
					isAttacking = false;
				}
			}
		}

		else
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //outside the if !cover to avoid keep moving when run and cover bug
		
		animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); 
		animator.SetFloat("VelocityY",Rb.velocity.y); //detects the Y speed for jump animation
		bool jumpButton = Input.GetButtonDown("Jump");
		//bool magicButton = Input.GetButtonDown("UseMagic");

		if (!playerIsCovering)
		{	
			bool attackButton = Input.GetButtonDown("Attack");
			if (attackButton && horizontalMove == 0 && Rb.velocity.y == 0) //if press attack 
			{			
				//CalculateImpact();
				if (!isAttacking) //and we were on idle
				{
					isAttacking = true;
					animator.SetBool("Attacking", true);
					comboCD = comboCDStart;
				}
				else //if we press in the middle of an attack
				{
					animator.SetBool("AttackCombo", true);
				}
			}			
				
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
				print("pickupmagic");
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

	IEnumerator UseMagic(playerMagic selectedMagic)
	{
		//cambiar el estado del personaje
		//iniciar particulas
		//particleSystem.Invoke(ICE)
		yield return new WaitForSeconds(5f);
		//desactivar particulas
		activeMagic = playerMagic.NEUTRAL;
	}

	/*
	void CalculateImpact ()
	{
		RaycastHit hit;
		Ray ray;
		//ray si hay enemigo
		//si lo hay
		//GiveDamage(enemigo);
	}
	*/
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
}
