using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// VERSION: S17 Week 9
// Add this script to a Box Collider set to Is Trigger.
// Fill in the two string properties for the desitnation scene,
// and the destination target's name (TargetDoor1, etc - a object you add yourself to the other scene)

// DONT FORGET: Your player object must have the tag set to "Player".

public class SceneChangeTrigger : MonoBehaviour {


	public string destinationSceneName;
	public string destinationTargetName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			Basic3rdPersonController.changePositionOnSceneLoad = true;
			Basic3rdPersonController.destinationTargetName = destinationTargetName;
			SceneManager.LoadScene (destinationSceneName);
		}
	}
}
