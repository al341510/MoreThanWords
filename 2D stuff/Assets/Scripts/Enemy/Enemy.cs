using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	int enemyHealth = 100;
	public enum enemyType {NEUTRAL, FIRE, ICE};
	public enemyType enemyMagic;

	public void TakeDamage(int damage)
	{
		enemyHealth -= damage;
		//print(damage + " damage taken!");
		if (enemyHealth <= 0)
		{
			//print("dead");
			Destroy(gameObject);
		}
	}

	//attack functions here 
}
