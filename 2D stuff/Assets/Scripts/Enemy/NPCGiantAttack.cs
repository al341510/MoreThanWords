
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
    private float x;
    private float y;


    void Start ()
    {
        calculations = player.GetComponent<AttackCalculate> ();
        controller = this.GetComponent<NPCPatrolController2D> ();
        playerLayer = LayerMask.GetMask ("Player");
    }


    public override void Attack (float x, float y, float direction)
    {
        this.x = x;
        this.y = y;

        Invoke ("CalculateImpact", 0.8f);
    }


    public void CalculateImpact ()
    {
        if (Vector2.Distance(new Vector2(x, y), new Vector2(player.transform.position.x, player.transform.position.y)) <= 3 && ((controller.m_FacingRight && player.transform.position.x >= x) || !controller.m_FacingRight && player.transform.position.x <= x))
        {
            //Debug.Log("get Hit");
            if (controller.m_FacingRight) //direction of the ray
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }

            Vector3 position = transform.position;
            position.y -= 30;

            calculations.RecieveDamage(this.GetComponent<Enemy>());
            //Invoke ("DoDamage", );
            //print ("hit");
        }
    }
}