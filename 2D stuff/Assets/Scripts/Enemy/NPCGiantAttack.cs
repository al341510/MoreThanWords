
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCGiantAttack : Attacker
{
    private NPCPatrolController2D controller;
    private int direction;
    public float giantAttackRange;
    public LayerMask playerLayer;
    [SerializeField] private Player player;
    private AttackCalculate calculations;
    private RaycastHit2D hitInfo;


    void Start ()
    {
        calculations = player.GetComponent<AttackCalculate> ();
        controller = this.GetComponent<NPCPatrolController2D> ();
        playerLayer = LayerMask.GetMask ("Player");
        giantAttackRange = 10;
    }


    public override void Attack (float x, float y, float direction)
    {
        // Okay, I actually have no idea how this works.
    }


    public void CalculateImpact ()
    {
        if (controller.m_FacingRight) //direction of the ray
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }

        hitInfo = Physics2D.Raycast (transform.position, Vector2.right * direction, giantAttackRange, playerLayer);
        Debug.DrawRay (transform.position, Vector2.right, Color.green);
        if (hitInfo.collider.gameObject.CompareTag("Player"))
        {
            calculations.RecieveDamage (this.GetComponent<Enemy> ());
            print ("hit");
        }
    }
}