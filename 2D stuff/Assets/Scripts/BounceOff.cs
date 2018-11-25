
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BounceOff : MonoBehaviour
{
    private GameObject player;
    private CircleCollider2D playerCollider;
    private BoxCollider2D platformCollider;
    private CharacterController2D playerController;

    //[SerializeField] private float horizontalForce
    [SerializeField] private float verticalForce;

    
    // Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerCollider = player.GetComponent<CircleCollider2D> ();
        platformCollider = this.GetComponent<BoxCollider2D> ();
        playerController = player.GetComponent<CharacterController2D> ();
	}


    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject == player && (playerCollider.transform.position.y - playerCollider.bounds.size.y) > (platformCollider.transform.position.y + platformCollider.bounds.size.y / 2))
        {
            playerController.Bounce (verticalForce);
        }
    }
}
