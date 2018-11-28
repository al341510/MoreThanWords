using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
				//solo cierra la puerta al tocar el jugador
				animator.SetBool("openDoor", false);
				StartCoroutine(NextLevel ()); //para cambiar de nivel
			}
		}

	}

	IEnumerator NextLevel(){
		//Tiene una espera para hacer una transicion

		yield return new WaitForSeconds (2);

		SceneManager.LoadScene ("TrueLevel2"); //Cambiar por el siguiente nivel cuando este
	}
}
