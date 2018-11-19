using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
	public float FollowSpeed = 2f;
	public float limitH = 4f;
	public float limitV = 3f;
	public Transform Target;

	private void LateUpdate()
	{
		Vector3 playerPosition = Target.position;
		playerPosition.z = 0;
		Vector3 cameraPosition = transform.position;

		if (playerPosition.x - cameraPosition.x > limitH) {
			cameraPosition.x = playerPosition.x-limitH;
		}

		if (playerPosition.x - cameraPosition.x < -limitH) {
			cameraPosition.x = playerPosition.x+limitH;
		}
		if (playerPosition.y - cameraPosition.y > limitV) {
			cameraPosition.y = playerPosition.y-limitV;
		}

		if (playerPosition.y - cameraPosition.y < -limitV) {
			cameraPosition.y = playerPosition.y+limitV;
		}

		if (playerPosition.x - cameraPosition.x < limitH &&
			playerPosition.x - cameraPosition.x > -limitH &&
			playerPosition.y - cameraPosition.y < limitV &&
			playerPosition.y - cameraPosition.y > -limitV)
				cameraPosition = Vector3.Slerp(cameraPosition, playerPosition, FollowSpeed * Time.deltaTime);

		transform.position = cameraPosition;
	} 
	

}