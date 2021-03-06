﻿
using UnityEngine.SceneManagement;
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

    //Time how long magic is active
    public float magicTime = 20f;

    //Flag to safe storedmagic
    private string storedMagicFlag = null;

    //important for refreshing Magic when active magic and storedmagic is the same
    //is use in ElementIconEffect
    private bool refreshMagic = false;

    //How many keys are in the map,and how many you have (default 10/10) 
    [SerializeField]
    private Text keyNumber;

	public GameObject powerUp;

    [SerializeField]
    private Image elementIcon;

    //numbers of key in on map, has to put manuell
    [SerializeField]
    private int keyOnMap;

    //numbers of the collected key
    public int collectedKey = 0;

    float comboCDStart = 0.3f;
	private float comboCD;

	public GameObject fireParticles;
	public GameObject iceParticles;

	public bool ElementFlag = false;

	void Start()
	{
		AttackCalc = GetComponent<AttackCalculate>();
		isAttacking = false;
		comboCD = comboCDStart;
        health.setHealth(startHealth, startHealth);

    }

	// Update is called once per frame
	void Update ()
	{
        if (!animator.GetBool("Death"))
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

            // set element into storedMagic, depends on OntriggerEnter2D and Exit
            if (Input.GetButtonDown("PickUpMagic"))
            {
				if (storedMagicFlag == "Fire")
				{
					StartCoroutine(DisablePowerUp(powerUp));
					storedMagic = playerMagic.FIRE;
				} else if (storedMagicFlag == "Ice")
				{
					StartCoroutine(DisablePowerUp(powerUp));
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
            animator.SetFloat("VelocityY", Rb.velocity.y); //detects the Y speed for jump animation
            bool jumpButton = Input.GetButtonDown("Jump");


            bool useMagicButton = Input.GetButtonDown("UseMagic"); //e

            /*
         When activate magic, check storadMagic and use that magic, if same element is in use set refreshMagic as true (important for
         ElementIconEffect). After activating set storadmagig to Neutral
         */
            if (useMagicButton)
            {
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
                else if (storedMagic.ToString() == "ICE")
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

            /*use magic
		if (useMagicButton && activeMagic.ToString() == "NEUTRAL" && storedMagic.ToString() != "NEUTRAL")
		{
			//StartCoroutine("UseMagic");
		}*/

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
        else
        {
            StartCoroutine (Respawn ());
			horizontalMove = 0;
			animator.SetFloat("VelocityY", 0); //cutre
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

    
    //set Flag to the element where the player is staying at
	void OnTriggerEnter2D(Collider2D other)
	{
		powerUp = other.gameObject;
		if (other.tag == "PowerUpFire")
		{
			ElementFlag = true;
			storedMagicFlag = "Fire";
			ElementFlag = false;
		}
		if (other.tag == "PowerUpIce")
		{
			ElementFlag = true;
			storedMagicFlag = "Ice";
			ElementFlag = false;
		}
	}


    // set flag to null by exit
    void OnTriggerExit2D() {
		powerUp = null;
        storedMagicFlag = null;
    }

    //actually this is a setter (is in use in ElementIconEffect)
    public void beNeutral() {
        activeMagic = playerMagic.NEUTRAL;
    }

    //getter and setter for refreshmagic
    public bool ReFreshMagic{
        get{ return refreshMagic; }
        set{ refreshMagic = value; }
    }

	public int CollectedKey{
		get{ return collectedKey; }
		set{ collectedKey = value;}
	}

    public bool GetPlayerIsCovering() //information for the enemies
    {
        return playerIsCovering;
    }


    IEnumerator Respawn ()
    {
        yield return new WaitForSeconds (4.5f);
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }

	private IEnumerator DisablePowerUp(GameObject powerUp){
		if (powerUp.tag == "PowerUpIce" && storedMagic.ToString() != "ICE" || powerUp.tag == "PowerUpFire" && storedMagic.ToString() != "FIRE" || storedMagic.ToString() == "NEUTRAL")
		{
			powerUp.SetActive(false);
			storedMagicFlag = null;
			//print("called");
			yield return new WaitForSeconds(5f);
			powerUp.SetActive(true);	
		}

	}
}
