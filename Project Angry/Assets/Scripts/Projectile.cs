using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public float dir;

	void Update () {
		transform.position += Vector3.right * dir * 5 * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			characterMovingScript script = other.gameObject.GetComponent<characterMovingScript> ();
			script.hearts [script.health--].SetActive (false);
			Destroy (gameObject);
		}
	}

	void OnBecameInvisible(){
		Destroy (gameObject);
	}
}
