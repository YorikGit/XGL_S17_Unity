using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Version: S17 WK9
// To the textManager field, Drag in object with a Textdisplay object attached
// Type in the text you want to show into "message"

// Possible extensions of this script:
// Adding an array of messages
// Having it support playing multiple message sin a row, playing different messages based on game state.

public class BoxOverlapNPC : MonoBehaviour {

	public Textdisplay textManager;
	public string message;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			// Show the dialog
			textManager.startDialog (message);
		}
	}
}
