using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCArcherAttack : MonoBehaviour {
    public GameObject arrow;
    float x, y, direction;

    public void CreateArrow(float x, float y, float direction)
    {
        this.x = x; // X del arquero
        this.y = y; // Y del arquero
        this.direction = direction; // es 1 si mira a la izquierda // es casi 0 (numero muy pequeño) si mira a la derecha
        Invoke("SpawnArrow", 0.85f); // Añade delay al metodo
    }

    private void SpawnArrow()
    {
        GameObject newArrow = Instantiate(arrow, new Vector3(x, y + 0.5f, 0), Quaternion.Euler(0, direction, 0)) as GameObject;
        //Debug.Log(direction);
        if (direction < 0.01)
        {
            newArrow.GetComponent<ArrowForce>().SetAngle(20);
        }
        else
        {
            newArrow.GetComponent<ArrowForce>().SetAngle(180 - 20);
        }
    }
}
