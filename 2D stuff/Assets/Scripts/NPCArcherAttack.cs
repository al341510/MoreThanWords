using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCArcherAttack : Attacker {
    public GameObject arrow;
    float x, y, direction;

    public override void Attack(float x, float y, float direction)
    {
        this.x = x; // X del arquero
        this.y = y; // Y del arquero - HAY UN DESAJUSTE DEL CENTRO DEL ARQUERO A LA POSICION Y DEL ARQUERO - APROXIMACION DE 1 USADA EN yDiff
        this.direction = direction; // es 1 si mira a la izquierda // es casi 0 (numero muy pequeño) si mira a la derecha
        Invoke("SpawnArrow", 0.85f); // Añade delay al metodo
    }

    private void SpawnArrow()
    {
        float angulo;
        float xDiff = GameObject.FindGameObjectWithTag("Player").transform.position.x - this.x;
        float yDiff = GameObject.FindGameObjectWithTag("Player").transform.position.y - this.y - 1;
        angulo = Mathf.Atan2(yDiff, xDiff) * 180 / Mathf.PI;
        GameObject newArrow = Instantiate(arrow, new Vector3(x, y + 0.5f, 0), Quaternion.Euler(0, direction, 0)) as GameObject;
        //Debug.Log(direction);
        if (direction < 0.01)
        {
            newArrow.GetComponent<ArrowForce>().SetAngle(angulo + 25);
        }
        else
        {
            newArrow.GetComponent<ArrowForce>().SetAngle(angulo - 25);
        }
    }
}
