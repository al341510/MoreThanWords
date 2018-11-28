using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Health health;

    private int startHealth = 100;

	private AttackCalculate AttackCalc; //script that makes attack calcules

    public CharacterController2D controller;

	public Animator animator;

	public Rigidbody2D Rb;

	public float runSpeed = 40f;
	private float maxSpeed = 30f;

	float horizontalMove = 0f; //from -1 to 1
	bool jump = false;
	[SerializeField] private bool playerIsCovering = false;

	private float attackPower = 1f;

	[SerializeField] private bool isAttacking;

	public enum playerMagic {NEUTRAL, FIRE, ICE};
	public playerMagic activeMagic;
    public playerMagic storedMagic;
    public float magicTime = 20f;
    private string storedMagicFlag = null;
    private bool refreshMagic = false;

    [SerializeField]
    private Text keyNumber;

    [SerializeField]
    private Image elementIcon;

    [SerializeField]
    private int keyOnMap;

    private int collectedKey = 0;

    float comboCDStart = 0.3f;
	private float comboCD;

	public GameObject fireParticles;
	public GameObject iceParticles;

	void Start()
	{
		AttackCalc = GetComponent<AttackCalculate>();
		isAttacking = false;
		comboCD = comboCDStart;
        health.setHealth(startHealth, startHealth);
    }

	// Update is called once per frame
	void Update()
	{
        //keyNumber.text = collectedKey.ToString() + "/" + keyOnMap.ToString();
        //Test health
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.CurrentValue -= 20;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            health.CurrentValue += 20;
        }

        if (Input.GetButtonDown("PickUpMagic")) {

            if(storedMagicFlag == "Fire")
            {
                storedMagic = playerMagic.FIRE;
            }
            else if(storedMagicFlag == "Ice")
            {
                storedMagic = playerMagic.ICE;
            }
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
		else //isAttacking = false
			horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; //outside the if !cover to avoid keep moving when run and cover bug


		animator.SetFloat("Speed", Mathf.Abs(horizontalMove)); 
		animator.SetFloat("VelocityY",Rb.velocity.y); //detects the Y speed for jump animation
		bool jumpButton = Input.GetButtonDown("Jump");


		bool useMagicButton = Input.GetButtonDown("UseMagic"); //e

        if (useMagicButton) {
            if (storedMagic.ToString() == "FIRE")
            {
                if (activeMagic.ToString() == "FIRE")
                {
                    refreshMagic = true;
                }
                else
                {
                    activeMagic = playerMagic.FIRE;
                }
            }
            else if(storedMagic.ToString() == "ICE")
            {
                if (activeMagic.ToString() == "ICE")
                {
                    refreshMagic = true;
                }
                else
                {
                    activeMagic = playerMagic.ICE;
                }
            }
            storedMagic = playerMagic.NEUTRAL;
        }

		//use magic
		if (useMagicButton && activeMagic.ToString() == "NEUTRAL" && storedMagic.ToString() != "NEUTRAL")
		{
			//StartCoroutine("UseMagic");
		}

		if (!playerIsCovering)
		{	
			bool attackButton = Input.GetButtonDown("Attack");
			if (attackButton && horizontalMove == 0 && Rb.velocity.y == 0) //if press attack 
			{
				AttackCalc.CalculateImpact(); //raycast and hit handler
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
		}

		if (Input.GetButtonUp("Cover") && playerIsCovering) //stop covering
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PowerUpFire")
		{
            storedMagicFlag = "Fire";
		}
		if (other.tag == "PowerUpIce")
		{
            storedMagicFlag = "Ice";
		}
	}

    void OnTriggerExit2D() {
        storedMagicFlag = null;
    }

    public void beNeutral() {
        activeMagic = playerMagic.NEUTRAL;
    }

    public bool ReFreshMagic{
        get{ return refreshMagic; }
        set{ refreshMagic = value; }
    }

	private IEnumerator UseMagic () //change player's state and after 5 seconds return to normal
	{
		//print("entering coroutine");
		//change character state
		GameObject particles;
		if (storedMagic.ToString() == "FIRE")
		{
			activeMagic = Player.playerMagic.FIRE;
			fireParticles.SetActive(true);
		} 
		else
		{
			activeMagic = Player.playerMagic.ICE;
			iceParticles.SetActive(true);
		}
		storedMagic = Player.playerMagic.NEUTRAL;
		yield return new WaitForSeconds(magicTime);
		//return to normal state
		activeMagic = Player.playerMagic.NEUTRAL;
		iceParticles.SetActive(false);
		fireParticles.SetActive(false);
		//print("exit coroutine");

	}
}
