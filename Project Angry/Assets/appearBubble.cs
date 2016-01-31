using UnityEngine;
using System.Collections;

public class appearBubble : MonoBehaviour {
		// Use this for initialization
	public GameObject speech;
	float timer = 0;
	bool _allow = false;

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (_allow) {
			timer += Time.deltaTime;
		}

		if (timer > 4) {
			Destroy (speech.gameObject);
			Destroy (gameObject);
		}

	}
    void OnTriggerEnter2D(Collider2D other)
    {
		GameObject.FindGameObjectWithTag ("audio").GetComponent<AudioSource> ().PlayOneShot(GameObject.FindGameObjectWithTag ("audio").GetComponent<Audios>().audios[7]);
		speech.SetActive (true);
		_allow = true;
    }

}
