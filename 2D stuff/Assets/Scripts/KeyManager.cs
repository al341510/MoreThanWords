using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour {
	/*
	 * En el objeto manager, controla la cantidad de 
	 * llaves que hay en el nivel (cada llave se añade 
	 * mediante otro script)
	*/
	public int levelKeys = 0;
	private bool levelCompleted = false;

	void Start () { //Funciona con una sola puerta en el nivel
		
	}

	public void AddLevelKey () {
		levelKeys++;
	}

	public void SubstractKey () {
		levelKeys = levelKeys-1;
		if (levelKeys == 0) {
			CompleteLevel ();
		}
	}

	public void CompleteLevel() {
		levelCompleted = true;
	}

	public bool IsCompleted() {
		return levelCompleted;
	}
}
