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

    // 3 TIPOS DE RANGO DE DETECCIÓN PARA EL ENEMIGO. ORDENADOS DE MAYOR ALCANCE A MENOR ALCANCE
    public float passiveRange = 10f; // DISTANCIA A LA QUE EL ENEMIGO DEJA DE ATACAR AL JUGADOR

    public float detectionRange = 8f; // DISTANCIA A LA QUE EL ENEMIGO DETECTA AL JUGADOR Y SE LE ACERCA

    public float attackRange = 4f; // DISTANCIA A LA QUE EL ENEMIGO REALIZA EL ATAQUE

    string status; // STATUS DEL ENEMIGO - combat // noCombat - ABIERTO A MAS OPCIONES

    //HORIZONTAL MOVE NO FUNCIONA EXACTAMENTE IGUAL AL DE PLAYER MOVEMENT
    //SOLO ES 1 o -1 PARA LA DIRECCION - EL CALCULO SE HACE AL LLAMAR A MOVE YA QUE SIEMPRE SE MUEVE A VELOCIDAD MAXIMA
    [SerializeField]  float horizontalMove = 0f; // 1 -> Se mueve hacia la derecha // -1 -> Se mueve hacia la izquierda

    public GameObject platform; // Plataforma a la que está vinculado. Más adelante se implementará un raycast que detecta la plataforma directamente inferior para mayor automatización.

    [SerializeField] float minX, maxX; // El NPC oscilará entre estos dos valores a la hora de moverse. Se asignan en Start()

    [SerializeField] GameObject player;

	void Start () {

        float centerX = platform.GetComponent<BoxCollider2D>().bounds.center.x;
        float width = platform.transform.localScale.x * platform.GetComponent<BoxCollider2D>().size.x; // Utiliza el tamaño del collider y la escala que se le aplica para averiguar la anchura de la plataforma InGame

        print(width);

        horizontalMove = 1;

        minX = centerX - width / 2;
        maxX = centerX + width / 2;

        status = "noCombat";

        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove) * runSpeed);
        if (Mathf.Abs(this.transform.position.x - player.transform.position.x) < detectionRange)
        {
            status = "combat";
        }
        else if (Mathf.Abs(this.transform.position.x - player.transform.position.x) > passiveRange && status != "noCombat")
        {
            status = "noCombat";
            horizontalMove *= -1;
        }
    }

    // Se llama X veces por segundo
    private void FixedUpdate()
    {
        if (status == "noCombat")
        {
            if (this.transform.position.x + safetyDistance > maxX)
            {
                horizontalMove = -1;
            }
            else if (this.transform.position.x - safetyDistance < minX)
            {
                horizontalMove = +1;
            }
        }
        if (status == "combat")
        {
            if (Mathf.Abs(this.transform.position.x - player.transform.position.x) > detectionRange)
            {
                if (this.transform.position.x - player.transform.position.x <  0)
                {
                    horizontalMove = +1;
                }
                else
                {
                    horizontalMove = -1;
                }
            }
            else
            {
                horizontalMove = 0;
                // AQUI SE LLAMARÁ AL ATAQUE DE NPC_PATROL_CONTROLLER_2D
            }
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed);
    }
}
