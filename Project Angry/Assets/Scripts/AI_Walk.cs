using UnityEngine;
using System.Collections;
using UnityEngine.Sprites;

public class AI_Walk : MonoBehaviour {

	Rigidbody2D rig2d;

	Animator anim;
	public bool dying = false;

	public Transform rayStart;
	public GameObject rockPrefab;
	public GameObject projPos;

	private GameObject playerObj;
	public GameObject rayFront;

	bool _active = false;
	bool _isAttacking = false;

	public bool _ranged = false;

	Vector3 aiScale;
	Vector3 dir = new Vector3(0,0,0);
	characterMovingScript playerScript = null;

	float speed = 2;

	Sprite aiSprite;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		rig2d = GetComponent<Rigidbody2D> ();
		aiSprite = GetComponent<Sprite> ();
		if (_ranged) {
			playerObj = GameObject.FindGameObjectWithTag("Player");
		}
		aiScale = transform.localScale;
	}

	void FixedUpdate(){
		if (_active && !_ranged) {
			MoveForward ();
		}
	}

	// Update is called once per frame
	void Update () {

		if (!dying) {
			if (_active && !_ranged) {
				checkEdges ();
			}

			if (_ranged && _active) {
				if (playerObj.transform.position.x < transform.position.x) {
					aiScale.x = -1;
				} else {
					aiScale.x = 1;
				}

				transform.localScale = aiScale;
			}
		} else {
			speed = 0;
			anim.SetBool ("dead", true);
		}
	}

	void MoveForward(){
		transform.Translate (Vector2.right * aiScale.x * speed * Time.deltaTime);
	}

	void checkEdges(){
		
		Vector3 rayDir = new Vector3 (aiScale.x, -1f, 0);


		RaycastHit2D ray = Physics2D.Raycast(rayStart.position, Vector3.down, 2f);
		Debug.DrawLine (rayStart.position, rayStart.position + rayDir, Color.red);
		Debug.DrawRay(rayStart.position, Vector3.down, Color.blue);

		if (ray.collider != null) {
			switch (ray.collider.tag) {
			case "Player":
				if (!_ranged && !_isAttacking) {
					StartCoroutine ("AttackEnemy");
					_isAttacking = true;
				}

				break;
			default:
				break;
			}

		} else {
			aiScale.x *= -1;
		}


		transform.localScale = aiScale;
	}

	void hitPlayer(RaycastHit2D ray){
		playerScript = ray.collider.gameObject.GetComponent<characterMovingScript> ();
		Rigidbody2D playerRig = ray.collider.gameObject.GetComponent<Rigidbody2D> ();
		if (!playerScript._tookDamage) {
			playerRig.AddForce (new Vector2 (aiScale.x, 1) * 300);
			playerScript.StartCoroutine ("invincibility");
			StartCoroutine ("WaitLaugh");
			playerScript.hearts[playerScript.health].SetActive(false);
			playerScript.health--;
			playerScript._tookDamage = true;

		}
	}

	void OnBecameVisible(){
		if (_ranged && !_active) {
			StartCoroutine ("ThrowRocks");
		}
		_active = true;
	}

	void OnBecameInvisible(){
		if (transform.position.x < GameObject.FindGameObjectWithTag ("Player").transform.position.x) {
			Debug.Log ("Destroyed " + gameObject.name);
			Destroy (gameObject);
		}
	}

	IEnumerator ThrowRocks(){
		GameObject instObj;
		bool throwRock = true;

		while (throwRock) {
			instObj = Instantiate (rockPrefab, projPos.transform.position, Quaternion.identity) as GameObject;
			instObj.GetComponent<Projectile>().dir = aiScale.x;
			Debug.Log ("DONE");
			yield return new WaitForSeconds(1.5f);
		}

	}

	public IEnumerator AttackEnemy(){
		//anim.SetBool ("_isAttacking", true);
		yield return new WaitForSeconds(0.3f);
		Vector3 forwardRay = new Vector3 (aiScale.x, 0, 0);
		RaycastHit2D ray = Physics2D.Raycast(rayFront.transform.position, forwardRay, 0.5f); //scan front
		if (ray.collider != null && ray.collider.tag == "Player") {
			playerScript = ray.collider.gameObject.GetComponent<characterMovingScript> ();
			Rigidbody2D playerRig = ray.collider.gameObject.GetComponent<Rigidbody2D> ();
			if (!playerScript._tookDamage && !dying) {
				playerRig.AddForce (new Vector2 (aiScale.x, 1) * 100);
				playerScript.StartCoroutine ("invincibility");
				StartCoroutine ("WaitLaugh");
				playerScript.hearts[playerScript.health].SetActive(false);
				playerScript.health--;
				playerScript._tookDamage = true;
			}
		}
		yield return new WaitForSeconds(0.2f);
		_isAttacking = false;
		//anim.SetBool ("_isAttacking", false);
	}

	IEnumerator WaitLaugh(){
		speed = 0;
		yield return new WaitForSeconds (2F);
		speed = 3;
	}

	public IEnumerator die(){
		GetComponent<Rigidbody2D>().gravityScale = 0;
		GetComponent<BoxCollider2D> ().enabled = false;
		AudioSource audio = GameObject.FindGameObjectWithTag ("audio").GetComponent<AudioSource> ();
		Audios clips = GameObject.FindGameObjectWithTag ("audio").GetComponent<Audios> ();
		audio.PlayOneShot (clips.audios [0]);
		yield return new WaitForSeconds (0.8f);
		Destroy (gameObject);
	}
}
