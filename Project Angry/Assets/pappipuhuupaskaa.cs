using UnityEngine;
using System.Collections;

public class pappipuhuupaskaa : MonoBehaviour {

	characterMovingScript script;
	public GameObject sprite;
	Animator anim;


	// Use this for initialization
	void Start () {
		script = GameObject.FindGameObjectWithTag ("Player").GetComponent<characterMovingScript> ();
		anim = GetComponent<Animator> ();
	}

	void OnBecameVisible(){
		StartCoroutine ("ALKU");
	}

	IEnumerator ALKU(){
		yield return new WaitForSeconds (1F);
		GameObject.FindGameObjectWithTag ("audio").GetComponent<AudioSource> ().PlayOneShot(GameObject.FindGameObjectWithTag ("audio").GetComponent<Audios>().audios[5]);
		script._animPlaying = true;
		sprite.SetActive (true);
		yield return new WaitForSeconds (2F);
		anim.SetBool ("said", true);
		gameObject.transform.localScale = new Vector3 (-1, 1, 1);
		yield return new WaitForSeconds (1F);
		script._animPlaying = false;
		Destroy (gameObject);
	}
}
