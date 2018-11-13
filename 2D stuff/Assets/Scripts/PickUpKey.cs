using UnityEngine;
using System.Collections;

public class PickUpKey : MonoBehaviour {
	private GameObject door;
	private Animator anim;
	// Use this for initialization
	void Start () { //Funciona con una sola puerta en el nivel
		door = GameObject.Find("Door");
		anim = door.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		//si hay que coger mas llaves habria que poner otro script en player y cambiar alguna variable antes de destruirla
		if (other.gameObject.CompareTag ("Player")) {
			Destroy(gameObject);
			anim.SetBool("openDoor", true);
		}
			
	}
}