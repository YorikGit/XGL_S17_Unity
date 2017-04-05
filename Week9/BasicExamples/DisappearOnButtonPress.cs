using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearOnButtonPress : MonoBehaviour {

	public GameObject target;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButton("Jump")) {
			target.SetActive (false);
		} else if (Input.GetButton ("Up")) {
			target.SetActive (true);
		}
	}
}
