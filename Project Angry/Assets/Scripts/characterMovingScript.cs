using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class characterMovingScript : MonoBehaviour {
    public Vector3 movement = new Vector3(0, 0); //Helps too see what direction should we go
	Vector3 faceDir = new Vector3(1, 1, 1); //Reference to save sprite lookdirection

	AudioSource audio;
	Audios clips;

    public float horizontal; //For input to left and right
    float vertical;
    Rigidbody2D rig2d;
	public LayerMask environment;

	int levelsToRun;

	public int beerCounter;

	public int health = 2;
	public GameObject[] hearts;

    public bool _isJumping = false; // Check if character is falling
	bool _canMove = true;
	public bool _tookDamage = false; // Did someone do damage to this character
	bool _isAttacking = false;
	public bool _animPlaying = false;
	

    public GameObject rayStart; // Empty gameObject from where we cast a raycast
	public GameObject rayFront;

	Animator anim;

    void Start () {
		audio = GameObject.FindGameObjectWithTag ("audio").GetComponent<AudioSource> ();
		clips = GameObject.FindGameObjectWithTag ("audio").GetComponent<Audios> ();
		levelsToRun = SceneManager.GetActiveScene ().buildIndex;
        rig2d = GetComponent<Rigidbody2D>(); // get the rigidbody2d from gameobject
		anim = GetComponent<Animator>();
	}

	void FixedUpdate(){
		if (!_animPlaying) {
			if (_canMove) {
				moveCharacter ();
			}
		}

	}

	void Update () {


			Attack ();
			checkFalling (); // Continuosly check are we falling?
			checkGameStatus ();
		
		if (!_canMove || _animPlaying) {
			horizontal = 0;
			anim.SetInteger ("walkInt", 0);
		}
    }

	void moveCharacter(){
		if (levelsToRun == 2 || levelsToRun == 4) {
			horizontal = 1;
		} else {
			horizontal = Input.GetAxis("Horizontal"); // Get horizontal input
		}

		if (horizontal != 0) { // If we are pressing left or right assign it to movement
			transform.Translate (Vector2.right * 5 * horizontal * Time.deltaTime);
			anim.SetInteger ("walkInt", 1);

		} else {
			anim.SetInteger ("walkInt", 0);
		}

		if (horizontal < 0) { // check what direction we should be going left == <0 and right == >0 
			faceDir = new Vector3 (-1, 1, 1);
		} else if (horizontal > 0) {
			faceDir =  new Vector3 (1, 1, 1);
		}

		transform.localScale = faceDir; //Set lookdirection
	}

	void checkFalling(){
		Vector3 rayDir = new Vector3 (0, -1f, 0);

		RaycastHit2D ray = Physics2D.Raycast(rayStart.transform.position, rayDir, 0.3f); //create a ray to check if there is ground under the feet
		Debug.DrawRay(rayStart.transform.position, rayDir, Color.blue); //Debugging, remove when done
		if (Input.GetButtonDown("Jump") && !_isJumping) //Did we press spacebar and are we jumping or not?
		{
			rig2d.AddForce (Vector2.up * 525);
			_isJumping = true; //Declare that we jumped
		}

		if (ray.collider == null) //If we fall etc, we have to say this to not jump in air
		{
			_isJumping = true;
		}

		if (_isJumping) { // lets stick in here while we are falling/jumping
			anim.SetBool ("_isJumping", true);
			if (ray.collider != null) {  //Ray detects something
				switch (ray.collider.tag) {
				case "Ground":
					_isJumping = false;
					break;
				case "Enemy":
					int power = 125;
					if (Input.GetButton ("Jump")) {
						power = 200;
					}
					rig2d.AddForce (Vector2.up * power);
					break;
				}
			}
		} else {
			anim.SetBool ("_isJumping", false);
			if (!audio.isPlaying && horizontal != 0) {
				audio.Play ();
			}
		}

	}

	void Attack(){
		if (Input.GetButtonDown ("Fire1") && !_isAttacking) {
			StartCoroutine ("AttackEnemy");
			_isAttacking = true;
		}
	}

	void checkFront(){
		
	}

	void checkGameStatus(){
		if (health < 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
		}
	}

	public IEnumerator invincibility(){
		float time = 2f;
		_canMove = false;
		Debug.Log ("DO");
		yield return new WaitForSeconds (time);
		_tookDamage = false;
		_canMove = true;
	}

	public IEnumerator AttackEnemy(){
		anim.SetBool ("_isAttacking", true);
		audio.PlayOneShot (clips.audios [1]);
		yield return new WaitForSeconds(0.3f);
		Vector3 forwardRay = new Vector3 (faceDir.x, 0, 0);
		RaycastHit2D ray = Physics2D.Raycast(rayFront.transform.position, forwardRay, 0.5f); //scan front
		if (ray.collider != null && ray.collider.tag == "Enemy") {
			ray.collider.GetComponent<AI_Walk> ().dying = true;
			ray.collider.GetComponent<AI_Walk> ().StartCoroutine("die");
		}
		yield return new WaitForSeconds(0.2f);
		_isAttacking = false;
		anim.SetBool ("_isAttacking", false);
	}
}
