using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
 The archer class inherits from the AI-class
     */
public class ArcherAI : AI
{
    /*
     Default values Attributes which the archer initiate, when no value is choosen
    */
    [Range(0, .3f)]
    [SerializeField]
    private float smoothMove = .05f;

    [SerializeField]
    private float archerLife = 10f;

    [SerializeField]
    private float archerSpeed = 1f;

    [SerializeField]
    private float attack_cooldown = 1f;

    [Range(0, 2)]
    [SerializeField]
    private int magic = 2;


    //Archer specific attributes
    private bool isPlattformB = false;
    private int layer_mask;
    private int layer_maskP;

    private bool reAttack = false;

    // Use this for initialization
    void Start () {

        trans = this.transform;
        rgb = this.GetComponent<Rigidbody2D>();
        MovementSmoothing = smoothMove;

        EnemyHealth = archerLife;
        EnemySpeed = archerSpeed;
        ElementNumber = magic;

        //Archer specific
        IsArcher = 1;
        layer_mask = LayerMask.GetMask("Platform");
        layer_maskP = LayerMask.GetMask("Default");

    }
	
	// Update is called once per frame
	void Update () {

        /*
         The purpose of this part is no check, if between player and enemy is a wall. 
         At first Player has to be notice from enemy, that happen via collider (see OnTriggerEnter2D and OnTriggerExit2D).
         */
        if (Target != null) {

            /*
             I dont add collider behind the enemy, so the player can sneak behind the enemy, but if the enemy has detected the player and the player
             jump behind the enemy, the enemy will automatly fip 
             */
            if (Target.position.x > this.transform.position.x && trans.right.x != 1)
            {
                flip();    
            }
            else if(trans.right.x != -1 && Target.position.x < this.transform.position.x)
            {
                flip();
            }
                  
            /*
             Check with Raycast if between player and enemy is a wall
             If yes, enemy stop shooting, but if they are reconnected again, start shooting
             */
            RaycastHit2D isPlattform = Physics2D.Raycast(transform.position, (Target.position - transform.position).normalized, Vector2.Distance(transform.position, Target.position), layer_mask);
            if (isPlattform != true)
            {

                //When view is block, check until view is not more block
                RaycastHit2D isPlayer = Physics2D.Raycast(transform.position, (Target.position - transform.position).normalized, Vector2.Distance(transform.position, Target.position), layer_maskP);
                if (isPlayer != false)
                {
                    if (reAttack != false)
                    { 
                        setAttack(attack_cooldown, true);
                        reAttack = false;
                    }
                }
            }
            else {
                setAttack(attack_cooldown, false);
                reAttack = true;
            }
        }
    }

    /*
     OnTriggerEnter2D and OnTriggerExit2D, says what happen when player enter the collider or leaving. By endering, start shooting,
     saf target and stop moving (code is in AI-Class)
     If leavinf, stop shooting, move normal and lost target
         */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Target = other.transform;
            setAttack(attack_cooldown, true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Target = null;
            isPlattformB = false;
            EnemySpeed = archerSpeed;
            HorizontalMove = 1;
            setAttack(attack_cooldown, false);
        }
    }

}
