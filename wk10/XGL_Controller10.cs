using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// VERSION: S17 WK10

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

		if (upOrDownHeld == false && rightOrLeftHeld == false && !Input.GetButton ("Jump")) {
			
			rb.drag = 1.5f;
			if (!touchingGround) {
				rb.AddForce (0,-10f,0);
			}
		} else {
			rb.drag = 0.1f;
			if (!touchingGround) {
				rb.AddForce (0,-5f,0);
				if (timerJump >= maxJumpTime) {
					rb.AddForce (0,-10f,0);
				}
			}
		}


		Vector3 vel = rb.velocity;
		if (rb.velocity.y > 5f) {
			vel.y = 5f;
		}

		float maxHor = 8f;
		vel.x= Mathf.Clamp (rb.velocity.x,-maxHor,maxHor);
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
