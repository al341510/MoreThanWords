using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour {

    [SerializeField] private int life = 3;
    [SerializeField] private int attack = 3;
    [SerializeField] private int keys = 0;           //int en vez de bool, porque hablabamos de poner varias llaves
    [SerializeField] private bool fire = false;      //si tenemos poder de fuego
    [SerializeField] private bool ice = false;       //si tenemos poder de hielo
    [SerializeField] private bool damage = false;    //si recibimos daño

    /*// Use this for initialization
	void Start () {
        

    }

    // Update is called once per frame
    void Update () {
		
	}*/

}
