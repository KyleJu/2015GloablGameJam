using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player_Script : MonoBehaviour {
	// Update is called once per frame
	public float gspeed = 5f;
	public float aspeed = 200f;
	public GameObject PlayerTorch;
	public int numTorches = 1;
	public int numDeaths = 0;
	public float score = 1000000;
	public bool isDead;
	public float deathTime;

	public GameObject GUILives;
	public GameObject GUITorches;
	public GameObject nearestTorch;
	public bool grounded = true;
	public bool airBorn = false;
	public int direction = 0; //0 is idle, 1 is left, 2 is right, 3 is jumping, 4 is dead

	public Animator animator;
	public AudioSource footstepsAudio;
	public AudioSource jumpingAudio;
	public AudioSource torchAudio;
	public AudioSource deathAudio;
	public AudioSource bloodAudio;

	public static Player_Script instance;

	public TorchPickup_Script torchPicked;
	public GameObject finish;
	public bool finished;
	public bool endLevel = false;
	public GroundBox_Script groundBoxScript;
	
	public List<TorchPickup_Script> torches = new List<TorchPickup_Script>();

	public Vector3 startingPos;
	public Vector3 playerScale;
	public Vector2 maxSpeedX;
	

	void Awake() {
		instance = this;
	}

	// Use this for initialization
	void Start () {
		endLevel = false;
		startingPos = transform.position;
		playerScale = this.gameObject.transform.localScale; // tried changing to Vector2
		isDead = false;
	}

	void pickupTorch() {
		if ( torchPicked.isPrevious) {
			numTorches++;
			print(numTorches);
			Destroy (nearestTorch);
			nearestTorch = null;
			torchPicked = null;
			GUITorches.SendMessage ("setTorchesUsed", numTorches);
			
		}
	}

	// If the player has a torch it will be spawned
	void spawn_torch() {
		if (numTorches > 0) {
			Vector3 torchPos = new Vector3(transform.position.x, transform.position.y + .3f);
			Instantiate (PlayerTorch, torchPos, Quaternion.identity);
			numTorches--;
			GUITorches.SendMessage ("setTorchesUsed", numTorches);
			torchAudio.Play ();
		}
	}
	/*bool isGrounded() {
		return Physics2D.Linecast (this.transform.position, groundedEndM.position, 1 << LayerMask.NameToLayer ("Level"));
	}*/

	// Update is called once per frame
	void Update () {

		if (!isDead)
			{
			// falling death
			if (transform.position.y < -15) {
				die ();
			}


			//grounded = isGrounded ();
			if (rigidbody2D.velocity.x > 8) {
				rigidbody2D.velocity = new Vector2(8f, rigidbody2D.velocity.y);
			}
			//grounded = Physics2D.Linecast (this.transform.position, groundedEnd.position, 1 << LayerMask.NameToLayer ("Level"));
			if (Input.GetKey(KeyCode.LeftArrow) && grounded){
				transform.position += Vector3.left * gspeed * Time.deltaTime;
				animator.SetFloat("Direction", 1);
				direction = 1;
				if (playerScale.x < 0 && !airBorn) {
					playerScale.x *= -1;
					this.gameObject.transform.localScale = playerScale;
				}
			} else if (!grounded && Input.GetKey(KeyCode.LeftArrow)) {
				rigidbody2D.AddForce(Vector3.left * aspeed/60);
			}
			if (Input.GetKey(KeyCode.RightArrow) && grounded){
				transform.position += Vector3.right * gspeed * Time.deltaTime;
				animator.SetFloat("Direction", 2);
				direction = 2;
				if (playerScale.x > 0 && !airBorn) {
					playerScale.x *= -1;
					this.gameObject.transform.localScale = playerScale;
				}
			} else if (!grounded && Input.GetKey(KeyCode.RightArrow)) {
				rigidbody2D.AddForce(Vector3.right * aspeed/60);
			}
			if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) {// checks if you are within 0.15 position in the Y of the ground)
				// check if finish level, load next scene
				rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 6f);
				if (direction == 1) {
					rigidbody2D.AddForce (Vector3.left * aspeed/2);
				} else if (direction == 2) {
					rigidbody2D.AddForce (Vector3.right * aspeed/2);
				}
				if (finished) {
					endLevel = true;
				} else {
					endLevel = false;
				}
				animator.SetFloat ("Direction", 3);
				airBorn = true;
				jumpingAudio.Play ();
			}
			if (grounded) {
				airBorn = false;
			}
			if (!grounded) {
				animator.SetFloat ("Direction", 3);
			} else if (grounded && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow)) {
				animator.SetFloat ("Direction", 0);
				direction = 0;
				rigidbody2D.velocity = new Vector2(0f, rigidbody2D.velocity.y);
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
				if (torchPicked == null){
					spawn_torch();
				}
				else{
					pickupTorch();
				}
			}

			if (animator.GetFloat ("Direction") == 1 || animator.GetFloat ("Direction") == 2)
			{
				if (!footstepsAudio.isPlaying)
				{
					footstepsAudio.Play ();
				} 
			} else {
			footstepsAudio.Stop();
			}

		} else { // when isDead, death animation for a few seconds
			if ((Time.time - deathTime) > 2) {
				reset ();
			}
		}
	}
	void OnTriggerStay2D(Collider2D coll) {
		if (coll.gameObject.tag == "Finish") {
			finished = true;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Obstacle") {
			Physics2D.IgnoreCollision(collider2D, coll.collider);
		}
	}

	public void die() {
		animator.SetFloat("Direction", 4);
		direction = 4;
		deathTime = Time.time;
		isDead = true;
		deathAudio.Play ();
		bloodAudio.Play ();
		footstepsAudio.Stop ();
	}

	public void reset() {
		transform.position = startingPos;
		isDead = false;
		animator.SetFloat("Direction", 0);
		direction = 0;
		numDeaths++;
		GUILives.SendMessage ("setNumDeaths", numDeaths);
		if (numTorches <= 20) {
			numTorches ++;
			GUITorches.SendMessage ("setTorchesUsed", numTorches);
		}
		
		foreach (var torch in torches) {
			torch.isPrevious = true;
		}
	}

	// check if finish is entered/exited
	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag == "Finish") {
			finish = coll.gameObject;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll) {
		if (coll.gameObject.tag == "Finish") {
			finish = null;
		}
	}
	

	
}
