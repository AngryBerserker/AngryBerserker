using UnityEngine;
using System.Collections;

public class dontDestonload : MonoBehaviour {

	UIscript texts;
	public int counter;

	// Use this for initialization
	void Start () {
		
	}

	void Awake(){
		DontDestroyOnLoad (gameObject);
	}


	// Update is called once per frame
	void Update () {
		texts = GameObject.FindGameObjectWithTag ("Counter").GetComponent<UIscript> ();
		texts.counter = counter;

	}
}
