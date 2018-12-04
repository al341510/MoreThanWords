using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBrujoAttack : Attacker {
	public GameObject magicNormal;
	public GameObject magicIce;
	public GameObject magicFire;
	private Enemy enemyScript;
	private GameObject newMagic;

	private Enemy.enemyType eType;

	float x, y, direction;

	void Start(){
		enemyScript = GetComponent<Enemy> ();
		eType = enemyScript.enemyMagic;
	}

    // Use this for initialization
    public override void Attack(float x, float y, float direction)
    {
		this.x = x;
		this.y = y;
		this.direction = direction;



		Invoke("spawnMagicAttack", 0.8f); // Añade delay al metodo
    }

	private void spawnMagicAttack(){
		float addX = 1f;
		int multiply = 1;
		if (direction == 1) {
			multiply = -1;
		}
		//Debug.Log (direction);
		if (eType == Enemy.enemyType.FIRE) {
			newMagic = Instantiate (magicFire, new Vector3 (x+multiply*addX, y, 0), Quaternion.Euler (0, 0, 0)) as GameObject;
		} else if (eType == Enemy.enemyType.ICE) {
			newMagic = Instantiate (magicIce, new Vector3 (x+multiply*addX, y, 0), Quaternion.Euler (0, 0, 0)) as GameObject;
		} else if (eType == Enemy.enemyType.NEUTRAL) {
			newMagic = Instantiate (magicNormal, new Vector3 (x+multiply*addX, y, 0), Quaternion.Euler (0, 0, 0)) as GameObject;
		}

		newMagic.GetComponent<MagicCollision>().SetEnemyReference(this.gameObject);
		if (direction == 1) {
			newMagic.transform.Rotate(0, 180, 0);
		}
	}
}
