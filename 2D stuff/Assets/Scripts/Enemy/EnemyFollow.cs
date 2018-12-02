using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
	public float FollowSpeed = 2f;
	public float limitH = 4f;
	public float limitV = 3f;
	public Transform TargetEnemy;

	private void LateUpdate()
	{
		Vector3 enemyPosition = TargetEnemy.position;
        enemyPosition.z = 0;
		Vector3 particlesPosition = transform.position;

		if (enemyPosition.x - particlesPosition.x > limitH) {
            particlesPosition.x = enemyPosition.x-limitH;
		}

		if (enemyPosition.x - particlesPosition.x < -limitH) {
            particlesPosition.x = enemyPosition.x+limitH;
		}
		if (enemyPosition.y - particlesPosition.y > limitV) {
            particlesPosition.y = enemyPosition.y-limitV;
		}

		if (enemyPosition.y - particlesPosition.y < -limitV) {
            particlesPosition.y = enemyPosition.y+limitV;
		}

		if (enemyPosition.x - particlesPosition.x < limitH &&
            enemyPosition.x - particlesPosition.x > -limitH &&
            enemyPosition.y - particlesPosition.y < limitV &&
            enemyPosition.y - particlesPosition.y > -limitV)
            particlesPosition = Vector3.Slerp(particlesPosition, enemyPosition, FollowSpeed * Time.deltaTime);
        
        transform.position = particlesPosition;
	} 
	

}