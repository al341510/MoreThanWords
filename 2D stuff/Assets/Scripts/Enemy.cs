using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float enemyHealth = 10f;
	public enum enemyType {NEUTRAL, FIRE, ICE};
	public enemyType enemyMagic;

	// Use this for initialization
	void Start () {
		enemyMagic = enemyType.NEUTRAL;
	}

	// Update is called once per frame
	void Update () {

	}

	public void TakeDamage(float damage)
	{
		enemyHealth -= damage;
		print("damage taken!");
	}
}
