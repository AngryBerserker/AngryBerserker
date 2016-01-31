using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIscript : MonoBehaviour {

	Text text;
	string textUI = "= ";
	public int counter;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = textUI + counter;
	}
}
