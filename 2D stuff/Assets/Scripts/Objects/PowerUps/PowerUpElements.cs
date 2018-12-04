using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpElements : MonoBehaviour {

    [SerializeField]
    private bool destroy = false;

	[SerializeField]
	private int cooldowntime = 3;
	/*
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (destroy != false)
            { 
                Destroy(gameObject);
            }
        }
    }*/
}
