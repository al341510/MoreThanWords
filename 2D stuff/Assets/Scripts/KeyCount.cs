using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCount : MonoBehaviour {

	private GameObject manager;
	private KeyManager keyManager;
	// Use this for initialization
	void Start () { //Funciona con una sola puerta en el nivel
		manager = GameObject.Find("Manager");
		keyManager = manager.GetComponent<KeyManager> ();
		keyManager.AddLevelKey ();
	}

}
