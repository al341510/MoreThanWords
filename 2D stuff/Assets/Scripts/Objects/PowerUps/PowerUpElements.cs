using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpElements : MonoBehaviour {

    [SerializeField]
    private bool destroy = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (destroy != false)
            { 
                Destroy(gameObject);
            }
        }
    }
}
