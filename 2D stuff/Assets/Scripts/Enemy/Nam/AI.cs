using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 This class is a parent-class, which means when another class use this class as base (example ArcherAI), the child class will have every function.
 So the child only need to add specific function. For example every enemies need movement, so you create a movement class which should be the parent class.
 When we creating archer, knight etc class and say this is our parent class, we dont have to write down a movement function for every enemy.
     */
public class AI : MonoBehaviour {

    private float speed;

    private float enemyHealth;

    private float enemyStrength;

    private Transform target = null;

    public Animator animator;

    private float horizontalMove = 1f;

    private enum enemyType { NEUTRAL, FIRE, ICE };

    private enemyType enemyMagic;

    //just test
    public string magic1;

    private int elementNumber;

    [SerializeField]
    private LayerMask enemyMask;

    private float m_MovementSmoothing = .05f;

    private Vector3 velocity = Vector3.zero;

    public Rigidbody2D rgb;
    public Transform trans;

    private float widh = 0;

    private float timerA;
    private float time;
    private bool attackingF = false;
    private int isArcher = 0;
    private NPCArcherAttack attacker;

    // Use this for initialization
    void Start() {
        //trans = this.transform;
        //rgb = this.GetComponent<Rigidbody2D>();
        //widh = this.GetComponent<SpriteRenderer>().bounds.extents.x;

    }

    // Update is called once per frame
    void Update() {
        //animator.SetFloat("Speed", Mathf.Abs(horizontalMove) * speed);
    }

    private void FixedUpdate()
    {
        magic1 = enemyMagic.ToString();

        // activate movement animation
        animator.SetFloat("Speed", Mathf.Abs(HorizontalMove) * speed);

        /* this part check with linecast, if the enemy reach the end of a plattform or if something else (not enemy or player) 
           block his way. If yes, flipp.         
         */
        Vector2 lineCastPos = (Vector2)trans.position - (Vector2)trans.right * widh;
        bool isGrounded = Physics2D.Linecast(lineCastPos, lineCastPos + Vector2.down, enemyMask);
        bool isBlock = Physics2D.Linecast(lineCastPos, lineCastPos - (Vector2)trans.right * 0.05f, enemyMask);

        /*
         this is yout smoothMovement part, i just add it here
         */
        Vector3 targetVelocity = new Vector2((Time.fixedDeltaTime * speed) * 10f, rgb.velocity.y);
        rgb.velocity = Vector3.SmoothDamp(rgb.velocity, targetVelocity, ref velocity, MovementSmoothing);

        if (!isGrounded || isBlock) {
            flip();
        }

        /*
         Movement speed
         */
        Vector2 myVel = rgb.velocity;
        myVel.x = trans.right.x * speed;
        rgb.velocity = myVel;

        /*
         check if child is archer or not (important for the arrows creating part)
         */
        if (IsArcher != 0)
        {
            attacker = this.GetComponent<NPCArcherAttack>();
        }

        /*
         Attack cooldown part: time is attack cooldown value. If play time is bigger then enemy can attack one time, after this this time cooldown value
         has to change intern, because times go forward, so the next time flag has to suits the new situation. But 1.2s after attack, the attack-
         animation stop. I suppost this is the animation time. Of cause animation attack stop also, when the attack flag is default
         */
        if (attackingF)
        {
            //enemy stop move by attack
            horizontalMove = 0;
            EnemySpeed = 0;
            if (Time.time > time)
            {
                //only for archer important. I dont like it, but it was late, when i create this
                if (IsArcher != 0) {
                    attacker.Attack(transform.position.x, transform.position.y, -transform.rotation.y);
                }

                animator.SetBool("Attack", attackingF);
                /*cooldown has to go with new time
                 Example: attackcooldown 3s, time starts with 0s
                 when time is 3 -> attack. Next attack has to be in time 6s -> time+cooldown = 6s
                 */
                time = Time.time + timerA;
            }

            if (Time.time > (time - timerA + 1.2f))
            {
                animator.SetBool("Attack", false);
            }
        }
        else if(!attackingF){
            animator.SetBool("Attack", false);
        }
    }

    /*
     Attack trigger function
         */
    public void setAttack(float time, bool attacking) {
        timerA = time;
        this.time = time;
        attackingF = attacking;
    }

    /*
     Flip function
         */
    public void flip() {
        Vector2 currRot = trans.eulerAngles;
        currRot.y += 180;
        trans.eulerAngles = currRot;
    }

    /*
     just there, not finish
         */
    public void takeDamage(float damage)
    {
        enemyHealth -= damage;
    }

    /*
     Well, my first language was Java, thats why I alwasy prefer getter/setter.
     In my opnion is, not good to have to open close, thats why I close most of the attribute with private.
     To access those attributes from another class, you have to you use getter/setter
         */

    public float EnemyHealth
    {
        get
        {
            return enemyHealth;
        }

        set
        {
            enemyHealth = value;
        }
    }

    public float EnemySpeed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float HorizontalMove
    {
        get
        {
            return horizontalMove;
        }

        set
        {
            horizontalMove = value;
        }
    }

    public Transform Target
    {
        get
        {
            return target;
        }

        set
        {
            target = value;
        }
    }

    public int IsArcher
    {
        get
        {
            return isArcher;
        }

        set
        {
            isArcher = value;
        }
    }

    public float EnemyStrength
    {
        get
        {
            return enemyStrength;
        }

        set
        {
            enemyStrength = value;
        }
    }

    /*
     working with enum to take values from attributes is very terrible, so I use number and swith function to add magic elements
         */
    public int ElementNumber
    {
        get
        {
            return elementNumber;
        }

        set
        {
            elementNumber = value;
            switch (elementNumber)
            {
                case 1:
                    enemyMagic = enemyType.FIRE;
                    break;
                case 2:
                    enemyMagic = enemyType.ICE;
                    break;
                default:
                    enemyMagic = enemyType.NEUTRAL;
                    break;
            }
        }
    }

    public float MovementSmoothing
    {
        get
        {
            return m_MovementSmoothing;
        }

        set
        {
            m_MovementSmoothing = value;
        }
    }
}
