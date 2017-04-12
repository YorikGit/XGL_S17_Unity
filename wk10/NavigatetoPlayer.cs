using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


// same idea as NavigatetoClick but automatically moves towards whatever object you assign to 'goal'
public class NavigatetoPlayer : MonoBehaviour {

	// Use this for initialization

	public Transform goal;
	NavMeshAgent nav;


	void Start () {
		nav = GetComponent <NavMeshAgent> ();	
	}
	
	// Update is called once per frame
	void Update () {
		nav.destination = goal.position;
	}
}
