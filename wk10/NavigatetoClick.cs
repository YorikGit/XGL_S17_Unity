using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// wk10, navigates a navmesh agent to a point clicked on scene
// the attached object should have a navmeshagent property
// don't forget to bake your navmesh

public class NavigatetoClick : MonoBehaviour {


	NavMeshAgent nav;


	void Start () {
		nav = GetComponent <NavMeshAgent> ();	
	}
	
	void Update () {


		// Check if mouse was left-clicked
		if (Input.GetMouseButtonDown (0)) {

			// Create a ray with origin at the camera and going *through* the mous eposition
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			// Ray should go for 100 units
			if (Physics.Raycast (ray,out hit,100f)) {

				// The object with the navmeshagent will automatically move to this point.
				nav.destination = hit.point;
			}

		}
	}
}
