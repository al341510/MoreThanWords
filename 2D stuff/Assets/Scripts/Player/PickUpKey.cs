using UnityEngine;
using System.Collections;

public class PickUpKey : MonoBehaviour {
	private GameObject manager;
	private KeyManager keyManager;
	private bool keyCollide = false;
	public Player player;

	private GameObject door;
	private Animator anim;
	// Use this for initialization
	void Start () {
		manager = GameObject.Find("Manager");
		keyManager = manager.GetComponent<KeyManager> ();

		door = GameObject.Find("Door");
		anim = door.GetComponent<Animator>();
	}
	// Update is called once per frame
	void Update () {
		if (keyCollide) {
			keyManager.SubstractKey ();
		}
		keyCollide = false;
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		//si hay que coger mas llaves habria que poner otro script en player y cambiar alguna variable antes de destruirla
		if (other.gameObject.CompareTag ("Key")) {
			player.collectedKey += 1; 
			Destroy(other.gameObject);
			keyCollide = true; //ESTO SE HACE PARA QUE NO HAGAN TRIGGER LAS 2 COLLIDERS (solo hace un update)
		}


		//Si colisiona con la puerta (se podria poner en otro script)
		if (other.gameObject.CompareTag ("OpenDoor")) {
			if (keyManager.IsCompleted())
				anim.SetBool("openDoor", true);
		}
			
	}
}