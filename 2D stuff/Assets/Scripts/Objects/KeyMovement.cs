using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovement : MonoBehaviour {

	float yOffsetUp;
    float yOffsetDown;

    private bool goUP = true;

    void Start() {
		yOffsetUp = transform.position.y + 0.15f;
		yOffsetDown = transform.position.y - 0.15f;
	}

    void FixedUpdate()
    {
		if (transform.position.y < yOffsetUp && goUP) {
	    	transform.Translate(Vector3.up/3 * Time.deltaTime);
	    } 
		else goUP = false;	    
		if (transform.position.y > yOffsetDown && !goUP) {			        
	        transform.Translate(-Vector3.up/3 * Time.deltaTime);
	    }
		else goUP = true;
	}
} 