
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BounceOff : MonoBehaviour
{
    private GameObject player;
    private CircleCollider2D playerCollider;
    private BoxCollider2D platformCollider;
    private Rigidbody2D playerRB;
    

    // Use this for initialization
	void Start ()
    {
        player = GameObject.FindWithTag ("Player");
        playerCollider = player.GetComponent<CircleCollider2D> ();
        platformCollider = this.GetComponent<BoxCollider2D> ();
        playerRB = player.gameObject.GetComponent<Rigidbody2D> ();
    }

    
    void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject == player && (playerCollider.transform.position.y - playerCollider.bounds.size.y) > (platformCollider.transform.position.y + platformCollider.bounds.size.y / 2))
        {
            playerRB.velocity = new Vector2 (0, 25);
        }
    }
}
