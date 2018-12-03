using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCArcherAttack : Attacker {
    public GameObject arrow;
    float x, y, direction;
    GameObject newArrow;

    public override void Attack(float x, float y, float direction)
    {
        this.x = x; // X del arquero
        this.y = y; // Y del arquero - HAY UN DESAJUSTE DEL CENTRO DEL ARQUERO A LA POSICION Y DEL ARQUERO - APROXIMACION DE 1 USADA EN yDiff
        this.direction = direction; // es 1 si mira a la izquierda // es casi 0 (numero muy pequeño) si mira a la derecha



        Invoke("SpawnArrow", 0.5f); // Añade delay al metodo
    }

    private void SpawnArrow()
    {
        float angulo;
        float xDiff = GameObject.FindGameObjectWithTag("Player").transform.position.x - this.x;
        float yDiff = GameObject.FindGameObjectWithTag("Player").transform.position.y - this.y - 1;
        float addX = 0.4f;

        if (this.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            addX = -addX;
        }

        angulo = Mathf.Atan2(yDiff, xDiff) * 180 / Mathf.PI;
        newArrow = Instantiate(arrow, new Vector3(x + addX, y + 0.5f, 0), Quaternion.Euler(0, direction, 0)) as GameObject;
        newArrow.GetComponent<ArrowCollision>().SetEnemyReference(this.gameObject);
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
