using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour {

	//TODO ESTO SE PUEDE PONER EN OTRO SCRIPT

	public float minSpeed;
	public float maxSpeed;

	private float lastSpeed;

	public Health health;
	private float maxHealth = 100f; //¿?

	private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		//coger valor de maxhealth
	}
	
	// Update is called once per frame
	void Update () {
		if (rb.velocity.y == 0) {
			if (lastSpeed < -maxSpeed) {
				//matar

				health.CurrentValue = 0;


				//Debug.Log ("killed");
			}
			else if (lastSpeed < -minSpeed) {
				float damage = maxHealth * (-lastSpeed - minSpeed) / (maxSpeed - minSpeed);

				health.CurrentValue -= damage;

				//Debug.Log ("damaged");
				//Debug.Log (damage);
				//hacer daño
			}
		}
		lastSpeed = rb.velocity.y;
	}
}
