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

                //Las puertas en cada nivel ahora tienen tags, con estas comparaciones iremos a otros niveles
                if (gameObject.CompareTag("doorLevel0"))
                {
                    StartCoroutine(NextLevel("Level1")); //para cambiar de nivel
                }
                else if (gameObject.CompareTag("doorLevel1"))
                {
                    StartCoroutine(NextLevel("Level2")); //para cambiar de nivel
                }
                else if (gameObject.CompareTag("doorLevel2"))
                {
                    StartCoroutine(NextLevel("Level3")); //para cambiar de nivel
                }


            }
		}

	}

	IEnumerator NextLevel(string destiny){
		//Tiene una espera para hacer una transicion

		yield return new WaitForSeconds (2);

		SceneManager.LoadScene (destiny); //Cambiar por el siguiente nivel cuando este
	}
}
