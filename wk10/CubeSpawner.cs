using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// XGL S17 wk10
// attach this to a cube

// Here's an example of how you create an object from a prefab, and add it to the scene
// You'll need to use the inspector to drag the prefab into the inspector property.
public class CubeSpawner : MonoBehaviour {

	public GameObject prefab;
	void Start () {

		// Makes a copy of the prefab, adds it to the current scene,
		// and sets its position and rotation, based on the GameObject this
		// script is attached to
		Instantiate(prefab,transform.position + Vector3.one,transform.rotation);


	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
