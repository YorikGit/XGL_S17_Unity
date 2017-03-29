using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basic3rdPersonController : MonoBehaviour {

	// Use this for initialization
	Rigidbody rb;
	public float pushSpeed = 25f;
	public float jumpSpeed = 30f;

	// Need this to correctly push the player.
	public Camera cam;

	void Start () {
		rb = GetComponent <Rigidbody> ();
	}

	void Update () {
		
	}

	// Physics code like applyforge goes in fixedupdate.
	void FixedUpdate () {


		// GetButtonDown is the same as "Just Pressed" (only returns true ONCE when the key is presseD)
		// GetButton is the same as "Pressed" (returns true AS LONG AS the key is pressed)
		if (Input.GetButton ("Jump")) {
			rb.AddForce (0, jumpSpeed, 0);
		}

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
		}

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
		}

		if (rb.velocity.y > 5f) {
			Vector3 v = rb.velocity;
			v.y = 5f;
			rb.velocity = v;
		}


		// Movement that works, but not if you rotate the camera view.
//		if (Input.GetButton ("Up")) {
//			rb.AddForce (0,0,pushSpeed);	
//		} else if (Input.GetButton ("Down")) {
//			rb.AddForce (0,0,-pushSpeed);
//		}

//		if (Input.GetButton ("Right")) {
//			rb.AddForce (pushSpeed,0,0);	
//		} else if (Input.GetButton ("Left")) {
//			rb.AddForce (-pushSpeed,0,0);
//		}





		// ~example~
		// GetAxis ("Horizontal") returns a value from -1 to 1, for LEFT held down or RIGHT held down
//		float hor = Input.GetAxis ("Horizontal");
//		if (hor > 0) {
//			// Now the rigidbody is being pushed at a force between 0 and pushspeed, because hor is
//			// between 0 and 1.
//			rb.AddForce (hor*pushSpeed,0,0);	
//		} else if (Input.GetAxis ("Horizontal") < 0) {
//			rb.AddForce (-pushSpeed,0,0);
//		}

	}

}
