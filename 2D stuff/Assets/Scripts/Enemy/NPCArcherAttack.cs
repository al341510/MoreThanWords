using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCArcherAttack : Attacker {
    public GameObject arrow;
    float x, y, direction;
    private float rotY;
    GameObject newArrow;
    float angulo;
    float addX = 0.4f;

    public override void Attack(float x, float y, float direction)
    {
        this.x = x; // X del arquero
        this.y = y; // Y del arquero - HAY UN DESAJUSTE DEL CENTRO DEL ARQUERO A LA POSICION Y DEL ARQUERO - APROXIMACION DE 1 USADA EN yDiff
        this.direction = direction; // es 1 si mira a la izquierda // es casi 0 (numero muy pequeño) si mira a la derecha

        rotY = this.gameObject.transform.rotation.y;

        float xDiff = GameObject.FindGameObjectWithTag("Player").transform.position.x - this.x;
        float yDiff = GameObject.FindGameObjectWithTag("Player").transform.position.y - this.y - 1;

        if (this.gameObject.transform.position.x > GameObject.FindGameObjectWithTag("Player").transform.position.x)
        {
            addX = - Mathf.Abs(addX);
        }
        else
        {
            addX = Mathf.Abs(addX);
        }

        angulo = Mathf.Atan2(yDiff, xDiff) * 180 / Mathf.PI;

        if (angulo > 40 && angulo < 140){
            //LLAMAR A LA FUNCION DE CANCELAR ATAQUE
            this.gameObject.GetComponent<NPCPatrolMovement>().SetCancelAttack(true);
            return;
        }
        else
        {
            this.gameObject.GetComponent<NPCPatrolMovement>().SetCancelAttack(false);
        }

        Invoke("SpawnArrow", 0.5f); // Añade delay al metodo
    }

    private void SpawnArrow()
    {

        newArrow = Instantiate(arrow, new Vector3(x + addX, y + 0.5f, 0), Quaternion.Euler(0, direction, 0)) as GameObject;
        newArrow.GetComponent<ArrowCollision>().SetEnemyReference(this.gameObject);
        //print(this.gameObject.transform.rotation.y);
        //Debug.Log(direction);
        if (direction < 0.5)
        {
            newArrow.GetComponent<ArrowForce>().SetAngle(angulo + 25);
        }
        else
        {
            newArrow.GetComponent<ArrowForce>().SetAngle(angulo - 25);
        }
    }
}
