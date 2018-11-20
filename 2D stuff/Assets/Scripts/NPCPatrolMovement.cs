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

    [SerializeField] string status; // STATUS DEL ENEMIGO - combat // noCombat - ABIERTO A MAS OPCIONES

    //HORIZONTAL MOVE NO FUNCIONA EXACTAMENTE IGUAL AL DE PLAYER MOVEMENT
    //SOLO ES 1 o -1 PARA LA DIRECCION - EL CALCULO SE HACE AL LLAMAR A MOVE YA QUE SIEMPRE SE MUEVE A VELOCIDAD MAXIMA
    [SerializeField]  float horizontalMove = 0f; // 1 -> Se mueve hacia la derecha // -1 -> Se mueve hacia la izquierda

    [SerializeField] GameObject platform; // Plataforma a la que está vinculado. Más adelante se implementará un raycast que detecta la plataforma directamente inferior para mayor automatización.

    [SerializeField] float minX, maxX; // El NPC oscilará entre estos dos valores a la hora de moverse. Se asignan en Start()

    [SerializeField] GameObject player;

    RaycastHit2D linecast;

    [SerializeField] private bool attacking;

    NPCArcherAttack attacker;

    public float attack_cooldown = 1.1f;

    float timer;

    void Start () {

        /*float centerX = platform.GetComponent<BoxCollider2D>().bounds.center.x;
        float width = platform.transform.localScale.x * platform.GetComponent<BoxCollider2D>().size.x; // Utiliza el tamaño del collider y la escala que se le aplica para averiguar la anchura de la plataforma InGame

        print(width);

        horizontalMove = 1;

        minX = centerX - width / 2;
        maxX = centerX + width / 2;

        status = "noCombat";

        player = GameObject.FindGameObjectWithTag("Player");*/
	}

    private void Awake()
    {
        attacker = this.GetComponent<NPCArcherAttack>();

        timer = attack_cooldown;
        attacking = false;

        float centerX = platform.GetComponent<BoxCollider2D>().bounds.center.x;
        float width = platform.transform.localScale.x * platform.GetComponent<BoxCollider2D>().size.x; // Utiliza el tamaño del collider y la escala que se le aplica para averiguar la anchura de la plataforma InGame

        print(width);

        horizontalMove = 1;

        minX = centerX - width / 2;
        maxX = centerX + width / 2;

        status = "noCombat";

        player = GameObject.FindGameObjectWithTag("Player");

        linecast = Physics2D.Linecast(this.transform.position, this.transform.position - new Vector3(0, 20, 0));

        platform = linecast.collider.gameObject;
    }

    // Update is called once per frame
    void Update () {
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove) * runSpeed);
        if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) < detectionRange)
        {
            status = "combat";
        }
        else if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) > passiveRange && status != "noCombat")
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
            if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) > attackRange && this.transform.position.x - safetyDistance > minX && this.transform.position.x + safetyDistance < maxX)
            {
                Debug.Log("entra");
                if (this.transform.position.x + 0.2 - player.transform.position.x <  0)
                {
                    horizontalMove = +1;
                }
                else if (this.transform.position.x - 0.2 - player.transform.position.x > 0)
                {
                    horizontalMove = -1;
                }
                else
                {
                    horizontalMove = 0;
                }
            }
            else if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) > attackRange && this.transform.position.x + safetyDistance > maxX && transform.position.x > player.transform.position.x)
            {
                horizontalMove = -1;
            }
            else if (Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) > attackRange && this.transform.position.x - safetyDistance < minX && transform.position.x < player.transform.position.x)
            {
                horizontalMove = +1;
            }
            else
            {
                //RANGO DE ESPERA O ATAQUE -> SE QUEDA QUIETO
                horizontalMove = 0;
                //MIRA HACIA EL PLAYER
                controller.LookPlayer(this.transform.position.x, player.transform.position.x, attacking);
                //HAY CONTACTO VISUAL ENTRE EL ARQUERO Y EL JUGADOR
                linecast = Physics2D.Linecast(this.transform.position, player.transform.position, ~(1<<9));
                if(linecast.collider.gameObject.name == "Kallum")
                {
                    if (!attacking && Mathf.Abs(Vector3.Distance(this.transform.position, player.transform.position)) < attackRange)
                    {
                        attacker.CreateArrow(transform.position.x, transform.position.y, transform.rotation.y);
                        //Debug.Log(transform.rotation);
                        attacking = true;
                        animator.SetBool("Attack", attacking);
                    }
                }
            }
        }

        if (attacking)
        {
            horizontalMove = 0;
            timer -= Time.fixedDeltaTime;
            if (timer<= 0)
            {
                attacking = false;
                animator.SetBool("Attack", attacking);
                timer = attack_cooldown;
            }
        }
        controller.Move(horizontalMove * Time.fixedDeltaTime * runSpeed);
    }
}
