using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


//XGL s17 wk10
// Attach this to your player

// This script checks for key presses, saves the current position and loads it as neeed.
// Note this will ONLY work if your project has my SaveManager script.

public class PlayerCheckpointTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.Return)) {
			SaveManager.position = gameObject.transform.position;
			SaveManager._Save ();
		}


		if (Input.GetKeyDown (KeyCode.L)) {
			SaveManager._Load ();
			gameObject.transform.position = SaveManager.position;
		}
	}



}
