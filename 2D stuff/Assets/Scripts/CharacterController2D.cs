using UnityEngine;
//using UnityEngine.Events; //FOR EVENTS

public class CharacterController2D : MonoBehaviour
{
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // Amount of movement smooth
    [SerializeField] private float m_JumpForce = 400f;                          // Amount of force added when the player jumps.   
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.    

    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is touching the ground.

    private Rigidbody2D m_Rigidbody2D; // Player physics
    public bool m_FacingRight = true; // For determining which way the player is currently facing.
    private Vector3 velocity = Vector3.zero;


	/* HOW TO CREATE AN EVENT
	[Header("Events")]
	[Space]

	public UnityEvent eventName;

	//invoke it with eventName.Invoke();

	*/

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		m_Grounded = false;
		/* //FOR THE EVENT
		if (eventName == null)
			eventName = new UnityEvent();
		*/ 
	}


    private void FixedUpdate ()
    {
        m_Grounded = false;        

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// We should do this using layers, but this way Sample Assets will not overwrite your project settings
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
			if (colliders [i].gameObject != gameObject)
				m_Grounded = true;		                   
        }
    }

	public void Move(float move, bool jump)
    {
        //only control the player if grounded or airControl is turned on
        if (m_Grounded || m_AirControl)
        {
            // Move the character by finding the target velocity
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
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));      //crear fuera cuando tengas ganas de vivir      
        }			
    }


    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0,-180,0);
    }
}
