using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DestroyPlayer : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}
}
