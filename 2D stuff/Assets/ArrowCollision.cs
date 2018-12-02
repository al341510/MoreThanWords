using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowCollision : MonoBehaviour {

	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {
    }

    /*private void CollisionPlatform()
    {
        if (this.GetComponent<ArrowForce>() != null && this.GetComponent<ArrowForce>().updateRotation == true)
        {
            this.GetComponent<ArrowForce>().UpdateLastAngle();
        }
        if ((this.GetComponent<BoxCollider2D>() != null))
        {
            Destroy(this.GetComponent<BoxCollider2D>());
        }
        if (this.GetComponent<Rigidbody2D>() != null)
        {
            Destroy(this.GetComponent<Rigidbody2D>());
        }
        if (this.GetComponent<ArrowCollision>() != null)
        {
            Destroy(this.GetComponent<ArrowCollision>());
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //COLLISION WITH THE PLAYER
        if (collision.transform.tag == "Player" && !collision.transform.GetComponent<Player>().GetPlayerIsCovering()) //PLAYER IS NOT COVERING
        {
            Destroy(this.gameObject);
            //Debug.Log("hit"); HACER DAÑO AQUI
        }
        else if (collision.transform.tag == "Player" && collision.transform.GetComponent<Player>().GetPlayerIsCovering()) //PLAYE IS COVERING
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-0.2f);
            this.gameObject.layer = 11;
            //Debug.Log("Blocked");
        }
        else if (collision.transform.tag == "Platform")
        {
            //Debug.Log("Plataforma");
            this.gameObject.layer = 11;
            Destroy(this.gameObject.GetComponent<Rigidbody2D>());
            Destroy(this.gameObject.GetComponent<ArrowForce>());
            Destroy(this.gameObject.GetComponent<ArrowCollision>());
            Destroy(this.gameObject, 5);
        }
    }

    private void CollisionPlayer()
    {

    }
}
