using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Esta clase esta basada en CharacterController2D para los enemigos que patrullan

public class NPCPatrolController2D : MonoBehaviour {

    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // Amount of movement smooth

    private Rigidbody2D m_Rigidbody2D; // Player physics
    public bool m_FacingRight = true; // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        //m_Grounded = false; -- DE MOMENTO NO HAY SALTO EN EL ENEMIGO
        /* //FOR THE EVENT
		if (eventName == null)
			eventName = new UnityEvent();
		*/
    }

    public void Move(float move)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
        // And then smoothing it out and applying it to the character
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

        // If the input is moving the player right and the player is facing left or if the input is moving the player left and the player is facing right...
        if ((move > 0 && !m_FacingRight) || (move < 0 && m_FacingRight))
        {
            // ... flip the player.
            Flip();
        }
    }


    // Update is called once per frame
    void Update () {
		
	}

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0, -180, 0);
    }

    internal void LookPlayer(float xArquero, float xPlayer, bool atacando)
    {
        if (!atacando)
        {
            if (xArquero > xPlayer && m_FacingRight)
            {
                Flip();
            }
            else if (xArquero < xPlayer && !m_FacingRight)
            {
                Flip();
            }
        }
    }
}
