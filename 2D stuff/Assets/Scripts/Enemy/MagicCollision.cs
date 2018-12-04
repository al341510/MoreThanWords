using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCollision : MonoBehaviour {

	private GameObject enemyReference;
	// Use this for initialization
	void Start () {
		Invoke ("DestroyObject", 1.5f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//Debug.Log ("hola");

		if (collision.CompareTag("Player"))
		{
			collision.gameObject.GetComponent<AttackCalculate>().RecieveDamage(enemyReference.GetComponent<Enemy>());
			//Debug.Log("hit"); 
		}

	}

	public void SetEnemyReference(GameObject gameObject)
	{
		enemyReference = gameObject;
	}

	public void DestroyObject(){
		GameObject.Destroy (gameObject);
	}
}
