using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour {
	private Animator animator;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void OnTriggerEnter2D (Collider2D other)
	{
		bool doorOpen = animator.GetBool ("openDoor");

		if (other.gameObject.CompareTag ("Player")) {
			if (doorOpen) {
				//solo cierra la puerta al tocar el jugador, pero podria cambiar de nivel
				//se puede hacer que se abra solo si el jugador esta cerca (la puerta esta en pantalla)
				animator.SetBool("openDoor", false); 
			}
		}

	}
}
