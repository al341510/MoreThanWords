using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float enemyHealth = 100f;
	public enum enemyType {NEUTRAL, FIRE, ICE};
	public int enemyMagic;

	//1 for neutral
	//2 for ice 
	//3 for fire

	public void TakeDamage(float damage)
	{
		enemyHealth -= damage;
		print(damage + "damage taken!");
		if (enemyHealth <= 0)
		{
			print("dead");
		}
	}
}
