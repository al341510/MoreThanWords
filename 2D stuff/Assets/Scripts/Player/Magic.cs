using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour {


	[SerializeField]
	private Health health;

	private float startHealth = 100f;

	private AttackCalculate AttackCalc; //script that makes attack calcules

	public CharacterController2D controller;

	public Animator animator;

	public Rigidbody2D Rb;



	public enum playerMagic {NEUTRAL, FIRE, ICE};
	//public playerMagic activeMagic;
	//public playerMagic storedMagic;
	public int magicStored = 1; //	//0 for nothing ,1 for ice ,2 for fire
	public bool usingMagic;
	public float magicTime = 20f;

	
	// Update is called once per frame
	void Update () {
		


		/*
		if (pickUpButton)
		{
			
		}

		if (pickUpButton)
		{
			print("pickupmagic");
			//comprobar si se puede recoger una magia
			//detectar el tipo
			//si se puede:
			//si hielo, storedMagic = playerMagic.ICE .. etc
		}*/

	}




}
