using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadlevel : MonoBehaviour {

	int index;
	GameObject kamu;

	// Use this for initialization
	void Start () {
		index = SceneManager.GetActiveScene ().buildIndex;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			if (SceneManager.GetActiveScene ().name == "Level1") {
				other.gameObject.GetComponent<characterMovingScript> ()._animPlaying = true;
				kamu = GameObject.FindGameObjectWithTag ("Kamu");
				StartCoroutine ("waitForFriend");
				gameObject.GetComponent<Collider2D> ().enabled = false;
			} else {
				SceneManager.LoadScene (++index);
			}

		}
	}

	IEnumerator waitForFriend(){
		kamuScript script = kamu.GetComponent<kamuScript> ();
		script.speeches [0].SetActive (true);
		GameObject.FindGameObjectWithTag ("audio").GetComponent<AudioSource> ().PlayOneShot(GameObject.FindGameObjectWithTag ("audio").GetComponent<Audios>().audios[7]);
		yield return new WaitForSeconds (5F);
		script.speeches [0].SetActive (false);
		script.speeches [1].SetActive (true);
		GameObject.FindGameObjectWithTag ("audio").GetComponent<AudioSource> ().PlayOneShot(GameObject.FindGameObjectWithTag ("audio").GetComponent<Audios>().audios[6]);
		yield return new WaitForSeconds (4F);
		SceneManager.LoadScene (++index);
	}
}
