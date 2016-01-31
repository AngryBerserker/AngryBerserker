using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	GameObject playerChar;
	characterMovingScript charScript;

	Vector3 camPos;
	Vector3 playerPos;
	float furthestPosX;
	float speed;
	// Use this for initialization
	void Start () {
		playerChar = GameObject.FindGameObjectWithTag ("Player");
		charScript = playerChar.GetComponent<characterMovingScript> ();
		playerPos = playerChar.transform.position;
		camPos = new Vector3 (playerPos.x, playerPos.y, -10);
		furthestPosX = playerPos.x;
	}
	
	// Update is called once per frame
	void Update () {

		playerPos = playerChar.transform.position;


		if (playerPos.x > furthestPosX) {
			furthestPosX = playerPos.x;
		}



		camPos = new Vector3 (playerPos.x, playerPos.y, -10);

		speed = 0.1F;

		gameObject.transform.position = Vector3.Slerp(transform.position, camPos, speed);
		Debug.Log (camPos + "");
	}
}
