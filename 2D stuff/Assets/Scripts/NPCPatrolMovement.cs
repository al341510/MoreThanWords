using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este SCRIPT es el equivalente a PlayerMovement para los enemigos que patrullan las plataformas

public class NPCPatrolMovement : MonoBehaviour {

    public NPCPatrolController2D controller;

    public Animator animator;

    public Rigidbody2D Rb;

    public float runSpeed = 10f; // Velocidad de movimiento

    public float safetyDistance = 1; // Distancia a la que se detendrá antes de llegar al borde de la plataforma

    //HORIZONTAL MOVE NO FUNCIONA EXACTAMENTE IGUAL AL DE PLAYER MOVEMENT
    //SOLO ES 1 o -1 PARA LA DIRECCION - EL CALCULO SE HACE AL LLAMAR A MOVE YA QUE SIEMPRE SE MUEVE A VELOCIDAD MAXIMA
    [SerializeField]  float horizontalMove = 0f; // 1 -> Se mueve hacia la derecha // -1 -> Se mueve hacia la izquierda

    public GameObject platform; // Plataforma a la que está vinculado. Más adelante se implementará un raycast que detecta la plataforma directamente inferior para mayor automatización.

    [SerializeField] float minX, maxX; // El NPC oscilará entre estos dos valores a la hora de moverse. Se asignan en Start()

	void Start () {

        float centerX = platform.GetComponent<BoxCollider2D>().bounds.center.x;
        float width = platform.transform.localScale.x * platform.GetComponent<BoxCollider2D>().size.x; // Utiliza el tamaño del collider y la escala que se le aplica para averiguar la anchura de la plataforma InGame

        print(width);

        horizontalMove = 1;

        minX = centerX - width / 2;
        maxX = centerX + width / 2;
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove) * runSpeed);
    }

    // Se llama X veces por segundo
    private void FixedUpdate()
    {
        if (this.transform.position.x + safetyDistance > maxX)
        {
            horizontalMove = -1;
        }
        else if (this.transform.position.x - safetyDistance < minX)
        {
            horizontalMove = +1;
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed);
    }
}
