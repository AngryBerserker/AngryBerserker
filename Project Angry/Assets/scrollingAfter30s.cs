using UnityEngine;
using System.Collections;

public class scrollingAfter30s : MonoBehaviour {
    private float timeCount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        timeCount = timeCount + Time.deltaTime;
        if ((timeCount > 5) && (timeCount <8))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 2);
        } 
        if ((timeCount > 13) && (timeCount < 16))
        {
            transform.Translate(Vector3.down * Time.deltaTime * 2);
        }
    }
}
