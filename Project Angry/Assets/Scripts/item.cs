using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class item : MonoBehaviour {
	characterMovingScript script;
	dontDestonload texts;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
			doPowerUp (other);
			Destroy (gameObject);
        }
    }

	void doPowerUp(Collider2D other){
		AudioSource audio = GameObject.FindGameObjectWithTag ("audio").GetComponent<AudioSource> ();
		Audios clips = GameObject.FindGameObjectWithTag ("audio").GetComponent<Audios> ();

		script = other.gameObject.GetComponent<characterMovingScript> ();
		switch (gameObject.tag) {
		case "Pizza":
			if (script.health < 2) {
				script.health++;
				script.hearts [script.health].SetActive (true);
			}
			audio.PlayOneShot (clips.audios [3]);
			break;
		case "Mushroom":
			Debug.Log ("Not yet implemented");
			break;
		case "Beer":
			texts = GameObject.FindGameObjectWithTag ("Survive").GetComponent<dontDestonload> ();
			script.beerCounter++;
			texts.counter++;
			audio.PlayOneShot (clips.audios [4]);
			break;
		}
	}
}
