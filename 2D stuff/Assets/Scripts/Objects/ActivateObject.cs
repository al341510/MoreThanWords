using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour {

    public GameObject[] gameObjects;
	
    public void Appear()
    {
        for(int i = 0; i<gameObjects.Length; i++)
        {
            gameObjects[i].SetActive(true);
        }
    }
}
