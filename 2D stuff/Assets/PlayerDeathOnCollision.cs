using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathOnCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<AttackCalculate>().health.CurrentValue = 0;
        }
    }
}
