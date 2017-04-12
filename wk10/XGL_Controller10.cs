using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// VERSION: S17 WK10

// This has been updated to use maxJumpTime to limit the jump height of the player
// It also uses raycasts to limit the jump height

//It also adds functionality for checking for falling out of bounds and restting the player

// there is also some 'game feel' code that limits the max velocity of the player (so you don't
// fly all over the place), and so jumping feels a little better.

public class XGL_Controller10 : MonoBehaviour {

	// Use this for initialization
	Rigidbody rb;
	Collider _collider;
	public float pushSpeed = 25f;
	public float jumpSpeed = 30f;
	public float maxJumpTime = 0.8f;
	float timerJump = 0f;

	public float outOfBoundsY = -20f;
	public static string destinationTargetName;
	public static bool changePositionOnSceneLoad;

	// Need this to correctly push the player.
	public Camera cam;

	void Start () {
		if (changePositionOnSceneLoad) {
			changePositionOnSceneLoad = false;
			GameObject g = GameObject.Find (destinationTargetName);
			transform.position = g.transform.position;
		}
		rb = GetComponent <Rigidbody> ();
		_collider = GetComponent <Collider> ();
	}

	void Update () {
		CheckForOutOfBounds ();
	}

	// Physics code like applyforge goes in fixedupdate.
	void FixedUpdate () {


		// GetButtonDown is the same as "Just Pressed" (only returns true ONCE when the key is presseD)
		// GetButton is the same as "Pressed" (returns true AS LONG AS the key is pressed)
		if (Input.GetButton ("Jump")) {
			timerJump += Time.fixedDeltaTime;
			if (timerJump <= maxJumpTime) {
				rb.AddForce (0, jumpSpeed, 0);
			}
		}

		// Cast a ray down from the player to check if the player is near the ground (and thus
		//	allowed to jump again) 
		bool touchingGround = false;
		if (Physics.Raycast (new Ray(transform.position,Vector3.down),0.1f+_collider.bounds.extents.y)) {
			timerJump = 0f;
			touchingGround = true;
		}

		bool rightOrLeftHeld = true;
		if (Input.GetButton ("Right")) {
			Vector3 v = Vector3.Normalize (cam.transform.right);
			v = new Vector3 (v.x, 0, v.z);
			v *= pushSpeed;
			rb.AddForce (v);
		} else if (Input.GetButton ("Left")) {
			Vector3 v = Vector3.Normalize (cam.transform.right);
			v = new Vector3 (v.x, 0, v.z);
			v *= -pushSpeed;
			rb.AddForce (v);
		} else {
			rightOrLeftHeld = false;
		}

		bool upOrDownHeld = true;
		if (Input.GetButton ("Up")) {
			Vector3 v = Vector3.Normalize (cam.transform.forward);
			v = new Vector3 (v.x, 0, v.z);
			v *= pushSpeed;
			rb.AddForce (v);
		} else if (Input.GetButton ("Down")) {
			Vector3 v = Vector3.Normalize (cam.transform.forward);
			v = new Vector3 (v.x, 0, v.z);
			v *= -pushSpeed;
			rb.AddForce (v);
		} else {
			upOrDownHeld = false;
		}

		// This code makes jumping feel better
		if (upOrDownHeld == false && rightOrLeftHeld == false && !Input.GetButton ("Jump")) {

			// Basically, not holding keys down makes drag higher, so you slow down faster
			// But I also add a negative y-force to the player so the drag doesn't make you 
			// fall downwards slower.
			rb.drag = 1.5f;
			if (!touchingGround) {
				rb.AddForce (0,-10f,0);
			}
		} else {

			// If keys are held, and you aren't on the ground, and the jump timer has 'expired'
			// then more force should make you hit the ground faster
			rb.drag = 0.1f;
			if (!touchingGround) {
				rb.AddForce (0,-5f,0);
				if (timerJump >= maxJumpTime) {
					rb.AddForce (0,-10f,0);
				}
			}
		}


		// Limit Velocity of the player
		Vector3 vel = rb.velocity;
		if (rb.velocity.y > 5f) {
			vel.y = 5f;
		}

		float maxHor = 8f;
		// E.g. - Mathf.Clamp(-5,0,5) will return 0, because -5 is less than the minimum value, 0
		vel.x = Mathf.Clamp (rb.velocity.x,-maxHor,maxHor);
		vel.z = Mathf.Clamp (rb.velocity.z, -maxHor, maxHor);
		rb.velocity = vel;

	}

	void CheckForOutOfBounds() {
		if (transform.position.y <= outOfBoundsY) {
			string scene = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene (scene);
		}
	}

}
