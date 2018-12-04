using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCalculate : MonoBehaviour {


	public LayerMask enemyLayer;
	private CharacterController2D controller;
	private Player player; 
	private Health health;
	public float kallumAtttackRange = 2f;
	public Animator animator;
	int direction;


	void Start () {
		controller = GetComponent<CharacterController2D>();
		player = GetComponent<Player>();
	}

	public void CalculateImpact ()
	{
		if (controller.m_FacingRight) //direction of the ray
			direction = 1;
		else
			direction = -1;
		
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.right * direction, kallumAtttackRange, enemyLayer);
		//Debug.DrawRay(transform.position, Vector2.right, Color.green);
		if (hitInfo.collider != null)
		{
			GiveDamage(hitInfo.collider.gameObject.GetComponent<Enemy>());
		}
	}
		

	void GiveDamage (Enemy enemy) //calls an enemy function to rest life depending on both character magic stats
	{
		int damageGiven;
		switch (enemy.enemyMagic)
		{
		case (Enemy.enemyType.ICE):
			{
				if (player.activeMagic.ToString() == "ICE") //worst combination
				{
					damageGiven = 0;
					//stun
					StartCoroutine("AttackFailAnimation");
				}					
				else if (player.activeMagic.ToString() == "FIRE") //best combination
					damageGiven = 20; 
				else //player is neutral
					damageGiven = 5;
			}
			break;
		case (Enemy.enemyType.FIRE):
			{
				if (player.activeMagic.ToString() == "FIRE") //worst combination
				{
					damageGiven = 0;
					//stun
					StartCoroutine("AttackFailAnimation");
				}					
				else if (player.activeMagic.ToString() == "ICE") //best combination
					damageGiven = 20;
				else //player is neutral
					damageGiven = 5;			
			}
			break;
		default: //NEUTRAL
			{
				if (player.activeMagic.ToString() == "FIRE" || player.activeMagic.ToString() == "ICE") 
					damageGiven = 15;
				else //player is neutral
					damageGiven = 10;				
			}
			break;
		}
		enemy.TakeDamage(damageGiven);
	}


	public void RecieveDamage (Enemy enemy) //not already in use, this is for the enemies in a future
	{
		int damage;
		switch (enemy.enemyMagic)
		{
		case (Enemy.enemyType.NEUTRAL):
			{
				damage = 10;
			}
			break;
		case (Enemy.enemyType.ICE):
			{
				if (player.activeMagic.ToString() == "ICE")
					damage = 20;
				else
					damage = 15;								
			}
			break;
		default: //FIRE
			{
				if (player.activeMagic.ToString() == "FIRE")
					damage = 20;
				else
					damage = 15;					
			}
			break;
		}

		health.CurrentValue -= damage;
	}

	IEnumerator AttackFailAnimation() //activates and deactivates parry with a little delay to avoid bugs and calcules
	{
		animator.SetBool("AttackFail",true);
		yield return new WaitForSeconds (0.3f);
		animator.SetBool("AttackFail",false);
	}
}